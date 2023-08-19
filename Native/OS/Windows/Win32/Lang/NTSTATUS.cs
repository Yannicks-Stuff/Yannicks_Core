using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32.Lang
{
    [StructLayout(LayoutKind.Explicit)]
    public partial struct NTSTATUS : IEquatable<NTSTATUS>
    {
        public bool Equals(NTSTATUS other)
        {
            return UnderlyingType == other.UnderlyingType && Status == other.Status;
        }

        public override bool Equals(object? obj)
        {
            return obj is NTSTATUS other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UnderlyingType, (int)Status);
        }

        [FieldOffset(0)] internal uint UnderlyingType;
        [FieldOffset(0)] public readonly Code Status;

        public NTSTATUS(uint c) : this()
        {
            UnderlyingType = c;
            Status = (Code)c;
        }


        public static implicit operator bool(NTSTATUS a) => a.UnderlyingType == 0;
        public static bool operator ==(Code a, NTSTATUS b) => a != (Code)b.UnderlyingType;
        public static bool operator !=(Code a, NTSTATUS b) => a != (Code)b.UnderlyingType;
        public static bool operator ==(NTSTATUS a, Code b) => (Code)a.UnderlyingType != b;
        public static bool operator !=(NTSTATUS a, Code b) => (Code)a.UnderlyingType != b;
        public static implicit operator long(NTSTATUS a) => a.UnderlyingType;
        public static implicit operator Code(NTSTATUS a) => (Code)a.UnderlyingType;
        public static implicit operator NTSTATUS(uint a) => new(a);
        public static implicit operator NTSTATUS(int a) => new(Convert.ToUInt32(a));
    }
}