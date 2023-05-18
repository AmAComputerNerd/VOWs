using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.MVVM.Model
{
    public class StorageWrapper<T>
    {
        /// <summary>
        /// The path leading to the key of the wrapped object within the database.
        /// </summary>
        public string DatabasePath { get; private set; }
        /// <summary>
        /// The name of the wrapped object, usually the same as the variable in <c>QuickStorage</c> or another file.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The value of the wrapped object.
        /// </summary>
        public T Value { get; private set; }
        /// <summary>
        /// Whether the object is readonly, and thus whether it can or can't be edited.
        /// </summary>
        public bool ReadOnly { get; private set; }

        /// <summary>
        /// Constructor <c>StorageWrapper</c> will create a new object based off the entered values.
        /// </summary>
        /// <param name="databasePath">The path to this value in the database.</param>
        /// <param name="name">The name to use for this value, not neccesarily the same as in the database.</param>
        /// <param name="value">The value that this <c>StorageWrapper</c> holds.</param>
        /// <param name="readOnly">Whether this <c>StorageWrapper</c> should be able to be edited.</param>
        public StorageWrapper(string databasePath, string name, T value, bool readOnly)
        {
            DatabasePath = databasePath;
            Name = name;
            Value = value;
            ReadOnly = readOnly;
        }
    }
}
