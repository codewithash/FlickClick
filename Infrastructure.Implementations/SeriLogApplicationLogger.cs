// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeriLogApplicationLogger.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Implementations
{
    using Infrastructure.Interfaces;

    /// <summary>
    /// Defines the <see cref="SeriLogApplicationLogger" />.
    /// </summary>
    public class SeriLogApplicationLogger : IApplicationLogger
    {
        /// <summary>
        /// Defines the log.
        /// </summary>
        private readonly Serilog.ILogger log;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriLogApplicationLogger"/> class.
        /// </summary>
        /// <param name="Log">The Log<see cref="Serilog.Core.Logger"/>.</param>
        public SeriLogApplicationLogger(Serilog.ILogger Log)
        {
            log = Log;
        }

        /// <summary>
        /// The Log.
        /// </summary>
        /// <param name="logType">The logType<see cref="LogType"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public void Log(LogType logType, string message)
        {
            switch (logType)
            {
                case LogType.Debug:
                    log.Debug(message);
                    break;
                case LogType.Info:
                    log.Information(message);
                    break;
                case LogType.Warning:
                    log.Warning(message);
                    break;
                case LogType.Error:
                    log.Error(message);
                    break;
                case LogType.Critical:
                    log.Fatal(message);
                    break;
                default:
                    log.Verbose(message);
                    break;
            }
        }
    }
}
