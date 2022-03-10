using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.domain.Expceptions
{
    public abstract class DomainException: Exception
    {
        public DomainException()
        {
        }

        //public DomainException(string message)
        //    : base(message)
        //{
        //}

        public abstract override string Message { get; }

        public abstract string ErrorCode { get; }

        //public DomainException(string message, Exception inner)
        //    : base(message, inner)
        //{
        //}
    }
}
