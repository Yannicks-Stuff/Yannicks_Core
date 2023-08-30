using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;


namespace Yannick.Network.Protocol.TCP
{
    /// <summary>
    /// Represents a TCP server.
    /// </summary>
    public class Server : IDisposable
    {
        private readonly SemaphoreSlim _connectionSemaphore;
        private readonly Socket _serverSocket;

        private readonly ConcurrentDictionary<IPEndPoint, User> _users;
        private readonly ConcurrentBag<IPEndPoint> _whiteList;

        /// <summary>
        /// Gets the host IP address.
        /// </summary>
        public readonly IPAddress Host;

        /// <summary>
        /// Gets the server's port.
        /// </summary>
        public readonly ushort Port;

        protected int MaxNativeQueue = 100;
        protected int MaxQueueThreads = 100;
        protected int MaxUserCount = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        public Server(IPAddress host, ushort port)
        {
            Host = host;
            Port = port;

            _users = new ConcurrentDictionary<IPEndPoint, User>();
            _whiteList = new ConcurrentBag<IPEndPoint>();
            _connectionSemaphore = new SemaphoreSlim(MaxQueueThreads, MaxQueueThreads);

            _serverSocket = new Socket(host.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Gets a value indicating whether the server should shut down.
        /// </summary>
        public bool RequestShutdown { get; private set; } = false;

        /// <summary>
        /// Disposes the server's resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var user in _users.Values)
                user.Dispose();

            _serverSocket.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Triggered when a new user connects.
        /// </summary>
        public event Action<User>? OnConnect;

        /// <summary>
        /// Triggered when a user disconnects.
        /// </summary>
        public event Action<User>? OnDisconnect;

        /// <summary>
        /// Triggered when data is received from a user.
        /// </summary>
        public event Action<User, byte[]>? OnDataReceived;

        /// <summary>
        /// Triggered when data is available to read from a user.
        /// </summary>
        public event Action<User> OnAvailable;

        /// <summary>
        /// Starts the server synchronously.
        /// </summary>
        public void Start()
        {
            StartAsync().Wait();
        }

        /// <summary>
        /// Starts the server asynchronously.
        /// </summary>
        public async Task StartAsync()
        {
            _serverSocket.Bind(new IPEndPoint(Host, Port));
            _serverSocket.Listen(MaxNativeQueue);

            while (!RequestShutdown)
            {
                await _connectionSemaphore.WaitAsync();
                AcceptUser();
            }
        }

        private async void AcceptUser()
        {
            try
            {
                var socket = await _serverSocket.AcceptAsync();
                var user = new User(socket, this);

                if (_users.Count >= MaxUserCount || !_whiteList.Contains(user.EndPoint))
                {
                    user.Dispose();
                    return;
                }

                _users[user.EndPoint] = user;

                OnConnect?.Invoke(user);
            }
            finally
            {
                _connectionSemaphore.Release();
            }
        }

        /// <summary>
        /// Sends data to a specific user.
        /// </summary>
        public void SendToUser(User user, byte[] data)
        {
            user._Send(data);
        }

        /// <summary>
        /// Broadcasts data to all users.
        /// </summary>
        public void Broadcast(byte[] data)
        {
            foreach (var user in _users.Values)
                user._Send(data);
        }

        /// <summary>
        /// Requests the server to shut down.
        /// </summary>
        public void Shutdown()
        {
            RequestShutdown = true;
        }

        /// <summary>
        /// Adds an IP endpoint to the whitelist.
        /// </summary>
        public void AddToWhiteList(IPEndPoint endpoint)
        {
            _whiteList.Add(endpoint);
        }

        /// <summary>
        /// Removes an IP endpoint from the whitelist.
        /// </summary>
        public bool RemoveFromWhiteList(IPEndPoint endpoint)
        {
            return _whiteList.TryTake(out _);
        }

        /// <summary>
        /// Represents a connected user.
        /// </summary>
        public class User : IDisposable
        {
            /// <summary>
            /// Gets the endpoint of the user.
            /// </summary>
            public readonly IPEndPoint EndPoint;

            protected readonly Server Server;
            protected readonly Socket Socket;

            private Task? _heart = null;
            private CancellationTokenSource? _token = null;
            protected int BufferSize = 1024;

            /// <summary>
            /// Initializes a new instance of the <see cref="User"/> class.
            /// </summary>
            public User(Socket socket, Server server)
            {
                Socket = socket;
                Server = server;
                EndPoint = Socket.RemoteEndPoint as IPEndPoint ??
                           throw new NotImplementedException("Unexpected remote endpoint type.");
            }

            /// <summary>
            /// Gets or sets a value that specifies the amount of time after which a synchronous receive call will time out.
            /// </summary>
            protected TimeSpan ReceiveTimeout
            {
                get => TimeSpan.FromMilliseconds(Socket.ReceiveTimeout);
                set => Socket.ReceiveTimeout = value.TotalMilliseconds > int.MaxValue
                    ? int.MaxValue
                    : Convert.ToInt32(value.TotalMilliseconds);
            }

            /// <summary>
            /// Gets or sets the listening state of the user.
            /// </summary>
            public bool Listen
            {
                get => _heart is not { Status: TaskStatus.Running };
                set
                {
                    switch (value)
                    {
                        case true when _token == null:
                            _token = new CancellationTokenSource();
                            _heart = Task.Run(HeartTick, _token.Token);
                            break;
                        case false when _token != null:
                            _token.Cancel();
                            _token = null;
                            break;
                    }
                }
            }

            /// <summary>
            /// Disposes the user's resources.
            /// </summary>
            public void Dispose()
            {
                Server.OnDisconnect?.Invoke(this);
                Socket.Dispose();
            }

            /// <summary>
            /// Triggered when data is received.
            /// </summary>
            public event Action<byte[]> OnReceive;

            /// <summary>
            /// Triggered when data is available to read
            /// </summary>
            public event Action OnAvailable;

            private void HeartTick()
            {
                try
                {
                    while (!_token?.Token.IsCancellationRequested ?? true)
                    {
                        if (!Socket.Poll(1000, SelectMode.SelectRead))
                            continue;

                        if (BufferSize <= 0)
                        {
                            OnAvailable?.Invoke();
                            Server.OnAvailable?.Invoke(this);
                        }

                        var buffer = new byte[BufferSize];
                        var size = Socket.Receive(buffer);

                        if (size == 0)
                        {
                            Dispose();
                            break;
                        }

                        var receivedData = new byte[size];
                        Array.Copy(buffer, receivedData, size);

                        OnReceive?.Invoke(receivedData);
                        Server.OnDataReceived?.Invoke(this, receivedData);
                    }
                }
                catch (SocketException)
                {
                    Dispose();
                }
            }

            /// <summary>
            /// Sends data to the user.
            /// </summary>
            protected void Send(byte[] data) => Socket.Send(data);

            internal void _Send(byte[] data) => Send(data);

            /// <summary>
            /// Sends data to the user.
            /// </summary>
            protected int Receive(byte[] data) => Socket.Receive(data);

            internal int _Receive(byte[] data) => Receive(data);

            public static implicit operator IPEndPoint(User user) => user.EndPoint;
            public static implicit operator Socket(User user) => user.Socket;
        }
    }
}