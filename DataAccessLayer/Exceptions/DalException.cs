using System;

namespace DataAccessLayer.Exceptions
{
    public class DalException : Exception
    {
        public DalException(string message) : base(message) 
        { }
    }
}
