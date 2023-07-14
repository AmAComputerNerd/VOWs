using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.IO;
using VOWsLauncher.Events;

namespace VOWsLauncher.MVVM.Model
{
    /// <summary>
    /// The <c>Logger</c> class handles all logging actions in the application.
    /// It can be written to with a <c>LogMessage</c> message, or through direct references to the object with the various utility methods.
    /// </summary>
    public class Logger : ObservableRecipient
    {
        /// <summary>
        /// The <c>LogUri</c> parameter represents the location of the log file in the system.
        /// It should be used in conjunction with the <c>LogFileInfo</c> to save the stream to the system,
        /// when required.
        /// </summary>
        public Uri LogUri;
        /// <summary>
        /// The <c>LogFileInfo</c> parameter represents the up-to-date info of the log file in the system.
        /// It is automatically updated whenever the <c>LogStream</c> is saved.
        /// </summary>
        public FileInfo LogFileInfo;

        /// <summary>
        /// The constructor for <c>Logger</c> will create a new instance that will log all received messages
        /// to a file, called <c>launcher.txt</c>, within the <c>log/</c> directory. If a file exists, it will be
        /// be renamed to avoid conflict.
        /// </summary>
        private Logger(Uri LogUri, FileInfo LogFileInfo)
        {
            // Activate Logger to receive messages.
            IsActive = true;
            // Initialise variables
            this.LogUri = LogUri;
            this.LogFileInfo = LogFileInfo;
        }

        /// <summary>
        /// The <c>Debug</c> method writes a new Debug log message to the log file.
        /// <c>Debug</c> represents a message that is added for flow management / development purposes; more for the developer than the user.
        /// </summary>
        /// <param name="message">The message to append to the Debug log.</param>
        public void Debug(string message)
        {
            Write("DEBUG: " + message);
        }

        /// <summary>
        /// The <c>Info</c> method writes a new Info log message to the log file.
        /// <c>Info</c> represents a message for any successful operation, e.g. a file pull.
        /// </summary>
        /// <param name="message">The message to append to the Info log.</param>
        public void Info(string message)
        {
            Write("INFO: " + message);
        }

        /// <summary>
        /// The <c>Warn</c> method writes a new Warning log message to the log file.
        /// <c>Warning</c> represents a message for an unsuccessful operation that shouldn't interupt the flow of
        /// the application - it is a guide message in a way.
        /// </summary>
        /// <param name="message">The message to append to the Warning log.</param>
        public void Warn(string message)
        {
            Write("WARN: " + message);
        }

        /// <summary>
        /// The <c>Error</c> method writes a new Error log message to the log file.
        /// <c>Error</c> represents a message for an unsuccessful operation that DOES interupt the flow of the
        /// application. It should be considered a bug most of the time.
        /// </summary>
        /// <param name="message">The message to append to the Error log.</param>
        public void Error(string message)
        {
            Write("ERROR: " + message);
        }

        /// <summary>
        /// The <c>Severe</c> method writes a new Severe log message to the log file.
        /// <c>Severe</c> represents a message for an unsuccessful operation that can't be recovered from.
        /// It is expected behaviour that the application or a subprocess terminates after this error is logged,
        /// but why the error was logged in the first place should be identified.
        /// </summary>
        /// <param name="message">The message to append to the Severe log.</param>
        public void Severe(string message)
        {
            Write("SEVERE: " + message);
        }

        /// <summary>
        /// The private method <c>Write</c> writes the specified message to the log file.
        /// </summary>
        /// <param name="message"></param>
        private void Write(string message)
        {
            // Write the message.
            using(StreamWriter w = LogFileInfo.AppendText())
            {
                w.WriteLine(DateTime.Now.ToString("(HH:mm:ss) ") + message);
            }
            // Refresh the FileInfo object.
            LogFileInfo.Refresh();
        }

        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messages from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            Messenger.Register<Logger, LogMessage>(this, (r, m) => r.Receive(m));
        }

        /// <summary>
        /// The <c>Receive</c> method will be called whenever the <c>LogMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event that was sent, with data.</param>
        private void Receive(LogMessage message)
        {
            string sentMessage;
            if (message != null && message.Message != null)
            {
                sentMessage = message.LogLevel + ": " + message.Message;
                if (message.SendingClassDescriptor != null) sentMessage += " (" + message.SendingClassDescriptor + ")";
                Write(sentMessage);
            }
        }

        /// <summary>
        /// The <c>New</c> static method creates a new Logger instance that will log all received messages
        /// to a file, called <c>launcher.txt</c>, within the <c>log/</c> directory. If a file exists, it will be
        /// be renamed to avoid conflict.
        /// </summary>
        /// <returns>A new <c>Logger</c>, or null if files cannot be resolved.</returns>
        public static Logger? New()
        {
            try
            {
                // Define the directory path, and create the directory if it doesn't exist.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string logDirectoryPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "/logs";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (!Directory.Exists(logDirectoryPath))
                {
                    Directory.CreateDirectory(logDirectoryPath);
                }
                // Define the path to the log file, and copy any existing one to another name.
                string logPath = logDirectoryPath + "/launcher.txt";
                if (File.Exists(logPath) && (new FileInfo(logPath)).Length != 0)
                {
                    File.Copy(logPath, logDirectoryPath + "/launcher-" + DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") + ".txt");
                }
                File.WriteAllText(logPath, string.Empty);
                // Return a Logger.
                return new(new(logPath), new(logPath));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
