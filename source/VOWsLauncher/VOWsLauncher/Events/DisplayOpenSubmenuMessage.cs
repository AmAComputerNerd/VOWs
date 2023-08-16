using CommunityToolkit.Mvvm.Messaging.Messages;
using System;

namespace VOWsLauncher.Events
{
    public class DisplayOpenSubmenuMessage : RequestMessage<string?>
    {
        public TargetType Target;
        public Uri? StartDirectory;

        public DisplayOpenSubmenuMessage(TargetType target)
        {
            Target = target;
        }

        public DisplayOpenSubmenuMessage(TargetType target, Uri? startDirectory) : this(target)
        {
            StartDirectory = startDirectory;
        }
    }

    public enum TargetType
    {
        DOCUMENT,
        WORKSPACE,
        DIRECTORY
    }
}
