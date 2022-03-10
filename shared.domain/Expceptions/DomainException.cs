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

        public DomainException(string message)
            : base(message)
        {
        }

        public abstract string ErrorCode();

        //public DomainException(string message, Exception inner)
        //    : base(message, inner)
        //{
        //}
    }
}
