using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem
{
    [Serializable]
    internal class WorngCredentialsException : Exception
    {
        public WorngCredentialsException()
        {
        }

        public WorngCredentialsException(string message) : base(message)
        {
        }

        public WorngCredentialsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorngCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}