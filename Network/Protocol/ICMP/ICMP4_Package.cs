using System.Net;

namespace Yannick.Network.Protocol.ICMP;

public sealed partial class ICMP4
{
    /// <summary>
    /// Represents an ICMPv4 packet.
    /// </summary>
    public sealed record Package
    {
        /// <summary>
        /// Gets a value indicating whether the checksum of the ICMPv4 packet is valid.
        /// </summary>
        public readonly bool IsValid;

        // Private constructor used internally.
        private Package()
        {
            IsValid = ChecksumCalculation() == Header.Checksum;
            Data = Array.Empty<byte>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class using the raw data.
        /// </summary>
        /// <param name="rawData">The raw data representing the ICMPv4 packet.</param>
        public Package(byte[] rawData)
        {
            if (rawData.Length < 8)
                throw new ArgumentException("Invalid ICMP packet length.");

            var type = rawData[0];
            var code = rawData[1];
            var checksum = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(rawData, 2));
            var identifier = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(rawData, 4));
            var sequenceNumber = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(rawData, 6));

            var dataSize = rawData.Length - 8;
            byte[] data;

            if (dataSize > 0)
            {
                data = new byte[dataSize];
                Array.Copy(rawData, 8, data, 0, data.Length);
            }
            else
                data = Array.Empty<byte>();

            Header = new Header(type, code, checksum, identifier, sequenceNumber);
            Data = data;
            if (type == 0)
                IsValid = true;
            else
                IsValid = ChecksumCalculation() == Header.Checksum;
        }

        /// <summary>
        /// Gets the header of the ICMPv4 packet.
        /// </summary>
        public Header Header { get; init; }

        /// <summary>
        /// Gets the data payload of the ICMPv4 packet.
        /// </summary>
        public byte[] Data { get; init; }

        // Calculates the checksum for this packet.
        private ushort ChecksumCalculation()
            => ChecksumCalculation(Header.RawType, Header.RawCode, Header.Identifier, Header.SequenceNumber, Data);

        /// <summary>
        /// Calculates the checksum of the specified data.
        /// </summary>
        /// <param name="type">The message type.</param>
        /// <param name="code">The code associated with the message type.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="data">The data payload.</param>
        /// <returns>The calculated checksum.</returns>
        internal static ushort ChecksumCalculation(byte type, byte code, ushort identifier, ushort sequenceNumber,
            byte[] data)
        {
            if (data == null || data.Length == 0)
                return 0;

            var totalLength = data.Length + 8;
            var isOddLength = totalLength % 2 != 0;
            if (isOddLength) totalLength++;

            var buffer = new ushort[totalLength / 2];

            buffer[0] = (ushort)((type << 8) | code);
            buffer[1] = 0;
            buffer[2] = identifier;
            buffer[3] = sequenceNumber;

            if (data.Length > 0)
            {
                for (var i = 0; i < data.Length / 2; i++)
                    buffer[4 + i] = (ushort)((data[i * 2] << 8) | data[i * 2 + 1]);

                if (isOddLength)
                    buffer[totalLength / 2 - 1] = (ushort)(data[^1] << 8);
            }

            long sum = 0;
            for (var i = 0; i < buffer.Length; i++)
                sum += buffer[i];

            while ((sum >> 16) != 0)
                sum = (sum & 0xFFFF) + (sum >> 16);

            return (ushort)~sum;
        }

        /// <summary>
        /// Creates a new <see cref="Package"/> instance using the specified parameters.
        /// </summary>
        /// <param name="type">The message type.</param>
        /// <param name="code">The code associated with the message type.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="data">The data payload.</param>
        /// <returns>A new <see cref="Package"/> instance.</returns>
        public static Package Create(byte type, byte code, ushort identifier, ushort sequenceNumber, byte[] data)
        {
            return new Package
            {
                Header = new Header(type, code, identifier, sequenceNumber, data),
                Data = data
            };
        }

        /// <summary>
        /// Creates a new <see cref="Package"/> instance using the specified parameters.
        /// </summary>
        /// <param name="type">The message type.</param>
        /// <param name="code">The code associated with the message type.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="data">The data payload.</param>
        /// <returns>A new <see cref="Package"/> instance.</returns>
        public static Package Create(MessageType type, Code code, ushort identifier, ushort sequenceNumber, byte[] data)
            => Create((byte)type, (byte)code, identifier, sequenceNumber, data);
    }
}