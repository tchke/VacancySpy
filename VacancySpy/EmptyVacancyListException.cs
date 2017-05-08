using System;
using System.Runtime.Serialization;

namespace VacancySpy
{
    [Serializable]
    public class EmptyVacancyLocatorListException : Exception
    {
        public EmptyVacancyLocatorListException()
        {
        }

        public EmptyVacancyLocatorListException(string message) : base(message)
        {
        }

        public EmptyVacancyLocatorListException(Exception e) : this("", e)
        {
        }

        public EmptyVacancyLocatorListException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyVacancyLocatorListException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
