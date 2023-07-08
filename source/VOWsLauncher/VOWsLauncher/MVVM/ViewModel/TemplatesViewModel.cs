using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class TemplatesViewModel : ObservableRecipient
    {
        public Globals Globals { get => Globals.Default; }
        public ObservableCollection<ListViewItem> Templates { get; }
        public Template SelectedTemplate { get; }

        public RelayCommand DoneCommand { get; }
        public RelayCommand CancelCommand { get; }

        public TemplatesViewModel()
        {
            // Activate TemplatesViewModel to receive messages.
            IsActive = true;

            Templates = new ObservableCollection<ListViewItem>()
            {
                QuickMake("📄   VOWs Tutorial"),
                QuickMake("📁   VOWs Workspace Tutorial")
            };
            SelectedTemplate = Template.Empty;
            DoneCommand = new(() =>
            {
                Messenger.Send(new ChangeViewMessage(1));
            });
            CancelCommand = new(() =>
            {
                Messenger.Send(new ChangeViewMessage(1));
            });
        }

        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messagers from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            // Register the class to reply to RetrieveSelectedTemplateMessage events.
            Messenger.Register<TemplatesViewModel, RetrieveSelectedTemplateMessage>(this, (r, m) => r.Reply(m));
        }

        /// <summary>
        /// The <c>Reply</c> method will be called whenever the <c>RetrieveSelectedTemplateMessage</c> is sent.
        /// </summary>
        /// <param name="message">The event to reply to.</param>
        private void Reply(RetrieveSelectedTemplateMessage message)
        {
            message.Reply(SelectedTemplate);
        }

        /// <summary>
        /// The <c>QuickMake</c> function handles the logic behind making a <c>ListViewItem</c> with content attached.
        /// This is a helper method that simplifies that process from 3 to 1 line.
        /// </summary>
        /// <param name="content">The content this <c>ListViewItem</c> will display.</param>
        /// <returns>The created <c>ListViewItem</c>.</returns>
        private ListViewItem QuickMake(string content)
        {
            ListViewItem item = new()
            {
                Content = content
            };
            return item;
        }
    }
}
