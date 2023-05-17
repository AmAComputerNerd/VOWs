using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.Core
{
    public class StorageWrapper<T>
    {
        public string DatabasePath { get; private set; }
        public string Name { get; private set; }
        public T Value { get; private set; }
        public bool ReadOnly { get; private set; }

        public StorageWrapper(string databasePath, string name, T value, bool readOnly)
        {
            DatabasePath = databasePath;
            Name = name;
            Value = value;
            ReadOnly = readOnly;
        }
    }
}
