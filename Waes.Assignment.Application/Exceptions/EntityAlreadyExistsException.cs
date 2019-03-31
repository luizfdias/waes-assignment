using System;

namespace Waes.Assignment.Application.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an entity already exists 
    /// </summary>
    public class EntityAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EntityAlreadyExistsException"/>
        /// </summary>
        public EntityAlreadyExistsException()
        {            
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EntityAlreadyExistsException"/> with a message
        /// </summary>
        public EntityAlreadyExistsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EntityAlreadyExistsException"/> with a message and an inner exception
        /// </summary>
        public EntityAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
