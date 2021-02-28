using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    [Serializable]
    internal class NegativeIdException : Exception
    {
        public NegativeIdException()
        {
        }

        public NegativeIdException(string message) : base(message)
        {
        }

        public NegativeIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NegativeIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}