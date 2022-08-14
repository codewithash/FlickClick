// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlickrImageSearchException.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick.Interfaces.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="FlickrImageSearchException" />.
    /// </summary>
    [Serializable]
    public class FlickrImageSearchException : FlickrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrImageSearchException"/> class.
        /// </summary>
        public FlickrImageSearchException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrImageSearchException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public FlickrImageSearchException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrImageSearchException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="inner">The inner<see cref="Exception"/>.</param>
        public FlickrImageSearchException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrImageSearchException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected FlickrImageSearchException(
           SerializationInfo info,
           StreamingContext context) : base(info, context)
        {
        }
    }
}
