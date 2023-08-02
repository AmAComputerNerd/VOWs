using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
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

        // Fields.
        private Document _document;
        private Workspace _workspace;
        // Properties.
        /// <summary>
        /// The <c>Document</c> property refers to the currently open document and it's info.
        /// Either <c>Document</c> or <c>Workspace</c> must hold a value.
        /// </summary>
        public Document Document { get => _document; set => SetProperty(ref _document, value); }
        /// <summary>
        /// The <c>Workspace</c> property refers to the currently open workspace and it's info.
        /// Either <c>Document</c> or <c>Workspace</c> must hold a value.
        /// </summary>
        public Workspace Workspace { get => _workspace; set => SetProperty(ref _workspace, value); }

        /// <summary>
        /// The constructor for <c>PageViewModel</c> initialises variables relevant to <c>PageView</c>.
        /// </summary>
        public PageViewModel()
        {
            
        }
    }
}
