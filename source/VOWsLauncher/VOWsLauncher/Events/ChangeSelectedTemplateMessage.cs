using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.Events
{
    public class ChangeSelectedTemplateMessage
    {
        public Template Template { get; }

        public ChangeSelectedTemplateMessage(Template template)
        {
            Template = template;
        }
    }
}
