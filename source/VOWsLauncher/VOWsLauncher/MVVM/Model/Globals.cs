using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

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
        public static readonly Globals Default = new Globals();

        // Properties.
        /// <summary>
        /// The <c>WrappedTheme</c> property will return the theme with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<string> WrappedTheme { get; }
        /// <summary>
        /// The <c>Theme</c> property exposes the value of the theme.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedTheme</c>.
        /// </summary>
        public string Theme { get => WrappedTheme.Item; set => WrappedTheme.Set(value); }
        /// <summary>
        /// The <c>WrappedUseHighContrast</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedUseHighContrast { get; }
        /// <summary>
        /// The <c>UseHighContrast</c> property exposes the value of the boolean setting dictating whether elements should use a high contrast colour scheme.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedUseHighContrast</c>.
        /// </summary>
        public bool UseHighContrast { get => WrappedUseHighContrast.Item; set => WrappedUseHighContrast.Set(value); }
        /// <summary>
        /// The <c>WrappedUseLargeText</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedUseLargeText { get; }
        /// <summary>
        /// The <c>UseLargeText</c> property exposes the value of the boolean setting dictating whether text should be enlarged for easier vision.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedUseLargeText</c>.
        /// </summary>
        public bool UseLargeText { get => WrappedUseLargeText.Item; set => WrappedUseLargeText.Set(value); }
        /// <summary>
        /// The <c>WrappedShowBeginnersText</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedShowBeginnersText { get; }
        /// <summary>
        /// The <c>ShowBeginnersText</c> property exposes the value of the boolean setting dictating whether beginners information should be shown in the launcher.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedShowBeginnersText</c>.
        /// </summary>
        public bool ShowBeginnersText { get => WrappedShowBeginnersText.Item; set => WrappedShowBeginnersText.Set(value); }
        /// <summary>
        /// The <c>WrappedUseTabsForVersionControl</c> property will return the boolean setting with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<bool> WrappedUseTabsForVersionControl { get; }
        /// <summary>
        /// The <c>UseTabsForVersionControl</c> property exposes the value of the boolean setting dictating whether the version control actions should be stored in the tabs menu
        /// (true) or as a seperate window (false).
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedUseTabsForVersionControl</c>.
        /// </summary>
        public bool UseTabsForVersionControl { get => WrappedUseTabsForVersionControl.Item; set => WrappedUseTabsForVersionControl.Set(value); }
        /// <summary>
        /// The <c>Logger</c> property will return the Logger object for the current session.
        /// This Logger may also be written to by sending <c>LogMessage</c> messages.
        /// </summary>
        public Logger Logger { get; }

        /// <summary>
        /// The constructor for <c>Global</c> will initialise a new instance.
        /// <b>NOTE: This should NOT be used outside of the <c>MainViewModel</c>.</b>
        /// <br></br><br></br>
        /// <i>Looking for the Globals instance? Use <c>Globals.Default</c> and read the description there for more info!</i>
        /// </summary>
        public Globals()
        {
            // TODO: Add database access.
            WrappedTheme = new WrappedItem<string>("application.theme", "Theme", "White", true);
            WrappedUseHighContrast = new WrappedItem<bool>("application.accessibility.highcontrast", "High Contrast", false, true);
            WrappedUseLargeText = new WrappedItem<bool>("application.accessibility.largetext", "Large Text", false, true);
            WrappedShowBeginnersText = new WrappedItem<bool>("application.launcher.beginnerstips", "Beginners Tips", true, true);
            WrappedUseTabsForVersionControl = new WrappedItem<bool>("application.editor.versioncontrol", "Use Tabs for Version Control", true, true);
            Logger = Logger.New();
        }
    }
}