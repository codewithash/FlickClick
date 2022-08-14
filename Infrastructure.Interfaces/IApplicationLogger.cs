// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationLogger.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IApplicationLogger" />.
    /// </summary>
    public interface IApplicationLogger
    {
        /// <summary>
        /// The Log.
        /// </summary>
        /// <param name="logType">The logType<see cref="LogType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        void Log(LogType logType, string message);
    }
}
