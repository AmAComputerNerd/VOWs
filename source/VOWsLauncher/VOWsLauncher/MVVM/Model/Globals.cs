using CommunityToolkit.Mvvm.ComponentModel;

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

        // Fields.
        private WrappedItem<string> _wrappedTheme;
        private Logger _logger;

        // Properties.
        /// <summary>
        /// The <c>WrappedTheme</c> property will return the theme with extra database information attached (<c>WrappedItem</c>).
        /// </summary>
        public WrappedItem<string> WrappedTheme { get { return _wrappedTheme; } }
        /// <summary>
        /// The <c>Theme</c> property exposes the value of the theme.
        /// Typically, unless you really need database information, it makes more sense to use this property over <c>WrappedTheme</c>.
        /// </summary>
        public string Theme { get => _wrappedTheme.Item; set => _wrappedTheme.Set(value); }
        /// <summary>
        /// The <c>Logger</c> property will return the Logger object for the current session.
        /// This Logger may also be written to by sending <c>LogMessage</c> messages.
        /// </summary>
        public Logger Logger { get => _logger; }

        /// <summary>
        /// The constructor for <c>Global</c> will initialise a new instance.
        /// <b>NOTE: This should NOT be used outside of the <c>MainViewModel</c>.</b>
        /// <br></br><br></br>
        /// <i>Looking for the Globals instance? Use <c>Globals.Default</c> and read the description there for more info!</i>
        /// </summary>
        public Globals()
        {
            // TODO: Add database access.
            _wrappedTheme = new WrappedItem<string>("application.theme", "Theme", "Dark", false);
            _logger = Logger.New();
        }
    }
}