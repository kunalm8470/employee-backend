using System.Runtime.Serialization;

namespace Employees.Domain.Exceptions
{
    public class InvalidPaginationParametersException : Exception
    {
        public InvalidPaginationParametersException()
        {
        }

        public InvalidPaginationParametersException(string? message) : base(message)
        {
        }

        public InvalidPaginationParametersException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidPaginationParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
