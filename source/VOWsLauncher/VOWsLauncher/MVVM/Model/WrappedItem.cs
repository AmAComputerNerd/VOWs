using System;

namespace VOWsLauncher.MVVM.Model
{
    /// <summary>
    /// The <c>WrappedItem</c> class includes extra information on objects retrieved from the database.
    /// Such information includes the database path to reach the item, it's name (for when dealing with an ambiguous object), the value of the item and whether runtime edits should be
    /// allowed.
    /// </summary>
    /// <typeparam name="T">The type of this object.</typeparam>
    public class WrappedItem<T>
    {
        /// <summary>
        /// The <c>DatabasePath</c> property represents the exact pathway to reaching this item when moving from the root position in the database.
        /// </summary>
        public string DatabasePath { get; private set; }
        /// <summary>
        /// The <c>Name</c> property represents a name for this object. It doesn't need to match variable names, but generally should remain similar to it's name in the database.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The <c>Item</c> property represents the item itself - more so, it's value.
        /// </summary>
        public T Item { get; private set; }
        /// <summary>
        /// The <c>RuntimeCanEdit</c> property gives indication on whether a variable should be able to be changed during runtime.
        /// Typically, this only remains false for constants (stored in <c>Defaults</c>).
        /// </summary>
        public bool RuntimeCanEdit { get; private set; }

        /// <summary>
        /// The constructor for <c>WrappedItem</c> will create this object with the provided parameters.
        /// </summary>
        /// <param name="databasePath">The exact path to this item in the database, in line with syntax.</param>
        /// <param name="name">A name to give this object (doesn't affect the item, but merely gives it an identifier).</param>
        /// <param name="item">The value of this WrappedItem.</param>
        /// <param name="runtimeCanEdit">Whether editing this variable with <c>Set</c> or <c>TrySet</c> should be allowed.</param>
        public WrappedItem(string databasePath, string name, T item, bool runtimeCanEdit)
        {
            DatabasePath = databasePath;
            Name = name;
            Item = item;
            RuntimeCanEdit = runtimeCanEdit;
        }

        /// <summary>
        /// The <c>Set</c> method will change the value of this <c>WrappedItem</c> to a new value.
        /// <br></br>
        /// If this object cannot be edited due to <c>RuntimeCanEdit</c> being false, an <c>ArgumentException</c> will be thrown.
        /// </summary>
        /// <param name="item">The new value of the <c>WrappedItem</c>.</param>
        /// <exception cref="ArgumentException">This variable cannot be edited due to <c>RuntimeCanEdit</c> being set to <c>false</c>.</exception>
        public void Set(T item)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (!RuntimeCanEdit) throw new ArgumentException("Cannot edit WrappedItem of " + Item.ToString() + " during runtime.");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Item = item;
        }

        /// <summary>
        /// The <c>TrySet</c> method will try to change the value of this <c>WrappedItem</c> to a new value.
        /// </summary>
        /// <param name="item">The new value of the <c>WrappedItem</c>.</param>
        /// <param name="success">Whether the operation was a success.</param>
        public void TrySet(T item, out bool success)
        {
            if (RuntimeCanEdit) Item = item;
            success = RuntimeCanEdit; return;
        }
    }
}
