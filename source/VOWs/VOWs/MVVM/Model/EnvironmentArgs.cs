using CommunityToolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Windows;
using VOWs.Events;

namespace VOWs.MVVM.Model
{
    public class EnvironmentArgs
    {
        /// <summary>
        /// The <c>environmentInput</c> private parameter stores the raw commandline input.
        /// </summary>
        private readonly string[] environmentInput;
        /// <summary>
        /// The <c>SourcePath</c> parameter will point to the currently open Document or Workspace.
        /// If no Document or Workspace is open, it will simply be null.
        /// </summary>
        public Uri? SourcePath { get; private set; }
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
            environmentInput = Environment.GetCommandLineArgs();
            // Iterate through environment input to define args.
            int passover = 1;
            for(int index = 1; index < environmentInput.Length-1; index += 2)
            {
                if (!environmentInput[index].StartsWith("-"))
                {
                    // Invalid input, something has gone wrong, so skip the argument and log an error message.
                    WeakReferenceMessenger.Default.Send(new LogMessage("Skipped environment variable '" + environmentInput[index] + "': Invalid format."));
                    continue;
                }
                if (environmentInput[index+1].StartsWith("\""))
                {
                    // This is a string that likely contains spaces, so we'll use the passover variable to iterate
                    // through the input list. If we don't find the value, we may as well exclude it.
                    while (index+passover < environmentInput.Length || environmentInput[index+passover].EndsWith("\""))
                    {
                        passover++;
                    }
                    if(index+passover >= environmentInput.Length)
                    {
                        // We hit the first clause, and the end of this value was never found.
                        // We'll skip over it and log an error message.
                        WeakReferenceMessenger.Default.Send(new LogMessage("Skipped environment variable '" + environmentInput[index] + "': Invalid format."));
                        continue;
                    }
                }
                // Read the key and value of the command.
                string key = environmentInput[index];
                string[] value = environmentInput[(index+1)..(index+passover)];
                // Try to set the variable with TrySet, and map the operation's success to 'success'.
                bool success;
                TrySet(key, value, out success);
                if(!success)
                {
                    // Conversion of the value wasn't successful, so we'll skip this argument and log an error message.
                    WeakReferenceMessenger.Default.Send(new LogMessage("Skipped environment variable '" + environmentInput[index] + "': Value couldn't be converted."));
                    continue;
                }
                // Reset 'passover' variable.
                passover = 1;
            }
        }

        /// <summary>
        /// The <c>TrySet</c> method will attempt to set the value of a variable based off a key and list of
        /// values from the commandline. If it is successful, the variable will be set, and 'success' will be
        /// returned.
        /// </summary>
        /// <param name="key">The key of the variable to set, including the '-' and as case-insensitive.</param>
        /// <param name="value">The value(s) of the variable to set. This is usually converted within this method to the required type.</param>
        /// <param name="success">Whether the variable was successfully set or not.</param>
        private void TrySet(string key, string[] value, out bool success)
        {
            switch(key.ToLower())
            {
                case "-w":
                case "-workspace":
                    // Try to convert value to a Uri.
                    string condValue = string.Join("", value).Replace("\"", "").Replace(Environment.NewLine, "");
                    if (condValue.EndsWith(".vws"))
                    {
                        Uri validUri;
                        success = Uri.TryCreate(condValue, new UriCreationOptions(), out validUri);
                        if (success) SourcePath = validUri;
                        return;
                    }
                    break;
                case "-d":
                case "-document":
                    // Try to convert value to a Uri.
                    condValue = string.Join("", value).Replace("\"", "").Replace(Environment.NewLine, "");
                    if (condValue.EndsWith(".vd"))
                    {
                        Uri validUri;
                        success = Uri.TryCreate(condValue, new UriCreationOptions(), out validUri);
                        if (success) SourcePath = validUri;
                        return;
                    }
                    break;
                case "-path":
                    // Try to convert value to a Uri.
                    condValue = string.Join("", value).Replace("\"", "").Replace(Environment.NewLine, "");
                    if(condValue.EndsWith(".vd") || condValue.EndsWith(".vws"))
                    {
                        Uri validUri;
                        success = Uri.TryCreate(condValue, new UriCreationOptions(), out validUri);
                        if (success) SourcePath = validUri;
                        return;
                    }
                    break;
                case "-debug":
                    // Try to convert value to a bool.
                    bool validBool;
                    success = bool.TryParse(value[0], out validBool);
                    if (success) Debug = validBool;
                    return;
            }
            success = false;
        }
    }
}
