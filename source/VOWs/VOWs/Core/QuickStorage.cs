using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VOWs.Core
{
    public class QuickStorage : ObservableObject
    {
        private StorageWrapper<string> _theme;
        private StorageWrapper<ObservableCollection<Button>> _writingDisplayTopButtons;
        public string Theme 
        { 
            get { return _theme.Value; } 
            set 
            {
                if (_theme.Intent == StorageIntent.LocalNone || _theme.Intent == StorageIntent.LocalReadOnly) return;
                _theme = new StorageWrapper<string>(this, _theme.Key, value);
            } 
        }
        public ObservableCollection<Button> WritingDisplayTopButtons
        {
            get { return _writingDisplayTopButtons.Value; }
            set
            {
                if (_writingDisplayTopButtons.Intent == StorageIntent.LocalNone || _writingDisplayTopButtons.Intent == StorageIntent.LocalReadOnly) return;
                _writingDisplayTopButtons = new StorageWrapper<ObservableCollection<Button>>(this, _writingDisplayTopButtons.Key, value);
            }
        }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public QuickStorage() 
        {
            loadValues();
        }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public void loadValue(string identifier)
        {
            // TODO: Retrieve a specific value from the database based off the provided identifier.
        }

        public void loadValues()
        {
            // TODO: Retrieve data values from the database to assign to all settings here.
            // Set Theme value.
            _theme = new StorageWrapper<string>(this, "Theme", "Dark", StorageIntent.All);
            // Set WritingDisplayTopButtons value.
            ObservableCollection<Button> writingDisplayTopButtons = new ObservableCollection<Button>();
            Button button;
            for(int i = 0; i < 4; i++)
            {
                button = new Button();
                button.Content = "EEE" + i;
                button.MaxWidth = 200;
                writingDisplayTopButtons.Add(button);
            }
            _writingDisplayTopButtons = new StorageWrapper<ObservableCollection<Button>>(this, "WritingDisplayTopButtons", writingDisplayTopButtons, StorageIntent.LocalOnly);
        }

        public void saveValue(string identifier)
        {
            // TODO: Update a specific value in the database based off the provided identifier.
        }

        public void saveValues()
        {
            // TODO: Update the value of all settings within the database to match assigned values here.
        }
    }
}
