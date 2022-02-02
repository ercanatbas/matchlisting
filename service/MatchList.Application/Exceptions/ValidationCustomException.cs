using System;

namespace MatchList.Application.Exceptions
{
    public class ValidationCustomException : Exception
    {
        public ValidationCustomException()
        { }

        public ValidationCustomException(string message)
            : base(message)
        { }

        public ValidationCustomException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
