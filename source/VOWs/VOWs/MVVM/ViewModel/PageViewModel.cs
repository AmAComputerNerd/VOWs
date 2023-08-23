using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Nodes;
using System.Windows;
using VOWs.Custom;
using VOWs.Events;
using VOWs.MVVM.Model;
using VOWs.MVVM.Model.Data;
using DataObject = VOWs.MVVM.Model.Data.DataObject;

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
        private Document? _document;
        private Workspace? _workspace;
        // Properties.
        /// <summary>
        /// The <c>Source</c> property refers to either the <c>Document</c> or <c>Workspace</c> properties.
        /// This variable is intended to be used for display (XAML) code, as the <c>DataObject</c> it retrieves
        /// is a parent class of both the <c>Document</c> and <c>Workspace</c> classes, offering all shared
        /// functionality.
        /// </summary>
        public DataObject Source 
        {
            get => Document == null ? Workspace : Document;
            private set
            {
                if (value is Document) Document = value as Document;
                else if (value is Workspace) Workspace = value as Workspace;
                OnPropertyChanged(nameof(Source));
            }
        }
        /// <summary>
        /// The <c>Document</c> property refers to the <c>Document</c> the editor currently has open.
        /// As document is only one of the two options for VOWs data structures, this variable will be set to
        /// <c>null</c> while <c>Workspace</c> has a value, and vice versa.
        /// </summary>
        public Document? Document
        {
            get
            {
                // As we want to ensure the 'Document' object retrieved is up-to-date, we will perform a sync when
                // it is retrieved.
                SyncPages();
                return _document;
            }
            set
            {
                if (value != null)
                {
                    // Just in case the old 'Document' object is retrieved, we'll perform a sync before setting the
                    // new value.
                    SyncPages();
                    // Perform swap.
                    Workspace = null; 
                    Pages = value.Pages;
                    OnPropertyChanged(nameof(Pages));
                }
                SetProperty(ref _document, value);
            }
        }
        /// <summary>
        /// The <c>Workspace</c> property refers to the <c>Workspace</c> the editor currently has open.
        /// As workspace is only one of the two options for VOWs data structures, this variable will be set to
        /// <c>null</c> while <c>Document</c> has a value, and vice versa.
        /// </summary>
        public Workspace? Workspace
        {
            get
            {
                // As we want to ensure the 'Workspace' object retrieved is up-to-date, we will perform a sync when
                // it is retrieved.
                SyncPages();
                return _workspace;
            }
            set
            {
                if (value != null)
                {
                    // Just in case the old 'Workspace' object is retrieved, we'll perform a sync before setting the
                    // new value.
                    SyncPages();
                    // Perform swap.
                    Document = null;
                    Pages = value.SelectedDocument.Pages;
                    OnPropertyChanged(nameof(Pages));
                }
                SetProperty(ref _workspace, value);
            }
        }
        /// <summary>
        /// The <c>Pages</c> property refers to either the <c>Document.Pages</c> or <c>Workspace.SelectedDocument
        /// .Pages</c> property, depending on which is currently in use. This variable is intended to be used
        /// for display (XAML) code - it holds a temporary value which may be saved with the <c>SaveCommand</c>
        /// from the DocumentEdit View.
        /// </summary>
        public ObservableCollection<Page> Pages { get; private set; }

        /// <summary>
        /// The constructor for <c>PageViewModel</c> initialises variables relevant to <c>PageView</c>.
        /// <para></para>
        /// </summary>
        public PageViewModel(DataObject dataObject)
        {
            // Activate PageViewModel to receive messages.
            IsActive = true;
            // Set Source variables.
            Source = dataObject;
            if (Globals.CommandLineArgs.ExtractedPath == null)
            {
                // Set data of the DataObject based off file name.
                Source.Info.Name = Globals.CommandLineArgs.SourcePath == null ? "null" : Path.GetFileNameWithoutExtension(Globals.CommandLineArgs.SourcePath.AbsolutePath);
            }
            else
            {
                // Set data of the DataObject based off the info file.;
                string infoPath = Globals.CommandLineArgs.ExtractedPath.AbsolutePath + "\\info.json";
                if (!File.Exists(infoPath))
                {
                    // Malformed - just use file name.
                    Source.Info.Name = Globals.CommandLineArgs.SourcePath == null ? "null" : Path.GetFileNameWithoutExtension(Globals.CommandLineArgs.SourcePath.AbsolutePath);
                    return;
                }
                // The file exists, so we'll read from it.
                using FileStream infoReadStream = File.OpenRead(infoPath);
                // Check if the JSON is valid - a JsonException will raise if it isn't.
                JsonObject keyValuePairs = null;
                try
                {
                    keyValuePairs = JsonObject.Parse(infoReadStream)?.AsObject();
                }
                catch { }
                if (keyValuePairs == null)
                {
                    // Malformed JSON file, so we'll log a message and just use file name.
                    WeakReferenceMessenger.Default.Send(new LogMessage("Encountered a malformed 'info.json' - defaulting to using standard methods.", ToString(), LogLevel.WARNING));
                    Source.Info.Name = Globals.CommandLineArgs.SourcePath == null ? "null" : Path.GetFileNameWithoutExtension(Globals.CommandLineArgs.SourcePath.AbsolutePath);
                    return;
                }
                if (keyValuePairs.ContainsKey("name")) Source.Info.Name = keyValuePairs["theme"].GetValue<string>();
                else Source.Info.Name = Globals.CommandLineArgs.SourcePath == null ? "null" : Path.GetFileNameWithoutExtension(Globals.CommandLineArgs.SourcePath.AbsolutePath);
            }
        }
    
        /// <summary>
        /// The <c>SyncPages</c> method will resync the <c>Pages</c> variable from this view model to the source
        /// object, hence overriding it's contents with the value of <c>Pages</c>.
        /// </summary>
        public void SyncPages()
        {
            if (_document != null) _document.Pages = Pages;
            else if (_workspace != null) _workspace.SelectedDocument.Pages = Pages;
            return;
        }

        protected override void OnActivated()
        {
            // Register PageViewModel to reply to RequestDataObjectMessage messages.
            Messenger.Register<PageViewModel, RequestDataObjectMessage>(this, (r, m) => r.Reply(m));
        }

        private void Reply(RequestDataObjectMessage message)
        {
            message.Reply(Source);
        }
    }
}
