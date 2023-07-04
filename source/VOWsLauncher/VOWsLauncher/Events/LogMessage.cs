namespace VOWsLauncher.Events
{
    /// <summary>
    /// The <c>LogMessage</c> will trigger a new log message to be submitted with the current settings.
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// The <c>Message</c> parameter contains the message to be logged.
        /// </summary>
        public string Message;
        /// <summary>
        /// The <c>SendingClassDescriptor</c> parameter is optional, but should contain information on the
        /// class that sent the <c>LogMessage</c>.
        /// </summary>
        public string SendingClassDescriptor;
        /// <summary>
        /// The <c>LogLevel</c> parameter is the string that will prefix the log message.
        /// </summary>
        public string LogLevel;

        /// <summary>
        /// The constructor for <c>LogMessage</c> will create a log with a message, no value for <c>SendingClassDescriptor</c>
        /// and an "INFO" log level.
        /// </summary>
        /// <param name="message"></param>
        public LogMessage(string message)
        {
            Message = message;
            SendingClassDescriptor = "null";
            LogLevel = "INFO";
        }

        /// <summary>
        /// The constructor for <c>LogMessage</c> will create a log with a message, description of the sending class and a log level.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sendingClassDescriptor"></param>
        /// <param name="logLevel"></param>
        public LogMessage(string message, string sendingClassDescriptor, string logLevel) : this(message)
        {
            SendingClassDescriptor = sendingClassDescriptor;
            LogLevel = logLevel;
        }
    }
}
