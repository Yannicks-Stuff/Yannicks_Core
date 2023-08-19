namespace Yannick.Native.OS.Windows.Win32.Lang
{
    /// <content>
    /// The <see cref="SeverityCode"/> nested type.
    /// </content>
    public partial struct HResult
    {
        /// <summary>
        /// HRESULT severity codes
        /// </summary>
        public enum SeverityCode : uint
        {
            Success = 0,
            Fail = 0x80000000
        }
    }
}