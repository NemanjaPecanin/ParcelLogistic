using System;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions
{
    public class BlException : Exception
    {
        public BlException()
        {

        }

        public BlException(string message) : base(message)
        {

        }

        public BlException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
