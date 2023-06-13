using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using VOWs.Events;
using VOWs.MVVM.Model;

namespace VOWs.MVVM.ViewModel
{
    public class PageViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the PageView.
        /// <summary>
        /// The <c>_storage</c> private parameter stores the backup value for <c>Storage</c>.
        /// </summary>
        private DatabaseWrapper _storage;
        /// <summary>
        /// The <c>Storage</c> parameter links back to the <c>MainViewModel</c>'s Storage parameter, and is
        /// either retrieved through messages or using the backup <c>_storage</c> parameter.
        /// </summary>
        public DatabaseWrapper Storage
        {
            get
            {
                try
                {
                    DatabaseWrapper retrievedWrapper = Messenger.Send(new RequestStorageMessage());
                    SetProperty(ref _storage, retrievedWrapper);
                }
                catch (Exception)
                {
                    // RequestStorageMessage was never answered (likely due to being called at startup).
                    // We'll return our saved copy of _storage in this case.
                }
                return _storage;
            }
            set
            {
                // Send the UpdateStorageMessage and save the new value into _storage.
                bool updated = Messenger.Send(new UpdateStorageMessage(value));
                // If Storage was never updated, we shouldn't update our local variable here.
                if (updated) SetProperty(ref _storage, value);
                else throw new ArgumentException("Storage variable was unable to be updated.");
            }
        }

        // Local resources relevant to the PageView.
        /// <summary>
        /// The <c>OpenDocument</c> parameter stores information about the currently open document, linked
        /// directly to <c>DocumentEditViewModel</c>. 
        /// </summary>
        public Document OpenDocument { get; set; }

        /// <summary>
        /// The constructor for <c>PageViewModel</c> initialises variables relevant to <c>PageView</c>.
        /// </summary>
        public PageViewModel()
        {
            // Set the backup storage variable.
            _storage = Messenger.Send(new RequestStorageMessage());
            // Set the OpenDocument variable to a default document for showcase purposes.
            OpenDocument = new Document(null);
        }
    }
}
