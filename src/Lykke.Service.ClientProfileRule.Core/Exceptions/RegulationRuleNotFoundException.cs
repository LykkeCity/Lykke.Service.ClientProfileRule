using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lykke.Service.ClientProfileRule.Core.Exceptions
{ 
    /// <summary>
    /// The exception that is thrown when requested regulation rule cannot be found.
    /// </summary>
    [Serializable]
    public class RegulationRuleNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegulationRuleNotFoundException"/> class.
        /// </summary>
        public RegulationRuleNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegulationRuleNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected RegulationRuleNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegulationRuleNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RegulationRuleNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegulationRuleNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<c>Nothing</c> in Visual Basic) if no inner exception is specified.</param>
        public RegulationRuleNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets or sets a regulation id.
        /// </summary>
        public string RegulationId { get; set; }
    }
}
