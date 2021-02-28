using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    [Serializable]
    internal class NoFlightsException : Exception
    {
        public NoFlightsException()
        {
        }

        public NoFlightsException(string message) : base(message)
        {
        }

        public NoFlightsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoFlightsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}