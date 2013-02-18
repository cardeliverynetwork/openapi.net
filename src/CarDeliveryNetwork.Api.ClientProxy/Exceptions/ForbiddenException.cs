using System;

namespace CarDeliveryNetwork.Api.ClientProxy.Exceptions
{
    /// <summary>
    /// Represents errors due to forbidden calls to the API
    /// </summary>
    public class ForbiddenException : Exception
    {
      /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.Exceptions.ForbiddenException"/> class.
        /// </summary>
        public ForbiddenException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.Exceptions.ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ForbiddenException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.Exceptions.ForbiddenException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }
    }
}
