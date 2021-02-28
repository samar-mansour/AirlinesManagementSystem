using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    [Serializable]
    internal class AdminAlreadyExistsException : Exception
    {
        public AdminAlreadyExistsException()
        {
        }

        public AdminAlreadyExistsException(string message) : base(message)
        {
        }

        public AdminAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}