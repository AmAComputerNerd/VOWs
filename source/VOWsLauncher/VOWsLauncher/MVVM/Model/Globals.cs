using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows;
using VOWsLauncher.Events;

namespace VOWsLauncher.MVVM.Model
{
    /// <summary>
    /// The <c>Globals</c> class contains data relevant to multiple classes or objects.
    /// Any properties, fields or data methods from a class that are required outside the scope of that class should be moved here!
    /// </summary>
    public class Globals : ObservableObject
    {
        /// <summary>
        /// The <c>Default</c> field is a reference to the default instance of this class.
        /// <br></br>
        /// In almost all cases, this should <b>always</b> be the instance used. Using a new instance to access data will likely result in <c>null</c> or default values for many objects
        /// in the instance - you are cutting yourself off from changes made from all other classes. Using a new instance to save data <i>will work,</i> but changes will not be seen by
        /// other classes - you are cutting other classes off from changes you make.
        /// <br></br><br></br>
        /// Keep it simple, stick with the Default!
        /// </summary>
        public static readonly Globals Default = new();

        // Properties (database).
        /// <summary>
        /// The <c>WrappedTheme</c> property will return the theme with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<string> WrappedTheme { get; private set; }
        /// <summary>
        /// The <c>Theme</c> property exposes the value of the theme.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedTheme</c>.
        /// </summary>
        public string Theme 
        { 
            get => WrappedTheme.Item;
            set
            {
                WrappedTheme.Set(value);
                OnPropertyChanged(nameof(Theme));
            }
        }
        /// <summary>
        /// The <c>WrappedAccent</c> property will return the accent colour with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<string> WrappedAccent { get; private set; }
        /// <summary>
        /// The <c>Accent</c> property exposes the value of the accent.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedAccent</c>.
        /// </summary>
        public string Accent
        {
            get => WrappedAccent.Item;
            set
            {
                WrappedAccent.Set(value);
                OnPropertyChanged(nameof(Accent));
            }
        }
        /// <summary>
        /// The <c>WrappedUseHighContrast</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedUseHighContrast { get; private set; }
        /// <summary>
        /// The <c>UseHighContrast</c> property exposes the value of the boolean setting dictating whether elements should use a high contrast colour scheme.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedUseHighContrast</c>.
        /// </summary>
        public bool UseHighContrast 
        { 
            get => WrappedUseHighContrast.Item;
            set
            {
                WrappedUseHighContrast.Set(value);
                OnPropertyChanged(nameof(UseHighContrast));
            }
        }
        /// <summary>
        /// The <c>WrappedUseLargeText</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedUseLargeText { get; private set; }
        /// <summary>
        /// The <c>UseLargeText</c> property exposes the value of the boolean setting dictating whether text should be enlarged for easier vision.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedUseLargeText</c>.
        /// </summary>
        public bool UseLargeText 
        { 
            get => WrappedUseLargeText.Item;
            set
            {
                WrappedUseLargeText.Set(value);
                OnPropertyChanged(nameof(UseLargeText));
            }
        }
        /// <summary>
        /// The <c>WrappedShowBeginnersText</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedShowBeginnersText { get; private set; }
        /// <summary>
        /// The <c>ShowBeginnersText</c> property exposes the value of the boolean setting dictating whether beginners information should be shown in the launcher.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedShowBeginnersText</c>.
        /// </summary>
        public bool ShowBeginnersText 
        { 
            get => WrappedShowBeginnersText.Item; 
            set 
            {
                WrappedShowBeginnersText.Set(value);
                OnPropertyChanged(nameof(ShowBeginnersText));
            } 
        }
        /// <summary>
        /// The <c>WrappedUseTabsForVersionControl</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedUseTabsForVersionControl { get; private set; }
        /// <summary>
        /// The <c>UseTabsForVersionControl</c> property exposes the value of the boolean setting dictating whether the version control actions should be stored in the tabs menu
        /// (true) or as a seperate window (false).
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedUseTabsForVersionControl</c>.
        /// </summary>
        public bool UseTabsForVersionControl 
        { 
            get => WrappedUseTabsForVersionControl.Item; 
            set 
            {
                WrappedUseTabsForVersionControl.Set(value);
                OnPropertyChanged(nameof(UseTabsForVersionControl));
            } 
        }
        
