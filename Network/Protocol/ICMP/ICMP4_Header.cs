using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Yannick.Network.Protocol.ICMP;

public sealed partial class ICMP4
{
    /// <summary>
    /// Represents the different codes associated with each ICMPv4 message type.
    /// </summary>
    public enum Code : byte
    {
        Unknown = 0,
        NoCode = 0,

        NetOrDestinatioUnreachable = 0,
        HostUnreachable = 1,
        ProtocolUnreachable = 2,
        PortUnreachable = 3,
        FragmentationRequiredAndDFBitSet = 4,
        SourceRouteFailed = 5,
        DestinationNetworkUnknown = 6,
        DestinationHostUnknown = 7,
        DestinationProtocolUnknown = 8,
        DestinationPortUnreachable = 9,
        AddressIncomplete = 10,

        RedirectDatagramForNetwork = 0,
        RedirectDatagramForHost = 1,
        RedirectDatagramForTOSAndNetwork = 2,
        RedirectDatagramForTOSAndHost = 3,

        TTLExpiredInTransit = 0,
        FragmentReassemblyTimeExceeded = 1,

        IPHeaderChecksumError = 0,
        IPPacketDatagramTooShort = 1,
    }

    /// <summary>
    /// Represents the different types of ICMPv4 messages.
    /// </summary>
    public enum MessageType : byte
    {
        EchoReply = 0,
        EchoRequest = 8,
        DestinationUnreachable = 3,
        SourceQuench = 4,
        Redirect = 5,
        TimeExceeded = 11,
        ParameterProblem = 12,
        TimestampRequest = 13,
        TimestampReply = 14,
        InfoRequest = 15,
        InfoReply = 16,
        AddressMaskRequest = 17,
        AddressMaskReply = 18
    }

    /// <summary>
    /// Represents the header of an ICMPv4 packet.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct Header
    {
        /// <summary>
        /// The size of the header in bytes.
        /// </summary>
        public const byte Size = 8;

        [FieldOffset(0)] public readonly byte RawType;

        /// <summary>
        /// Gets the message type of the ICMPv4 packet.
        /// </summary>
        public MessageType Type => (MessageType)RawType;

        [FieldOffset(1)] public readonly byte RawCode;

        /// <summary>
        /// Gets the code associated with the message type of the ICMPv4 packet.
        /// </summary>
        public Code Code
        {
            get
            {
                switch (Type)
                {
                    case MessageType.DestinationUnreachable:
                        switch (RawCode)
                        {
                            case 0:
                                return Code.NetOrDestinatioUnreachable;
                            case 1:
                                return Code.HostUnreachable;
                            case 2:
                                return Code.ProtocolUnreachable;
                            case 3:
                                return Code.PortUnreachable;
                            case 4:
                                return Code.FragmentationRequiredAndDFBitSet;
                            case 5:
                                return Code.SourceRouteFailed;
                            case 6:
                                return Code.DestinationNetworkUnknown;
                            case 7:
                                return Code.DestinationHostUnknown;
                            case 8:
                                return Code.DestinationProtocolUnknown;
                            case 9:
                                return Code.DestinationPortUnreachable;
                            case 10:
                                return Code.AddressIncomplete;
                        }

                        break;
                    case MessageType.Redirect:
                        switch (RawCode)
                        {
                            case 0:
                                return Code.RedirectDatagramForNetwork;
                            case 1:
                                return Code.RedirectDatagramForHost;
                            case 2:
                                return Code.RedirectDatagramForTOSAndNetwork;
                            case 3:
                                return Code.RedirectDatagramForTOSAndHost;
                        }

                        break;
                    case MessageType.TimeExceeded:
                        switch (RawCode)
                        {
                            case 0:
                                return Code.TTLExpiredInTransit;
                            case 1:
                                return Code.FragmentReassemblyTimeExceeded;
                        }

                        break;
                    case MessageType.ParameterProblem:
                        switch (RawCode)
                        {
                            case 0:
                                return Code.IPHeaderChecksumError;
                            case 1:
                                return Code.IPPacketDatagramTooShort;
                        }

                        break;
                }

                return Code.Unknown;
            }
        }

        [FieldOffset(2)] public readonly ushort Checksum;
        [FieldOffset(3)] public readonly ushort Identifier;
        [FieldOffset(4)] public readonly ushort SequenceNumber;

        /// <summary>
        /// Initializes a new instance of the Header struct with the specified values.
        /// </summary>
        public Header(byte rawType, byte rawCode, ushort checksum, ushort identifier, ushort sequenceNumber)
        {
            RawType = rawType;
            RawCode = rawCode;
            Checksum = checksum;
            Identifier = identifier;
            SequenceNumber = sequenceNumber;
        }

        /// <summary>
        /// Initializes a new instance of the Header struct with the specified values and calculates the checksum based on the provided data.
        /// </summary>
        public Header(byte rawType, byte rawCode, ushort identifier, ushort sequenceNumber, byte[] data)
        {
            RawType = rawType;
            RawCode = rawCode;
            Checksum = Package.ChecksumCalculation(rawType, rawCode, identifier, sequenceNumber, data);
            Identifier = identifier;
            SequenceNumber = sequenceNumber;
        }
    }
}