using CommunityToolkit.Mvvm.ComponentModel;
using VOWs.Events;

namespace VOWs.MVVM.Model
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
        public WrappedItem<string> WrappedTheme { get; }
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
        public WrappedItem<string> WrappedAccent { get; }
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
        public WrappedItem<bool> WrappedUseHighContrast { get; }
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
        public WrappedItem<bool> WrappedUseLargeText { get; }
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
        /// The <c>WrappedUseTabsForVersionControl</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedUseTabsForVersionControl { get; }
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
        public Globals()
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
            WrappedTheme = new("application.theme", "Theme", "Black", true);
            WrappedAccent = new("application.accent", "Accent", "#5da1c0", true);
            WrappedUseHighContrast = new("application.accessibility.highcontrast", "High Contrast", false, true);
            WrappedUseLargeText = new("application.accessibility.largetext", "Large Text", false, true);
            WrappedUseTabsForVersionControl = new("application.editor.versioncontrol", "Use Tabs for Version Control", true, true);
        }
    }
}