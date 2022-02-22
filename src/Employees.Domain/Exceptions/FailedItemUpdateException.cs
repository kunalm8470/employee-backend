using System.Runtime.Serialization;

namespace Employees.Domain.Exceptions
{
    public class FailedItemUpdateException : Exception
    {
        public FailedItemUpdateException()
        {
        }

        public FailedItemUpdateException(string? message) : base(message)
        {
        }

        public FailedItemUpdateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FailedItemUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
