using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Yannick.Extensions.IntPtrExtensions;
using Yannick.Native.OS.Windows.Win32;

namespace Yannick.Native.OS.Windows.UI.Forms;

public sealed class Window : IDisposable
{
    private static Dictionary<OnTickA, User32.WM> HandlerA = new Dictionary<OnTickA, User32.WM>();
    private static Dictionary<OnTickR, User32.WM> HandlerR = new Dictionary<OnTickR, User32.WM>();

    /// <summary>
    /// Gets the host process.
    /// </summary>
    public readonly Process Host;

    private ushort Atom;
    private IntPtr Handle;

    private bool RequestStop;
    private bool Visibly;

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/> class with default values.
    /// </summary>
    public Window() : this(null, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/> class with the specified title and state.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="state">The initial state of the window.</param>
    public Window(string title, User32.ShowWindowCommands state = User32.ShowWindowCommands.Normal) : this(null,
        title, state)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/> class with the specified window handle.
    /// </summary>
    /// <param name="wHandle">The handle of the window.</param>
    public Window(IntPtr wHandle)
    {
        Handle = wHandle;
        Host = Process.GetProcessById((int)User32.GetWindowThreadProcessId(wHandle));
        OnCallback += Tick;
        State = User32.ShowWindowCommands.Normal;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/> class with the specified host process, title, and state.
    /// </summary>
    /// <param name="host">The host process for the window. If null, the current process is used.</param>
    /// <param name="title">The title of the window.</param>
    /// <param name="state">The initial state of the window.</param>
    public Window(Process? host, string title = "", User32.ShowWindowCommands state = User32.ShowWindowCommands.Normal)
    {
        if (host != null)
        {
            Handle = host.MainWindowHandle;
            Host = host;
            //CurrentMenu = new Yannick.Native.OS.Windows.UI.Forms.Window.Menu(this);
        }
        else
        {
            Host = Process.GetCurrentProcess();
            Title = title;
        }

        State = state;
        RequestStop = false;
        OnCallback += Tick;
    }

    /// <summary>
    /// Gets the state of the window.
    /// </summary>
    public User32.ShowWindowCommands State { get; }

    /// <summary>
    /// Gets or sets the title (caption) of the window.
    /// </summary>
    /// <remarks>
    /// Retrieving the title involves querying the window text length and then retrieving the text.
    /// Setting the title directly sets the window text.
    /// </remarks>
    public string Title
    {
        get
        {
            var size = User32.GetWindowTextLength(Handle);
            if (size > 0)
            {
                var len = size + 1;
                var sb = new StringBuilder(len);
                return ((User32.GetWindowText(Handle, sb, len) > 0) ? sb.ToString() : string.Empty) ??
                       string.Empty;
            }

            return string.Empty;
        }
        set => User32.SetWindowText(Handle, value);
    }

    /// <summary>
    /// Gets or sets the size of the window, in pixels.
    /// </summary>
    /// <value>
    /// The size of the window.
    /// </value>
    public Size Size
    {
        set => SetSize(value.Width, value.Height);
        get
        {
            var rect = Rectangle;
            return new Size(rect.Width, rect.Height);
        }
    }

    /// <summary>
    /// Gets the size of the client area of the window, in pixels.
    /// </summary>
    /// <value>
    /// The size of the client area.
    /// </value>
    public Size ClientSize
    {
        get
        {
            var k = ClientRectangle;
            return new Size(k.Width, k.Height);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is visible.
    /// </summary>
    /// <value>
    /// <c>true</c> if the window is visible; otherwise, <c>false</c>.
    /// </value>
    public bool IsVisible => User32.IsWindowVisible(Handle);

    /// <summary>
    /// Gets a value indicating whether the window is enabled for user input.
    /// </summary>
    /// <value>
    /// <c>true</c> if the window is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool IsEnabled => User32.IsWindowEnabled(Handle);

    /// <summary>
    /// Gets the window rectangle of the current window.
    /// </summary>
    /// <remarks>
    /// This property retrieves the dimensions of the bounding rectangle of the window in screen coordinates.
    /// </remarks>
    /// <returns>
    /// A <see cref="Rectangle"/> structure that represents the bounding rectangle of the window in screen coordinates.
    /// </returns>
    public Rectangle Rectangle
    {
        get
        {
            User32.GetWindowRect(Handle, out var a);
            return a;
        }
    }

    /// <summary>
    /// Gets the client rectangle of the current window.
    /// </summary>
    /// <remarks>
    /// This property retrieves the dimensions of the client area of the window. The client area is the portion of the window's interior where the application displays output.
    /// </remarks>
    /// <returns>
    /// A <see cref="Rectangle"/> structure that represents the client area of the window.
    /// </returns>
    public Rectangle ClientRectangle
    {
        get
        {
            User32.GetClientRect(Handle, out var k);
            return k;
        }
    }

    /// <summary>
    /// Gets or sets the window long value at the specified index.
    /// </summary>
    /// <param name="index">The index at which to get or set the value, represented by a <see cref="User32.WindowLongFlags"/> enumeration value.</param>
    /// <remarks>
    /// This property allows you to get or set the window long value at the specified index. It is a wrapper for the GetWindowLongPtr and SetWindowLongPtr functions of the User32 library.
    /// </remarks>
    /// <returns>
    /// The window long value at the specified index.
    /// </returns>
    public IntPtr this[User32.WindowLongFlags index]
    {
        get => User32.GetWindowLongPtr(Handle, (int)index);
        set => User32.SetWindowLongPtr(Handle, (int)index, value);
    }


    /// <summary>
    /// Gets or sets the styles of the window. This corresponds to the GWL_STYLE index in the User32.dll.
    /// </summary>
    public User32.WindowStyles Styles
    {
        /// <summary>
        /// Gets the window styles for the current window.
        /// </summary>
        /// <value>
        /// The window styles for the current window, represented as a <see cref="User32.WindowStyles"/> enumeration value.
        /// </value>
        get => (User32.WindowStyles)this[User32.WindowLongFlags.GWL_STYLE].ToSafeInt32();

        /// <summary>
        /// Sets the window styles for the current window.
        /// </summary>
        /// <value>
        /// The window styles for the current window, represented as a <see cref="User32.WindowStyles"/> enumeration value.
        /// </value>
        set => this[User32.WindowLongFlags.GWL_STYLE] = new IntPtr((int)value);
    }

    /// <summary>
    /// Gets or sets the extended styles of the window. This corresponds to the GWL_EXSTYLE index in the User32.dll.
    /// </summary>
    public User32.WindowStylesEx StylesEx
    {
        /// <summary>
        /// Gets the extended window styles for the current window.
        /// </summary>
        /// <value>
        /// The extended window styles for the current window, represented as a <see cref="User32.WindowStylesEx"/> enumeration value.
        /// </value>
        get => (User32.WindowStylesEx)this[User32.WindowLongFlags.GWL_EXSTYLE].ToSafeInt32();

        /// <summary>
        /// Sets the extended window styles for the current window.
        /// </summary>
        /// <value>
        /// The extended window styles for the current window, represented as a <see cref="User32.WindowStylesEx"/> enumeration value.
        /// </value>
        set => this[User32.WindowLongFlags.GWL_EXSTYLE] = new IntPtr((int)value);
    }

    /// <summary>
    /// Gets a value indicating whether the current window has focus.
    /// </summary>
    /// <value>
    /// <c>true</c> if the current window has focus; otherwise, <c>false</c>.
    /// </value>
    public bool HasFocus => User32.GetFocus() == Handle;


    //public Yannick.Native.OS.Windows.UI.Forms.Window.Menu CurrentMenu { get; private set; }
    /// <summary>
    /// Gets or sets a value indicating whether the window is visible.
    /// </summary>
    public bool Show
    {
        get => Visibly;
        set
        {
            Visibly = value;
            if (Visibly)
                User32.ShowWindow(Handle, User32.ShowWindowCommands.Show);
            else
                User32.ShowWindow(Handle, User32.ShowWindowCommands.Hide);
        }
    }

    /// <summary>
    /// Disposes of the window and associated resources.
    /// </summary>
    public void Dispose()
    {
        OnCallback -= Tick;
        Host.Dispose();
    }

    private static event Func<IntPtr, uint, IntPtr, IntPtr, IntPtr?>? OnCallback;
    private static event OnTick? Handler;
    private static event OnTickReadOnly? HandlerReadOnly;

    /// <summary>
    /// Handles window messages for the current window and invokes any registered handlers for the specified message.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="msg">The window message.</param>
    /// <param name="wParam">Additional message-specific information.</param>
    /// <param name="lParam">Additional message-specific information.</param>
    /// <returns>Returns the result of the message handling or null if the default window procedure should be called.</returns>
    private IntPtr? Tick(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        if (hWnd == Handle)
        {
            var wm = (User32.WM)msg;
            HandlerReadOnly?.Invoke(wm, wParam, lParam);
            foreach (var a in HandlerA)
                if ((uint)a.Value == msg)
                    a.Key.Invoke(wParam, lParam);
            var @if = Handler?.Invoke(wm, wParam, lParam);
            if (@if.HasValue && @if.Value != IntPtr.Zero)
                return @if;

            foreach (var a in HandlerR)
                if ((uint)a.Value == msg)
                    return a.Key.Invoke(msg, wParam, lParam);
        }

        return null;
    }

    /// <summary>
    /// Initializes a new window class and creates a window with the specified parameters.
    /// </summary>
    /// <param name="style">The window class styles. Defaults to <c>User32.ClassStyles.VerticalRedraw | User32.ClassStyles.HorizontalRedraw</c>.</param>
    /// <param name="icon">The handle to the icon for the window class. Defaults to null.</param>
    /// <param name="cursor">The handle to the cursor for the window class. Defaults to null.</param>
    /// <param name="background">The handle to the background brush for the window class. Defaults to null.</param>
    /// <param name="menuName">The name of the menu resource. Defaults to null.</param>
    /// <param name="className">The name of the window class. Defaults to null.</param>
    /// <exception cref="Win32Exception">Thrown when the <c>User32.RegisterClassEx2</c> or <c>User32.CreateWindowEx2</c> methods fail.</exception>
    public void Init(
        User32.ClassStyles style = User32.ClassStyles.VerticalRedraw | User32.ClassStyles.HorizontalRedraw,
        IntPtr? icon = null, IntPtr? cursor = null, IntPtr? background = null, string? menuName = null,
        string? className = null)
    {
        var wcx = new User32.WNDCLASSEX();

        wcx.cbSize = Marshal.SizeOf(wcx);
        wcx.style = (int)style;

        var address2 = Marshal.GetFunctionPointerForDelegate((Delegate)(User32.WndProc)MainWndProc);
        wcx.lpfnWndProc = address2;

        wcx.cbClsExtra = 0;
        wcx.cbWndExtra = 0;
        wcx.hInstance = Host.Handle;
        wcx.hIcon = icon ?? User32.LoadIcon(
            IntPtr.Zero, new IntPtr((int)User32.SystemIcons.IDI_APPLICATION));
        //wndClass.hCursor = WinAPI.LoadCursor(IntPtr.Zero, (int)IdcStandardCursor.IDC_ARROW);  
        wcx.hCursor = cursor ?? User32.LoadCursor(IntPtr.Zero, User32.IDC_ARROW);
        wcx.hbrBackground = background ?? Gdi32.GetStockObject(Gdi32.StockObjects.WHITE_BRUSH);
        wcx.lpszMenuName = menuName ?? "MainMenu";
        wcx.lpszClassName = className ?? "MainWClass";

        Atom = User32.RegisterClassEx2(ref wcx);
        if (Atom == 0)
            throw new Win32Exception("Failed to call RegisterClasEx");

        Handle = User32.CreateWindowEx2(
            0,
            //"MainWClass",  
            Atom,
            Title,
            User32.WindowStyles.WS_OVERLAPPED,
            User32.CW_USEDEFAULT,
            User32.CW_USEDEFAULT,
            500,
            500,
            IntPtr.Zero,
            IntPtr.Zero,
            Host.Handle,
            IntPtr.Zero);

        if (Handle == IntPtr.Zero)
            throw new Win32Exception("Failed to call InitInstance");

        User32.ShowWindow(Handle, State);
        User32.UpdateWindow(Handle);
        //CurrentMenu = new Yannick.Native.OS.Windows.UI.Forms.Window.Menu(this);
    }

    /// <summary>
    /// Main window procedure that handles incoming window messages.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="msg">The window message.</param>
    /// <param name="wParam">Additional message-specific information.</param>
    /// <param name="lParam">Additional message-specific information.</param>
    /// <returns>Returns the result of the message handling or the result of the default window procedure.</returns>
    private static IntPtr MainWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        IntPtr? rr = null;
        if (OnCallback != null)
            rr = OnCallback(hWnd, msg, wParam, lParam);

        switch ((User32.WM)msg)
        {
            case User32.WM.DESTROY:
                User32.PostQuitMessage(0);
                return IntPtr.Zero;
        }

        return rr ?? User32.DefWindowProc(hWnd, (User32.WM)msg, wParam, lParam);
    }

    /// <summary>
    /// Starts processing messages for the window until a stop is requested.
    /// </summary>
    public void Start()
    {
        User32.MSG msg;
        bool hasMessage;

        while ((hasMessage = User32.GetMessage(out msg, IntPtr.Zero, 0, 0)) && hasMessage || !RequestStop)
        {
            User32.TranslateMessage(ref msg);
            User32.DispatchMessage(ref msg);
        }
    }

    /// <summary>
    /// Sets the focus to the window.
    /// </summary>
    public void SetFocus() => User32.SetFocus(Handle);

    /// <summary>
    /// Sets the size of the window to the specified width and height.
    /// </summary>
    /// <param name="width">The desired width of the window.</param>
    /// <param name="height">The desired height of the window.</param>
    /// <returns>Returns true if the window size is successfully set, otherwise false.</returns>
    public bool SetSize(int width, int height)
    {
        return User32.SetWindowPos(Handle, IntPtr.Zero, -1, -1, width, height, (User32.WindowPositionFlags)22);
    }

    //public bool SetMenu(Yannick.Native.OS.Windows.UI.Forms.Window.Menu menu) => User32.SetMenu(Handle, menu);
    /// <summary>
    /// Sets the position of the window to the specified x and y coordinates.
    /// </summary>
    /// <param name="x">The desired x-coordinate of the window.</param>
    /// <param name="y">The desired y-coordinate of the window.</param>
    /// <returns>Returns true if the window position is successfully set, otherwise false.</returns>
    public bool SetPosition(int x, int y)
    {
        return User32.SetWindowPos(Handle, IntPtr.Zero, x, y, -1, -1, (User32.WindowPositionFlags)21);
    }

    /// <summary>
    /// Centers the window on the screen.
    /// </summary>
    /// <param name="useWorkArea">If true, the window will be centered within the work area of the monitor, otherwise it will be centered within the monitor's entire visible area.</param>
    public void SetWindowToCenter(bool useWorkArea = true)
    {
        unsafe
        {
            var monitor = User32.MonitorFromWindow(Handle, User32.MonitorFlag.MONITOR_DEFAULTTONEAREST);
            var lpmi = new User32.MonitorInfo
            {
                Size = (uint)sizeof(User32.MonitorInfo)
            };
            User32.GetMonitorInfo(monitor, ref lpmi);
            var screenRect = (useWorkArea ? lpmi.WorkRect : lpmi.MonitorRect);

            var midX = screenRect.Width / 2;
            var midY = screenRect.Height / 2;
            var size = Size;
            SetPosition(midX - size.Width / 2, midY - size.Height / 2);
        }
    }

    /// <summary>
    /// Destroys the window.
    /// </summary>
    public void Destroy()
    {
        User32.DestroyWindow(Handle);
        Dispose();
    }

    /// <summary>
    /// Converts the Window instance to an IntPtr, returning the handle of the window.
    /// </summary>
    /// <param name="w">The Window instance to convert.</param>
    /// <returns>Returns the handle of the window as an IntPtr.</returns>
    public static implicit operator IntPtr(Window w) => w.Handle;

    private delegate IntPtr OnTickR(uint msg, IntPtr wParam, IntPtr lParam);

    private delegate void OnTickA(IntPtr wParam, IntPtr lParam);

    private delegate IntPtr OnTick(User32.WM code, IntPtr wParam, IntPtr lParam);

    private delegate void OnTickReadOnly(User32.WM code, IntPtr wParam, IntPtr lParam);
}