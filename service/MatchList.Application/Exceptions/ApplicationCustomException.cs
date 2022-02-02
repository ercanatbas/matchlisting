using System;

namespace MatchList.Application.Exceptions
{
    public class ApplicationCustomException : Exception
    {
        public ApplicationCustomException()
        { }

        public ApplicationCustomException(string message)
            : base(message)
        { }

        public ApplicationCustomException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
