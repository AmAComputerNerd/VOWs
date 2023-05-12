using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.Core
{
    enum StorageIntent
    {
        /// <summary>
        /// This intent will block QuickStorage from allowing edit or save access to a StorageWrapper object.
        /// Best used temporarily while initialising an object.
        /// </summary>
        LocalNone,
        /// <summary>
        /// This intent will block QuickStorage from allowing edit access to a StorageWrapper object.
        /// Therefore, this object cannot have it's value changed from any means within the program.
        /// Best used for constant values not intended to be changed.
        /// </summary>
        LocalReadOnly,
        /// <summary>
        /// This intent will block QuickStorage from allowing save access to a StorageWrapper object.
        /// An object with this intent cannot be saved externally (so will not be saved to any database).
        /// Best used for temporary values or program values that are used to hold information about the state of the program (i.e. a Window state).
        /// </summary>
        LocalOnly,
        /// <summary>
        /// This intent will not block any access to a StorageWrapper object from within QuickStorage.
        /// Best used for settings values that remain across application restarts (i.e. theme).
        /// </summary>
        All
    }
}
