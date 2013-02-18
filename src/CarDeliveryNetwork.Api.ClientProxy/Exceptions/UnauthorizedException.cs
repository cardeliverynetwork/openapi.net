using System;

namespace CarDeliveryNetwork.Api.ClientProxy.Exceptions
{
    /// <summary>
    /// Represents errors due to anauthorized calls to the API
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.Exceptions.UnauthorizedException"/> class.
        /// </summary>
        public UnauthorizedException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.Exceptions.UnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnauthorizedException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.Exceptions.UnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
