namespace WebApi.Exceptions
{
    public class UserNotFoundException : ResourceNotFoundException
    {
        public UserNotFoundException() : base()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}