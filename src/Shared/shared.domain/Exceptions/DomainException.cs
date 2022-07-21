namespace Shared.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException()
        {
        }

        //public DomainException(string message)
        //    : base(message)
        //{
        //}

        public abstract override string Message { get; }

        /// <summary>
        /// This is an error string that could be localized in the front end.
        /// </summary>
        public abstract string ErrorCode { get; }

        //public DomainException(string message, Exception inner)
        //    : base(message, inner)
        //{
        //}
    }
}
