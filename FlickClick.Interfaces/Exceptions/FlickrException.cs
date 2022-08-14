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
    /// Defines the <see cref="FlickrException" />.
    /// </summary>
    [Serializable]
    public abstract class FlickrException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class.
        /// </summary>
        protected FlickrException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        protected FlickrException(string message)
           : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="inner">The inner<see cref="Exception"/>.</param>
        protected FlickrException(string message, Exception inner)
           : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected FlickrException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
