using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.MVVM.Model
{
    public class DatabaseWrapper
    {
        // TODO: Add Database link and autocreation.
        public WrappedItem<string> WrappedTheme { get; private set; }
        public string Theme
        {
            get
            {
                return WrappedTheme.Item;
            }
            set
            {
                WrappedTheme.Set(value);
            }
        }
    
        public DatabaseWrapper()
        {
            AssignValues();
        }

        private void AssignValues()
        {
            // TODO: Retrieve values and parse from database instead of just dummy values.
            WrappedTheme = new WrappedItem<string>("app.theme", "Theme", "Light", true);
        }
    
        public void RefreshValues()
        {
            // TODO: Refresh values by retrieving them anew from database. This method can throw an exception if the database isn't available or a value can't be found.
            AssignValues();
        }

        public void SaveValues()
        {
            // TODO: Save current instances of values to the database.
        }
    }
}
