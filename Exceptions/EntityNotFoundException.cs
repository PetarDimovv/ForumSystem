using System;

namespace StackOverflow.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) 
            : base(message)
        {

        }
    }
}
