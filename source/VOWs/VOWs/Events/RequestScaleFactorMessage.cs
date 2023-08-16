using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.Events
{
    /// <summary>
    /// The <c>RequestScaleFactorMessage</c> will return an array of doubles - a width scale factor & a height factor.
    /// These are calculated in mind with the current application size and the value of the zoom setting.
    /// </summary>
    class RequestScaleFactorMessage : RequestMessage<double[]>
    {
    }
}
