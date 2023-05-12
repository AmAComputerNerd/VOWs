using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.Core
{
    class StorageWrapper<T>
    {
        private readonly QuickStorage quickStorage;
        public string Key { get; private set; }
        public T Value { get; set; }
        public StorageIntent Intent { get; private set; }

        public StorageWrapper(QuickStorage quickStorage, string key, T value)
        {
            this.quickStorage = quickStorage;
            Key = key;
            Value = value;
            Intent = StorageIntent.All;
        }

        public StorageWrapper(QuickStorage quickStorage, string key, T value, StorageIntent intent) : this(quickStorage, key, value)
        {
            Intent = intent;
        }

        public StorageWrapper(QuickStorage quickStorage, Func<string> key, Func<T> value)
        {
            this.quickStorage = quickStorage;
            // Set Intent to LocalNone until the key and value have resolved.
            Intent = StorageIntent.LocalNone;
            Key = key();
            Value = value();
            // All required parameters must have resolved, so Intent can be set to it's default (All).
            Intent = StorageIntent.All;
        }

        public StorageWrapper(QuickStorage quickStorage, Func<string> key, Func<T> value, StorageIntent intent) : this(quickStorage, key, value)
        {
            Intent = intent;
        }
    }
}
