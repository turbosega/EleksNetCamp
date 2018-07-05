using System;

namespace BusinessLogicLayer.Exceptions
{
    public class LoginIsTakenException : Exception
    {
        public LoginIsTakenException() : base()
        {
        }

        public LoginIsTakenException(string message) : base(message)
        {
        }
    }
}