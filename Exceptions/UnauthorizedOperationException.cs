using System;

namespace StackOverflow.Exceptions
{
    public class UnauthorizedOperationException : ApplicationException 
    {
        public UnauthorizedOperationException(string message)
            : base(message)
        {

        }
    }
}
