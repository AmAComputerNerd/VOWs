using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VOWsLauncher.MVVM.Model
{
    /// <summary>
    /// The <c>DynamicURIResolver</c> class is a wrapper for themed images or URIs.
    /// When created, up to four different (string) URIs can be entered to be used depending on the selected theme, as well as a backup URI which must exist to display in case the 
    /// selected URI cannot be created.
    /// </summary>
    public class DynamicURIResolver : ObservableObject
    {
        // External variables
        private Globals Globals { get => Globals.Default; }
        // URI variables
        private string? _uri;
        private Uri _resolveUri;
        // Theme-related variables.
        private bool _usesThemes;
        private string? _blackUri;
        private string? _darkUri;
        private string? _lightUri;
        private string? _whiteUri;

        /// <summary>
        /// The <c>Uri</c> property will return the appropriate URI depending on the theme.
        /// If the themed URI cannot be resolved, the backup URI will instead be returned.
        /// </summary>
        public Uri Uri
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            get
            {
                Uri validUri;
                if (_usesThemes && Globals.Theme != null)
                {
                    switch (Globals.Theme.ToLower())
                    {
                        case "black":
                            Uri.TryCreate(_blackUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                        case "dark":
                            Uri.TryCreate(_darkUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                        case "light":
                            Uri.TryCreate(_lightUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                        case "white":
                            Uri.TryCreate(_whiteUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                    }
                }
                // Either it isn't themed or the theme / URI wasn't valid. We'll make a check for the standard _uri, and then just return the _resolveUri.
                if (_uri != null)
                {
                    Uri.TryCreate(_uri, new UriCreationOptions(), out validUri);
                    if (validUri != null) return validUri;
                }
                // Time to use the resolve uri.
                return _resolveUri;
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        /// <summary>
        /// The <c>ImageSource</c> property will return the appropriate URI as an ImageSource.
        /// This is mainly for XAML access, as it saves needing the use of a converter class.
        /// </summary>
        public ImageSource ImageSource { get => new BitmapImage(Uri); }

        /// <summary>
        /// The constructor for <c>DynamicURIResolver</c> will create a new object with a main (string) uri, and a backup (URI) uri.
        /// The main URI will attempt to be resolved whenever the <c>Uri</c> property is referenced. If it can't, the pre-made backup URI will instead be returned.
        /// </summary>
        /// <param name="uri">The main URI - this is in path format (thus should be valid syntax for creating a new URI).</param>
        /// <param name="resolveUri">The resolve (backup) URI - this is used in cases where the main URI cannot be resolved.</param>
        public DynamicURIResolver(string uri, Uri resolveUri)
        {
            _usesThemes = false;
            _uri = uri;
            _resolveUri = resolveUri;
        }

        /// <summary>
        /// The constructor for <c>DynamicURIResolver</c> will create a new object with a (string) uri for dark themes, a (string) uri for light themes and a backup (URI) uri.
        /// A Dark theme is classified as: "dark", "black".
        /// A White theme is classified as: "light", "white".
        /// If the appropriate URI cannot be resolved, the pre-made backup URI will instead be returned.
        /// </summary>
        /// <param name="darkUri">A dark-theme URI - this is in path format (thus should be valid syntax for creating a new URI).</param>
        /// <param name="lightUri">A light-theme URI - this is in path format (thus should be valid syntax for creating a new URI).</param>
        /// <param name="resolveUri">The resolve (backup) URI - this is used in cases where the themed URI cannot be resolved.</param>
        public DynamicURIResolver(string darkUri, string lightUri, Uri resolveUri)
        {
            _usesThemes = true;
            _blackUri = darkUri;
            _darkUri = darkUri;
            _lightUri = lightUri;
            _whiteUri = lightUri;
            _resolveUri = resolveUri;
        }

        /// <summary>
        /// The constructor for <c>DynamicURIResolver</c> will create a new object with a (string) uri for all themes and a backup (URI) uri.
        /// If the appropriate URI cannot be resolved, the pre-made backup URI will instead be returned.
        /// </summary>
        /// <param name="blackUri">A black theme URI - this is in path format (thus should be valid syntax for creating a new URI).</param>
        /// <param name="darkUri">A dark theme URI - this is in path format (thus should be valid syntax for creating a new URI).</param>
        /// <param name="lightUri">A light theme URI - this is in path format (thus should be valid syntax for creating a new URI).</param>
        /// <param name="whiteUri">A white theme URI - this is in path format (thus should be valid syntax for creating a new URI).</param>
        /// <param name="resolveUri">The resolve (backup) URI - this is used in cases where the themed URI cannot be resolved.</param>
        public DynamicURIResolver(string blackUri, string darkUri, string lightUri, string whiteUri, Uri resolveUri)
        {
            _usesThemes = true;
            _blackUri = blackUri;
            _darkUri = darkUri;
            _lightUri = lightUri;
            _whiteUri = whiteUri;
            _resolveUri = resolveUri;
        }
    }
}
