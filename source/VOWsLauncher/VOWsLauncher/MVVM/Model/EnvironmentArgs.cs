using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Linq;
using VOWsLauncher.Events;

namespace VOWsLauncher.MVVM.Model
{
    /// <summary>
    /// The <c>EnvironmentArgs</c> class contains information on the command line arguments that were used to launch VOWsuite.
    /// This allows for dynamic commands to be executed.
    /// </summary>
    public class EnvironmentArgs
    {
        /// <summary>
        /// The <c>environmentInput</c> private parameter stores the raw commandline input.
        /// </summary>
        private readonly string[] environmentInput;

        /// <summary>
        /// The <c>Debug</c> parameter is a flag for other areas of the application to show Debug
        /// values while enabled.
        /// </summary>
        public bool Debug { get; private set; }

        /// <summary>
        /// The constructor for <c>EnvironmentArgs</c> will populate variable data by retrieving and converting 
        /// the Application's commandline arguments.
        /// </summary>
        public EnvironmentArgs()
        {
            // Retrieve command line arguments, except for the first argument which contains the executable file name.
            environmentInput = Environment.GetCommandLineArgs().Skip(1).ToArray();
            // For debug purposes, we'll print the array to the logs file.
            WeakReferenceMessenger.Default.Send(new LogMessage("EnvironmentArgs has retrieved command line arguments: " + environmentInput, ToString(), LogLevel.DEBUG));
            // Begin iteration to set variables.
            int i = 0;
            while (i < environmentInput.Length)
            {
                string extension = environmentInput[i];
                if (!extension.StartsWith("-"))
                {
                    // This isn't a valid extension. We should ignore it.
                    WeakReferenceMessenger.Default.Send(new LogMessage("Ignored extension '" + extension + "' due to invalid format (extensions must start with '-').", ToString(), LogLevel.WARNING));
                    i++;
                    continue;
                }
                // The extension is in a valid format. From there, we should iterate to the next value and try to retrieve a value.
                int j = i + 1;
                string value = "";
                while (j < environmentInput.Length)
                {
                    // Add the value to the 'value' string.
                    value += environmentInput[j];
                    // Check if it starts with a quotation mark ("). If it doesn't, this is a value with no spaces, thus no action is needed.
                    if (!value.StartsWith('"'))
                    {
                        // It doesn't start with a quotation mark, so we'll just exit the loop.
                        break;
                    }
                    // The value contains spaces, thus must use quotation marks to specify the start and end of this value.
                    // We'll check if the currently retrieved value ends in a quotation mark.
                    if (value.EndsWith('"'))
                    {
                        // The quotation mark at the end of the 'value' string indicates we've collected the entire string that represents the value.
                        // Now, we'll remove the first and last characters (which we know are quotation marks) to get our true value string.
                        value = value.Remove(0).Remove(value.Length - 1);
                        break;
                    }
                    // Nothing yet, we'll continue running the loop.
                    j++;
                }
                // We've (probably) found the value. First we'll use the TrySet method to see if the extension / key and value equate to any of the settings in this file.
                TrySet(extension, value, out bool success);
                // If this set was unsuccessful, we'll submit a log message.
                if (!success) WeakReferenceMessenger.Default.Send(new LogMessage("Ignored extension '" + extension + "' due to invalid value format (value '" + value + "' could not be converted to the required type).", ToString(), LogLevel.WARNING));
                else WeakReferenceMessenger.Default.Send(new LogMessage("Extension '" + extension + "' with value '" + value + "' was recognised and read.", ToString(), LogLevel.DEBUG));
                // We'll set i to the value of j (the index of the final part of the value) plus one to continue iterating the loop.
                i = j + 1;
            }
        }

        /// <summary>
        /// The <c>TrySet</c> method will attempt to set the value of a variable based off a key and list of
        /// values from the commandline. If it is successful, the variable will be set, and 'success' will be
        /// returned.
        /// </summary>
        /// <param name="key">The key of the variable to set, including the '-' and as case-insensitive.</param>
        /// <param name="value">The value of the variable to set.</param>
        /// <param name="success">Whether the variable was successfully set or not.</param>
        private void TrySet(string key, string value, out bool success)
        {
            switch (key.ToLower())
            {
                case "-debug":
                    // Try to convert value to a bool.
                    success = bool.TryParse(value, out bool validBool);
                    if (success) Debug = validBool;
                    return;
            }
            success = false;
        }
    }
}
