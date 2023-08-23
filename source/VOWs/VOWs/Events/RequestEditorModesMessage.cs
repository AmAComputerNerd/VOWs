using CommunityToolkit.Mvvm.Messaging.Messages;

namespace VOWs.Events
{
    public class RequestEditorModesMessage : RequestMessage<RequestEditorModesMessage>
    {
        public bool CompatibilityMode { get; set; }
        public bool TextOnlyMode { get; set; }
        public bool ReadOnlyMode { get; set; }

        public RequestEditorModesMessage() { }
        public RequestEditorModesMessage(bool compatibilityMode, bool textOnlyMode, bool readOnlyMode)
        {
            CompatibilityMode = compatibilityMode;
            TextOnlyMode = textOnlyMode;
            ReadOnlyMode = readOnlyMode;
        }
    }
}
