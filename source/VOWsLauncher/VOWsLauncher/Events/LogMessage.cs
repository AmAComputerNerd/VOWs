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
        public string? SendingClassDescriptor;
        /// <summary>
        /// The <c>LogLevel</c> parameter is the string that will prefix the log message.
        /// </summary>
        public LogLevel LogLevel;

        /// <summary>
        /// The constructor for <c>LogMessage</c> will create a log with a message, no value for <c>SendingClassDescriptor</c>
        /// and an "INFO" log level.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public LogMessage(string message)
        {
            Message = message;
            SendingClassDescriptor = null;
            LogLevel = LogLevel.INFO;
        }

        /// <summary>
        /// The constructor for <c>LogMessage</c> will create a log with a message, description of the sending class and a log level.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="sendingClassDescriptor">The information about the class sending this message (typically the result of <c>ToString()</c>).</param>
        /// <param name="logLevel">The <c>LogLevel</c> for this message.</param>
        public LogMessage(string message, string? sendingClassDescriptor, LogLevel logLevel) : this(message)
        {
            SendingClassDescriptor = sendingClassDescriptor;
            LogLevel = logLevel;
        }
    }

    public enum LogLevel
    {
        /// <summary>
        /// <c>DEBUG</c> represents a message that is added for flow management / development purposes; more for the developer than the user.
        /// </summary>
        DEBUG,
        /// <summary>
        /// <c>INFO</c> represents a message for any successful operation, e.g. a file pull.
        /// </summary>
        INFO,
        /// <summary>
        /// <c>WARNING</c> represents a message for an unsuccessful operation that shouldn't interupt the flow of
        /// the application - it intends to provide additional information that may have led up to an <c>ERROR</c> or <c>SEVERE</c> message.
        /// </summary>
        WARNING,
        /// <summary>
        /// <c>ERROR</c> represents a message for an unsuccessful operation that DOES interupt the flow of the
        /// application. It should be considered a bug most of the time, and should be addressed if it comes up frequently or spontaneously.
        /// </summary>
        ERROR,
        /// <summary>
        /// <c>SEVERE</c> represents a message for an unsuccessful operation that can't be recovered from.
        /// It is expected behaviour that the application or a subprocess terminates after this type of error is logged,
        /// but why the error was logged in the first place should be identified.
        /// </summary>
        SEVERE
    }
}