        // Properties (local).
        /// <summary>
        /// The <c>Logger</c> property will return the Logger object for the current session.
        /// This Logger may also be written to by sending <c>LogMessage</c> messages.
        /// </summary>
        public Logger Logger { get; }
        /// <summary>
        /// The <c>CommandLineArgs</c> property will return an object that exposes properties obtained from command line arguments.
        /// This notably includes a debug status.
        /// </summary>
        public EnvironmentArgs CommandLineArgs { get; }

        /// <summary>
        /// The constructor for <c>Global</c> will initialise a new instance.
        /// <b>NOTE: This should NOT be used outside of the <c>MainViewModel</c>.</b>
        /// <br></br><br></br>
        /// <i>Looking for the Globals instance? Use <c>Globals.Default</c> and read the description there for more info!</i>
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Globals()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            // Set local properties.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Logger = Logger.New();
            CommandLineArgs = new();
            if (CommandLineArgs.Debug) Logger.MinimumLogLevel = (int)LogLevel.DEBUG; // If the 'Debug' property was set, we should display Debug-level messages in the log.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8601 // Possible null reference assignment.
            // TODO: Add database access.
            RefreshValues();
        }

        public void RefreshValues()
        {
            // Check if Uri to settings.json exists.
            string configDirectoryPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "/config";
            Directory.CreateDirectory(configDirectoryPath);
            string settingsPath = configDirectoryPath + "/settings.json";
            if (!File.Exists(settingsPath))
            {
                // The file doesn't exist! We'll create a new one.
                WeakReferenceMessenger.Default.Send(new LogMessage("No 'settings.json' file found - creating a new one with default settings.", ToString(), LogLevel.INFO));
                JsonObject defaultContent = new()
                {
                    ["theme"] = "Dark",
                    ["accent"] = "#5da1c0",
                    ["accessibility"] = new JsonObject()
                    {
                        ["highcontrast"] = false,
                        ["largetext"] = false
                    },
                    ["launcher"] = new JsonObject()
                    {
                        ["beginnerstips"] = true
                    },
                    ["editor"] = new JsonObject()
                    {
                        ["versioncontrol"] = true
                    }
                };
                using FileStream settingsStream = File.Create(settingsPath);
                using Utf8JsonWriter settingsWriter = new(settingsStream);
                defaultContent.WriteTo(settingsWriter, new() { WriteIndented = true });
                WeakReferenceMessenger.Default.Send(new LogMessage("Successfully created new 'settings.json' file!", ToString(), LogLevel.DEBUG));
                RefreshValues();
            }
            // The file exists, so we'll read from it.
            using FileStream settingsReadStream = File.OpenRead(settingsPath);
            // Check if the JSON is valid - a JsonException will raise if it isn't.
            JsonObject keyValuePairs = null;
            try
            {
                keyValuePairs = JsonObject.Parse(settingsReadStream)?.AsObject();
            }
            catch { }
            if (keyValuePairs == null)
            {
                // Malformed JSON file, so we'll log a message and reconstruct the file with default values.
                WeakReferenceMessenger.Default.Send(new LogMessage("Encountered a malformed 'settings.json' - attempting to reconstruct file.", ToString(), LogLevel.WARNING));
                // Before deletion, we'll also close the FileStream.
                settingsReadStream.Dispose();
                settingsReadStream.Close();
                File.Delete(settingsPath);
                WeakReferenceMessenger.Default.Send(new LogMessage("Removed malformed 'settings.json' file.", ToString(), LogLevel.DEBUG));
                MessageBox.Show("Detected a malformed 'settings.json' file. Default settings have been restored.", "Warning");
                RefreshValues();
                return;
            }
            // Valid JSON file - we can begin the slow process of extracting values.
            if (keyValuePairs.ContainsKey("theme")) WrappedTheme = new(keyValuePairs["theme"].GetPath(), "Theme", keyValuePairs["theme"].GetValue<string>(), true);
            else WrappedTheme = new("", "Theme", "Dark", true);
            if (keyValuePairs.ContainsKey("accent")) WrappedAccent = new(keyValuePairs["accent"].GetPath(), "Accent", keyValuePairs["accent"].GetValue<string>(), true);
            else WrappedAccent = new("", "Accent", "#5da1c0", true);
            // Retrieve "accessibility" sub-category and extract child values.
            JsonObject accessibility = new();
            if (keyValuePairs.ContainsKey("accessibility"))
            {
                try { accessibility = keyValuePairs["accessibility"].AsObject(); }
                catch { accessibility = new(); }
            }
            if (accessibility.ContainsKey("highcontrast")) WrappedUseHighContrast = new(accessibility["highcontrast"].GetPath(), "Use High Contrast", accessibility["highcontrast"].GetValue<bool>(), true);
            else WrappedUseHighContrast = new("", "Use High Contrast", false, true);
            if (accessibility.ContainsKey("largetext")) WrappedUseLargeText = new(accessibility["largetext"].GetPath(), "Use Large Text", accessibility["largetext"].GetValue<bool>(), true);
            else WrappedUseLargeText = new("", "Use Large Text", false, true);
            // Retrieve "launcher" sub-category and extract child values.
            JsonObject launcher = new();
            if (keyValuePairs.ContainsKey("launcher"))
            {
                try { launcher = keyValuePairs["launcher"].AsObject(); }
                catch { launcher = new(); }
            }
            if (launcher.ContainsKey("beginnerstips")) WrappedShowBeginnersText = new(launcher["beginnerstips"].GetPath(), "Show Beginners Tips", launcher["beginnerstips"].GetValue<bool>(), true);
            else WrappedShowBeginnersText = new("", "Show Beginners Tips", true, true);
            // Retrieve "editor" sub-category and extract child values.
            JsonObject editor = new();
            if (keyValuePairs.ContainsKey("editor"))
            {
                try { editor = keyValuePairs["editor"].AsObject(); }
                catch { editor = new(); }
            }
            if (editor.ContainsKey("versioncontrol")) WrappedUseTabsForVersionControl = new(editor["versioncontrol"].GetPath(), "Use Tabs for Version Control", editor["versioncontrol"].GetValue<bool>(), true);
            else WrappedUseTabsForVersionControl = new("", "Use Tabs for Version Control", true, true);
        }
    
        public void SaveValues()
        {
            // Retrieve directory values.
            string configDirectoryPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName + "/config";
            Directory.CreateDirectory(configDirectoryPath);
            string settingsPath = configDirectoryPath + "/settings.json";
            // Now we can begin writing anew to this file.
            JsonObject newContent = new()
            {
                ["theme"] = WrappedTheme.Item,
                ["accent"] = WrappedAccent.Item,
                ["accessibility"] = new JsonObject()
                {
                    ["highcontrast"] = WrappedUseHighContrast.Item,
                    ["largetext"] = WrappedUseLargeText.Item
                },
                ["launcher"] = new JsonObject()
                {
                    ["beginnerstips"] = WrappedShowBeginnersText.Item
                },
                ["editor"] = new JsonObject()
                {
                    ["versioncontrol"] = WrappedUseTabsForVersionControl.Item
                }
            };
            using FileStream settingsStream = File.Create(settingsPath);
            using Utf8JsonWriter settingsWriter = new(settingsStream);
            newContent.WriteTo(settingsWriter, new() { WriteIndented = true });
            // Now, to update variables, we'll call RefreshValues().
            // Unfortunately, this means we cant make use of the fancy using keyword.
        }
    }
}