using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Runtime.Serialization;

namespace APBDExam.Services
{
    [Serializable]
    internal class ClinicException : Exception
    {
        public ClinicException()
        {
        }

        public ClinicException(string message) : base(message)
        {
        }

        public ClinicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClinicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    
    }
}