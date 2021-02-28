using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    [Serializable]
    internal class FlightAlreadyExistsException : Exception
    {
        public FlightAlreadyExistsException()
        {
        }

        public FlightAlreadyExistsException(string message) : base(message)
        {
        }

        public FlightAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}