using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOWs.MVVM.Model;

namespace VOWs.Events
{
    class RequestStorageMessage : RequestMessage<DatabaseWrapper>
    {
    }
}
