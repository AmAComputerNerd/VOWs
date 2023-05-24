using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOWs.Events;

namespace VOWs.MVVM.Model
{
    public class DynamicURIResolver
    {
        private string _uri;
        private Uri _resolveUri;
        // Theme-related variables.
        private bool _usesThemes;
        private DatabaseWrapper _databaseWrapper;
        private string _blackUri;
        private string _darkUri;
        private string _lightUri;
        private string _whiteUri;

        public Uri Uri
        {
            get
            {
                Uri validUri;
                if (_usesThemes && _databaseWrapper != null)
                {
                    string theme = _databaseWrapper.Theme;
                    switch (theme)
                    {
                        case "Black":
                            Uri.TryCreate(_blackUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                        case "Dark":
                            Uri.TryCreate(_darkUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                        case "Light":
                            Uri.TryCreate(_lightUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                        case "White":
                            Uri.TryCreate(_whiteUri, new UriCreationOptions(), out validUri);
                            if (validUri == null) break;
                            return validUri;
                    }
                }
                // Either it isn't themed or the theme / URI wasn't valid. We'll make a check for the standard _uri, and then just return the _resolveUri.
                if(_uri != null)
                {
                    Uri.TryCreate(_uri, new UriCreationOptions(), out validUri);
                    if (validUri != null) return validUri;
                }
                // Time to use the resolve uri.
                return _resolveUri;
            }
        }
    
        public DynamicURIResolver(string uri, Uri resolveUri)
        {
            _usesThemes = false;
            _uri = uri;
            _resolveUri = resolveUri;
        }

        public DynamicURIResolver(string darkUri, string lightUri, Uri resolveUri)
        {
            _usesThemes = true;
            _blackUri = darkUri;
            _darkUri = darkUri;
            _lightUri = lightUri;
            _whiteUri = lightUri;
            _databaseWrapper = WeakReferenceMessenger.Default.Send(new RequestStorageMessage());
            _resolveUri = resolveUri;
        }

        public DynamicURIResolver(string blackUri, string darkUri, string lightUri, string whiteUri, Uri resolveUri)
        {
            _usesThemes = true;
            _blackUri = blackUri;
            _darkUri = darkUri;
            _lightUri = lightUri;
            _whiteUri = whiteUri;
            _databaseWrapper = WeakReferenceMessenger.Default.Send(new RequestStorageMessage());
            _resolveUri = resolveUri;
        }
    }
}
