using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.Events
{
    class RetrieveSelectedTemplateMessage : RequestMessage<Template>
    {
    }
}
