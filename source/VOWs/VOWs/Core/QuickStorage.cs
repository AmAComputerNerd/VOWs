using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VOWs.Core
{
    public class QuickStorage : ObservableObject
    {
        private StorageWrapper<string> _theme;
        public string Theme
        {
            get
            {
                return _theme.Name;
            }
            set
            {
                if (_theme != null && _theme.ReadOnly == true) throw new ArgumentException();
                _theme = new StorageWrapper<string>("theme", "Theme", value, false);
            }
        }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public QuickStorage() 
        {
            RefreshValues();
        }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private void RefreshValue(string identifier)
        {
            // TODO: Retrieve a specific value from the database based off the provided identifier.
        }

        private void RefreshValues()
        {
            // TODO: Retrieve data values from the database to assign to all settings here.
            // Set Theme value.
            Theme = "Dark";
        }

        public void SaveValue(string identifier)
        {
            // TODO: Update a specific value in the database based off the provided identifier.
        }

        public void SaveValues()
        {
            // TODO: Update the value of all settings within the database to match assigned values here.
        }
    }
}
