namespace Yannick.Crypto.Compression.bzip2O
{
    internal class BZip2Exception : Exception
    {
        /// <summary>
        /// Initialise a new instance of <see cref="BZip2Exception" />.
        /// </summary>
        public BZip2Exception()
        {
        }

        /// <summary>
        /// Initialise a new instance of <see cref="BZip2Exception" /> with its message string.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the error.</param>
        public BZip2Exception(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialise a new instance of <see cref="BZip2Exception" />.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the error.</param>
        /// <param name="innerException">The <see cref="Exception"/> that caused this exception.</param>
        public BZip2Exception(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}