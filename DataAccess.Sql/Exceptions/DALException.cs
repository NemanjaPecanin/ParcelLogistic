using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions
{

    public class DALException : Exception
    {
        public DALException()
        {

        }

        public DALException(string message) : base(message)
        {

        }

        public DALException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
