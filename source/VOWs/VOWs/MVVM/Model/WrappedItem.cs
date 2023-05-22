using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.MVVM.Model
{
    public class WrappedItem<T>
    {
        public string DatabasePath { get; private set; }
        public string Name { get; private set; }
        public T Item { get; private set; }
        public bool RuntimeCanEdit { get; private set; }

        public WrappedItem(string databasePath, string name, T item, bool runtimeCanEdit)
        {
            DatabasePath = databasePath;
            Name = name;
            Item = item;
            RuntimeCanEdit = runtimeCanEdit;
        }

        public void Set(T item)
        {
            if (!RuntimeCanEdit) throw new ArgumentException("Cannot edit WrappedItem of " + Item.ToString() + " during runtime.");
            Item = item;
        }

        public void TrySet(T item, out bool success)
        {
            if (RuntimeCanEdit) Item = item;
            success = RuntimeCanEdit; return;
        }
    }
}
