using Infrastructure.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations.UnitTest
{
    [TestClass]
    public class SeriLogApplicationLoggerTest
    {
        private Mock<Serilog.ILogger> logger;

        [TestInitialize]
        public void Setup()
        {
            this.logger = new Mock<Serilog.ILogger>();
        }
        [TestMethod]
        public void ProvidedMessageWithDebugLogType_Log_ShouldLogTheSame()
        {
          
            SeriLogApplicationLogger applicationLogger = new SeriLogApplicationLogger(logger.Object);
            applicationLogger.Log(LogType.Debug, "Debug");
            logger.Verify(x => x.Debug("Debug"), Times.Once);
        }

        [TestMethod]
        public void ProvidedMessageWithInfoLogType_Log_ShouldLogTheSame()
        {
           
            SeriLogApplicationLogger applicationLogger = new SeriLogApplicationLogger(logger.Object);
            applicationLogger.Log(LogType.Info, "Info");
            logger.Verify(x => x.Information("Info"), Times.Once);
        }

        [TestMethod]
        public void ProvidedMessageWithWarningLogType_Log_ShouldLogTheSame()
        {

            SeriLogApplicationLogger applicationLogger = new SeriLogApplicationLogger(logger.Object);
            applicationLogger.Log(LogType.Warning, "Warning");
            logger.Verify(x => x.Warning("Warning"), Times.Once);
        }

        [TestMethod]
        public void ProvidedMessageWithErrorLogType_Log_ShouldLogTheSame()
        {

            SeriLogApplicationLogger applicationLogger = new SeriLogApplicationLogger(logger.Object);
            applicationLogger.Log(LogType.Error, "Error");
            logger.Verify(x => x.Error("Error"), Times.Once);
        }

        [TestMethod]
        public void ProvidedMessageWithCriticalLogType_Log_ShouldLogTheSame()
        {

            SeriLogApplicationLogger applicationLogger = new SeriLogApplicationLogger(logger.Object);
            applicationLogger.Log(LogType.Critical, "Critical");
            logger.Verify(x => x.Fatal("Critical"), Times.Once);
        }

    }
}
