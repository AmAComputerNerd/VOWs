using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using VOWs.Events;
using VOWs.MVVM.Model.Data;

namespace VOWs.MVVM.Model
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
        /// The <c>SourcePath</c> parameter will point to the currently open Document or Workspace.
        /// If no Document or Workspace is open, it will simply be null.
        /// </summary>
        public Uri? SourcePath { get; private set; }
        /// <summary>
        /// The <c>ExtractedPath</c> parameter will point to the extracted path to the currently open Document or Workspace.
        /// If no Document or Workspace is open, it will simply be null.
        /// </summary>
        public Uri? ExtractedPath { get; private set; }
        /// <summary>
        /// The <c>SourcePathExtensionType</c> is populated simultaneously to <c>SourcePath</c> and contains information about
        /// the format of the open document / workspace.
        /// </summary>
        public ExtensionType SourcePathExtensionType { get; private set; }
        /// <summary>
        /// The <c>EnforceCompatibilityMode</c> parameter is a flag to enforce whether the editor should run in
        /// a legacy compatibility mode, which disables custom VOWs features such as version control.
        /// </summary>
        public bool EnforceCompatibilityMode { get; private set; }
        /// <summary>
        /// The <c>EnforceTextOnlyMode</c> parameter is a flag to enforce whether the editor should run in a
        /// limited text-only mode, which disables all media-related features.
        /// </summary>
        public bool EnforceTextOnlyMode { get; private set; }
        /// <summary>
        /// The <c>EnforceReadOnlyMode</c> parameter is a flag to enforce whether the editor should run in a
        /// limited read-only mode, which disables all editing features. Export functions, along with some version
        /// control features are still available.
        /// </summary>
        public bool EnforceReadOnlyMode { get; private set; }
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
            // Initialise a temporary cache for log messages.
            // Since the Logger will log all messages until EnvironmentArgs are read, we should not print any messages until after this file has been initialised.
            List<LogMessage> cache = new();
            // Retrieve command line arguments, except for the first argument which contains the executable file name.
            environmentInput = Environment.GetCommandLineArgs().Skip(1).ToArray();
            // For debug purposes, we'll enter a log stating that EnvironmentArgs has retrieved command line arguments.
            if (environmentInput.Length > 0) cache.Add(new("EnvironmentArgs has retrieved command line arguments: " + string.Join(", ", environmentInput), ToString(), LogLevel.DEBUG));
            // Begin iteration to set variables.
            int i = 0;
            while (i < environmentInput.Length)
            {
                string extension = environmentInput[i];
                if (!extension.StartsWith("-"))
                {
                    // This isn't a valid extension. We should ignore it.
                    cache.Add(new("Ignored extension '" + extension + "' due to invalid format (extensions must start with '-').", ToString(), LogLevel.WARNING));
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
                if (!success) cache.Add(new("Ignored extension '" + extension + "' due to invalid value format (value '" + value + "' could not be converted to the required type).", ToString(), LogLevel.WARNING));
                else cache.Add(new("Extension '" + extension + "' with value '" + value + "' was recognised and read.", ToString(), LogLevel.DEBUG));
                // We'll set i to the value of j (the index of the final part of the value) plus one to continue iterating the loop.
                i = j + 1;
            }
            // For any values that weren't already set, we'll apply defaults.
            ApplyDefaults();
            // Submit the collected LogMessages to the Logger.
            cache.ForEach(lm => WeakReferenceMessenger.Default.Send(lm));
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
            switch(key.ToLower())
            {
                case "-w":
                case "-workspace":
                    // To certify that this is a valid workspace, we'll check it's extension.
                    if (ExtensionUtils.WorkspaceExtensions.ContainsKey(ExtensionUtils.GetType("." + value.Split('.').Last())))
                    {
                        // This is a valid Workspace extension, so we'll try converting it to a Uri.
                        success = Uri.TryCreate(value, new UriCreationOptions(), out Uri workspaceUri);
                        if (success)
                        {
                            SourcePath = workspaceUri;
                            SourcePathExtensionType = ExtensionUtils.GetType("." + value.Split('.').Last());
                            if (SourcePathExtensionType.Equals(ExtensionType.VWSP)) ExtractData();
                        }
                        return;
                    }
                    break;
                case "-d":
                case "-document":
                    // To certify that this is a valid document, we'll check it's extension.
                    if (ExtensionUtils.DocumentExtensions.ContainsKey(ExtensionUtils.GetType("." + value.Split('.').Last())))
                    {
                        // This is a valid Document extension, so we'll try converting it to a Uri.
                        success = Uri.TryCreate(value, new UriCreationOptions(), out Uri documentUri);
                        if (success)
                        {
                            SourcePath = documentUri;
                            SourcePathExtensionType = ExtensionUtils.GetType("." + value.Split('.').Last());
                            if (SourcePathExtensionType.Equals(ExtensionType.VDOC)) ExtractData();
                        }
                        return;
                    }
                    break;
                case "-path":
                case "-source":
                    // The source uri for the document or workspace is left ambiguous.
                    // We'll just try to convert it to a Uri and set the value without doing any checking.
                    success = Uri.TryCreate(value, new UriCreationOptions(), out Uri ambiguousUri);
                    if (success)
                    {
                        SourcePath = ambiguousUri;
                        SourcePathExtensionType = ExtensionUtils.GetType("." + value.Split('.').Last());
                        ExtractData();
                    }
                    return;
                case "-enforceCompatibility":
                    // Try to convert value to a bool.
                    success = bool.TryParse(value, out bool compatibilityBool);
                    if (success) EnforceCompatibilityMode = compatibilityBool;
                    return;
                case "-enforceTextOnly":
                    // Try to convert value to a bool.
                    success = bool.TryParse(value, out bool textOnlyBool);
                    if (success) EnforceTextOnlyMode = textOnlyBool;
                    return;
                case "-enforceReadOnly":
                    // Try to convert value to a bool.
                    success = bool.TryParse(value, out bool readOnlyBool);
                    if (success) EnforceReadOnlyMode = readOnlyBool;
                    return;
                case "-debug":
                    // Try to convert value to a bool.
                    success = bool.TryParse(value, out bool debugBool);
                    if (success) Debug = debugBool;
                    return;
            }
            success = false;
        }
    
        /// <summary>
        /// The <c>ApplyDefaults</c> method will set all variables that are yet to be set to their default
        /// values. This does <b>not</b> override any values already set, so is safe to use at any point.
        /// </summary>
        private void ApplyDefaults()
        {
            // Set the default value for SourcePath.
            if (SourcePath == null) SourcePath = null;
        }
    
        /// <summary>
        /// The <c>ExtractData</c> method will be called once to extract the data of the open Document or Workspace.
        /// This should only be called where the document / workspace is in VOWs format.
        /// </summary>
        private void ExtractData()
        {
            // Define path to /temp/ folder for all unzipping and rezipping operations.
            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "\\temp";
            // Create the directory anew if it doesn't exist, or delete it's contents if it does.
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new(path);
                foreach (FileInfo file in di.GetFiles()) file.Delete();
                foreach (DirectoryInfo dir in di.GetDirectories()) dir.Delete(true);
            }
            else Directory.CreateDirectory(path);
            // Now we can begin constructing a new VOWs object in this directory!
            // First, we'll copy the open file to the temp directory.
            File.Copy(SourcePath.AbsolutePath, Directory.GetParent(SourcePath.AbsolutePath).FullName + "\\zipped.zip");
            // Next, we can extract it to the /temp/ directory.
            ZipFile.ExtractToDirectory(Directory.GetParent(SourcePath.AbsolutePath).FullName + "\\zipped.zip", path);
            // Now that it's extracted, we'll delete the zip file we created and set the value of ExtractedPath.
            File.Delete(Directory.GetParent(SourcePath.AbsolutePath).FullName + "\\zipped.zip");
            ExtractedPath = new(path);
        }
    }
}
