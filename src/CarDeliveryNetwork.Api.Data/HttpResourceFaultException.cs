using System;
using System.Net;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Represents errors that occur on calls to the API
    /// </summary>
    public class HttpResourceFaultException : Exception
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResourceFaultException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public HttpResourceFaultException(HttpStatusCode statusCode)
            : this(statusCode, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResourceFaultException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        public HttpResourceFaultException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResourceFaultException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public HttpResourceFaultException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
