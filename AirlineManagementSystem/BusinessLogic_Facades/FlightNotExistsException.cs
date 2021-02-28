using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    [Serializable]
    internal class FlightNotExistsException : Exception
    {
        public FlightNotExistsException()
        {
        }

        public FlightNotExistsException(string message) : base(message)
        {
        }

        public FlightNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}