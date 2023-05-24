using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOWs.MVVM.Model;

namespace VOWs.Events
{
    public class UpdateStorageMessage : RequestMessage<bool>
    {
        public DatabaseWrapper UpdatedValue;

        public UpdateStorageMessage(DatabaseWrapper updatedValue)
        {
            UpdatedValue = updatedValue;
        }
    }
}
