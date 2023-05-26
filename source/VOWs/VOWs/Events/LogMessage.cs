using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.Events
{
    public class LogMessage
    {
        public string Message;
        public string SendingClassDescripter;
        public string LogLevel;

        public LogMessage(string message)
        {
            Message = message;
            SendingClassDescripter = "null";
            LogLevel = "INFO";
        }

        public LogMessage(string message, string sendingClassDescripter, string logLevel) : this(message)
        {
            SendingClassDescripter = sendingClassDescripter;
            LogLevel = logLevel;
        }
    }
}
