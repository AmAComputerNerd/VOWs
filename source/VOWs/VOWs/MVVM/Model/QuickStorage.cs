using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VOWs.Core;

namespace VOWs.MVVM.Model
{
    public class QuickStorage : ObservableObject
    {
        /// <summary>
        /// The static field exposing the current instance of QuickStorage.
        /// 
        /// <b>NOTE:</b> This field will be overriden as soon as a new QuickStorage object is created,
        /// but older versions can still be accessed if you have access to the object.
        /// </summary>
        public static QuickStorage? Instance;

        /// <summary>
        /// The private field storing the <c>StorageWrapper</c>, notably storing the location of the key in the database, identifier, value and whether it is readonly.
        /// </summary>
        private StorageWrapper<string>? _theme;
        /// <summary>
        /// The public field exposing the value of the private <c>StorageWrapper</c> field. The variable can be set as long as the value is not readonly.
        /// </summary>
        public string Theme
        {
            get
            {
                return _theme.Value;
            }
            set
            {
                if (_theme != null && _theme.ReadOnly == true) throw new ArgumentException();
                _theme = new StorageWrapper<string>("theme", "Theme", value, false);
            }
        }

        /// <summary>
        /// Constructor <c>QuickStorage</c> will create an instance of this object, populating values with those saved within the database.
        /// </summary>
        public QuickStorage()
        {
            RefreshValues();
            Instance = this;
        }

        /// <summary>
        /// Method <c>RefreshValue</c> will reload a variable matching the <c>identifier</c> by resetting the value from that within the database.
        /// </summary>
        /// <param name="identifier">The identifier that specifies which variable to refresh. This should match the <c>Name</c> variable in the <c>StorageWrapper</c> class.</param>
        public void RefreshValue(string identifier)
        {
            // TODO: Retrieve a specific value from the database based off the provided identifier.
        }

        /// <summary>
        /// Method <c>RefreshValues</c> will reload all variables in the file by resetting them to their saved value in the database.
        /// </summary>
        public void RefreshValues()
        {
            // TODO: Retrieve data values from the database to assign to all settings here.
            // Set Theme value.
            Theme = "Light";
        }

        /// <summary>
        /// Method <c>SaveValue</c> will save a variable matching the <c>identifier</c> to the database, overriding it.
        /// </summary>
        /// <param name="identifier">The identifier that specifies which variable to save. This should match the <c>Name</c> variable in the <c>StorageWrapper</c> class.</param>
        public void SaveValue(string identifier)
        {
            // TODO: Update a specific value in the database based off the provided identifier.
        }

        /// <summary>
        /// Method <c>SaveValues</c> will save all variables in the file to the database, overriding the existing values.
        /// </summary>
        public void SaveValues()
        {
            // TODO: Update the value of all settings within the database to match assigned values here.
        }
    }
}
