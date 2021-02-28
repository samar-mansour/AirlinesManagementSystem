using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    [Serializable]
    internal class AdminLevelNotQualifiedToCreateException : Exception
    {
        public AdminLevelNotQualifiedToCreateException()
        {
        }

        public AdminLevelNotQualifiedToCreateException(string message) : base(message)
        {
        }

        public AdminLevelNotQualifiedToCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminLevelNotQualifiedToCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}