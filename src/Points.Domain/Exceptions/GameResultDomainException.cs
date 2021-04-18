using System;
using System.Collections.Generic;
using System.Text;

namespace Points.Domain.Exceptions
{
    public class GameResultDomainException : Exception
    {
        public GameResultDomainException() { }

        public GameResultDomainException(string message): base(message) { }

        public GameResultDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
