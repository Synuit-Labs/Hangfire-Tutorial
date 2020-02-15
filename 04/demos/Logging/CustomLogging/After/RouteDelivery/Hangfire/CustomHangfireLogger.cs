using Hangfire.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RouteDelivery.Hangfire
{
    public class CustomHangfireLogger : ILog
    {
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
        {
            if (messageFunc == null)
            {
                return true;
            }

            var text = messageFunc();

            switch (logLevel)
            {
                case LogLevel.Trace:
                    Debug.Print($"Trace: {text}");
                    break;
                case LogLevel.Debug:
                    Debug.Print($"Debug: {text}");
                    break;
                case LogLevel.Info:
                    Debug.Print($"Info: {text}");
                    break;
                case LogLevel.Warn:
                    Debug.Print($"Warn: {text}");
                    break;
                case LogLevel.Error:
                    Debug.Print($"Error: {text} ex: {exception.Message}");
                    break;
                case LogLevel.Fatal:
                    Debug.Print($"Fatal: {text}");
                    break;
                default:
                    break;
            }

            return true;
        }
    }
}