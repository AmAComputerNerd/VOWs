using System;

namespace VOWsLauncher.Events
{
    public class DisplayNewSubmenuMessage
    {
        public bool IsWorkspace;
        public Uri? TemplateUri;

        public DisplayNewSubmenuMessage(bool isWorkspace, Uri? templateUri)
        {
            IsWorkspace = isWorkspace;
            TemplateUri = templateUri;
        }

        public static DisplayNewSubmenuMessage Document()
        {
            return new DisplayNewSubmenuMessage(false, null);
        }

        public static DisplayNewSubmenuMessage Workspace()
        {
            return new DisplayNewSubmenuMessage(true, null);
        }

        public static DisplayNewSubmenuMessage Template(Uri templateLoc)
        {
            // TODO: Stuff with templates.
            return new DisplayNewSubmenuMessage(true, templateLoc);
        }
    }
}
