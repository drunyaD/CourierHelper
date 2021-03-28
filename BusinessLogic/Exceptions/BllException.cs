using System;

namespace BusinessLogic.Exceptions
{
    public class BllException : Exception
    {
        public BllException(string message) : base(message)
        { }
    }
}
