using System;

namespace PsnLib.Exceptions
{
    public class RefreshTokenException : Exception
    {
        public RefreshTokenException()
        {
        }

        public RefreshTokenException(string message)
            : base(message)
        {
        }
    }
}
