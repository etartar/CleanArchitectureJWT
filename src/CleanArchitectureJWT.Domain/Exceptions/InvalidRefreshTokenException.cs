﻿namespace CleanArchitectureJWT.Domain.Exceptions
{
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException() : base("Refresh token is not valid.")
        {
        }
    }
}
