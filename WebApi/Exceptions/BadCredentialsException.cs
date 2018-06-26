using System;

namespace WebApi.Exceptions
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