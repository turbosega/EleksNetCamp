using System;

namespace BusinessLogicLayer.Exceptions
{
    public class NameOfResourceIsTakenException : Exception
    {
        public NameOfResourceIsTakenException() : base()
        {

        }

        public NameOfResourceIsTakenException(string message) : base(message)
        {

        }
    }
}