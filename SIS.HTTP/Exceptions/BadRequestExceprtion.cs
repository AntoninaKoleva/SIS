using System;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestExceprtion : Exception
    {
        public const string BadRequestExceptionDefaultMessage = 
            "The Request was malformed or contains unsupported elements.";
        public BadRequestExceprtion() 
            : this(BadRequestExceptionDefaultMessage)
        {

        }
        public BadRequestExceprtion(string name) : base(name)
        {

        }
    }
}
