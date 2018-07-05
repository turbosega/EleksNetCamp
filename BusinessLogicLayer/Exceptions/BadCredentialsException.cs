using System;

namespace BusinessLogicLayer.Exceptions
{
    public class BadCredentialsException : Exception
    {
        public BadCredentialsException() : base()
        {
        }

        public BadCredentialsException(string message) : base(message)
        {
        }
    }
}