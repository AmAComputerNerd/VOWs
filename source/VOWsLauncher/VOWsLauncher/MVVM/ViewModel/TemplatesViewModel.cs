using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class TemplatesViewModel : ObservableRecipient
    {
        public Globals Globals { get => Globals.Default; }

        // Fields.
        private bool _isVOWsTutorial1Selected;
        private bool _isVOWsTutorial2Selected;
        // Properties.
        public ObservableCollection<RadioButton> Templates { get; }
        public Template SelectedTemplate { get; private set; }

        public bool IsVOWsTutorial1Selected
        {
            get => _isVOWsTutorial1Selected;
            set
            {
                if (value && _isVOWsTutorial1Selected)
                {
                    IsVOWsTutorial1Selected = false;
                    IsVOWsTutorial2Selected = false;
                    SelectedTemplate = Template.Empty;
                    return;
                }
                else if (value)
                {
                    if (!Uri.TryCreate(Environment.CurrentDirectory + "/templates/tutorial1.vdoc", new(), out Uri uri))
                    {
                        // The tutorial was deleted or otherwise isn't present.
                        Messenger.Send(new LogMessage("Failed to locate 'templates/tutorial1.vdoc' - aborting template selection.", ToString(), LogLevel.WARNING));
                        MessageBox.Show("Failed to locate 'templates/tutorial1.vdoc' - is the file missing?\nCouldn't select template - please try again.", "Error");
                        return;
                    }
                    SelectedTemplate = new Template("VOWs for Beginners", "A tutorial aiming to introduce new users to VOWs!", uri, false);
                }
                SetProperty(ref _isVOWsTutorial1Selected, value);
            }
        }
        public bool IsVOWsTutorial2Selected
        {
            get => _isVOWsTutorial2Selected;
            set
            {
                if (value && _isVOWsTutorial2Selected)
                {
                    IsVOWsTutorial1Selected = false;
                    IsVOWsTutorial2Selected = false;
                    SelectedTemplate = Template.Empty;
                    return;
                }
                else if (value)
                {
                    if (!Uri.TryCreate(Environment.CurrentDirectory + "/templates/tutorial2.vwsp", new(), out Uri uri))
                    {
                        // The tutorial was deleted or otherwise isn't present.
                        Messenger.Send(new LogMessage("Failed to locate 'templates/tutorial2.vwsp' - aborting template selection.", ToString(), LogLevel.WARNING));
                        MessageBox.Show("Failed to locate 'templates/tutorial2.vwsp' - is the file missing?\nCouldn't select template - please try again.", "Error");
                        return;
                    }
                    SelectedTemplate = new Template("VOWs Workspace Tutorial", "A tutorial aiming to introduce new users to VOWs workspaces!", uri, true);
                }
                SetProperty(ref _isVOWsTutorial2Selected, value);
            }
        }

        public RelayCommand DoneCommand { get; }

        public TemplatesViewModel()
        {
            // Activate TemplatesViewModel to receive messages.
            IsActive = true;

            Templates = new()
            {
                QuickMake("📄   VOWs for Beginners"),
                QuickMake("📁   VOWs Workspace Tutorial")
            };
            SelectedTemplate = Template.Empty;
            DoneCommand = new(() =>
            {
                Messenger.Send(new ChangeViewMessage(1));
            });
        }

        /// <summary>
        /// The overriden <c>OnActivated</c> method registers the class with a variety of messagers from the Messenger object.
        /// </summary>
        protected override void OnActivated()
        {
            // Register the class to respond to ChangeSelectedTemplateMessage events.
            Messenger.Register<TemplatesViewModel, ChangeSelectedTemplateMessage>(this, (r, m) => r.Receive(m));
            // Register the class to reply to RetrieveSelectedTemplateMessage events.
            Messenger.Register<TemplatesViewModel, RetrieveSelectedTemplateMessage>(this, (r, m) => r.Reply(m));
        }

        /// <summary>
        /// The <c>Receive</c> method will be called whenever the <c>ChangeSelectedTemplateMessage</c> is sent.
        /// </summary>
        /// <param name="message"></param>
        private void Receive(ChangeSelectedTemplateMessage message)
        {
            SelectedTemplate = message.Template;
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
        private RadioButton QuickMake(string content)
        {
            Style radioButtonStyle = (Style)Application.Current.TryFindResource("StylisedButtonSticky");
            RadioButton item = new()
            {
                Content = content,
                Padding = new(0d, 5d, 0d, 5d),
                Style = radioButtonStyle
            };
            return item;
        }
    }
}
