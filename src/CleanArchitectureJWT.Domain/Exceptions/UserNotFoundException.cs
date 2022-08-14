namespace CleanArchitectureJWT.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User can't be found.")
        {
        }
    }
}
