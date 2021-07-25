using FluentValidation.Results;
using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    internal class ValidatonException : Exception
    {
        private ValidationResult validationResult;

        public ValidatonException()
        {
        }

        public ValidatonException(ValidationResult validationResult)
        {
            this.validationResult = validationResult;
        }

        public ValidatonException(string message) : base(message)
        {
        }

        public ValidatonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidatonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}