using System;
using System.Runtime.Serialization;

namespace VacancySpy
{
    [Serializable]
    public class EmptyLocatorException : Exception
    {
        public EmptyLocatorException()
        {
        }

        public EmptyLocatorException(string message) : base(message)
        {
        }

        public EmptyLocatorException(Exception e) : this("", e)
        {
        }

        public EmptyLocatorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyLocatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
