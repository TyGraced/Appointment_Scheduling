﻿using NLog;

namespace Appointment_Scheduling.API.LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
            
        }

        public void LogDebug(string message) => _logger.Debug(message);
        public void LogError(string message) => _logger.Error(message);

        public void LogInfo(string message) => _logger.Info(message);

        public void LogWarning(string message) => _logger.Warn(message);
    }
}
