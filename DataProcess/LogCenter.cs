using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace MonitorPlatform.Data
{
    public class LogCenter
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Log(Exception exp)
        {
            Logger.ErrorException(exp.Message + "\r\nStackTrace:" + exp.StackTrace, exp);
        }

        public static void Log(string des, Exception exp)
        {
            Logger.ErrorException(des + "\r\nStackTrace:" + exp.StackTrace, exp);
        }

        public static void LogMessage(string message)
        {
            Logger.Info(message);
        }

    }
}
