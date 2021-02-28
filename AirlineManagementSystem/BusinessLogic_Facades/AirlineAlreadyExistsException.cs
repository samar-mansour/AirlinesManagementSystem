﻿using System;
using System.Runtime.Serialization;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    [Serializable]
    internal class AirlineAlreadyExistsException : Exception
    {
        public AirlineAlreadyExistsException()
        {
        }

        public AirlineAlreadyExistsException(string message) : base(message)
        {
        }

        public AirlineAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirlineAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}