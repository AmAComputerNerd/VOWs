using CommunityToolkit.Mvvm.ComponentModel;
using VOWs.MVVM.Model;
using VOWs.MVVM.Model.Data;

namespace VOWs.MVVM.ViewModel
{
    public partial class PageViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the PageView.
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        // Local resources relevant to the PageView.
        private Document _openDocument;
        /// <summary>
        /// The <c>OpenDocument</c> property stores information about the currently open document.
        /// </summary>
        public Document OpenDocument { get => _openDocument; set => SetProperty(ref _openDocument, value); }

        /// <summary>
        /// The constructor for <c>PageViewModel</c> initialises variables relevant to <c>PageView</c>.
        /// </summary>
        public PageViewModel()
        {
            // Set the OpenDocument variable to a default document for showcase purposes.
            OpenDocument = new Document(null);
        }
    }
}
