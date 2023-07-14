using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Text.RegularExpressions;
using System.Windows;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        /// <summary>
        /// The <c>SaveSettingsCommand</c> command will update the settings of the <c>Globals</c> file to match that which is recorded in here.
        /// </summary>
        public RelayCommand SaveSettingsCommand { get; }

        public bool IsBlackTheme { get; set; }
        public bool IsDarkTheme { get; set; }
        public bool IsLightTheme { get; set; }
        public bool IsWhiteTheme { get; set; }
        public string AccentHex { get; set; }
        public bool IsHighContrast { get; set; }
        public bool IsLargerText { get; set; }
        public bool IsBeginnerTips { get; set; }
        public bool IsRibbonVersionControl { get; set; }
        public bool IsDockedVersionControl { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SettingsViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            // Assign function for the SaveSettingsCommand.
            SaveSettingsCommand = new(() =>
            {
                WeakReferenceMessenger.Default.Send(new LogMessage("Begin application settings update.", ToString(), LogLevel.INFO));
                // Save Theme.
                string theme = "Dark";
                if (IsBlackTheme) theme = "Black";
                else if (IsDarkTheme) theme = "Dark";
                else if (IsLightTheme) theme = "Light";
                else if (IsWhiteTheme) theme = "White";
                if (!Globals.Theme.Equals(theme))
                {
                    WeakReferenceMessenger.Default.Send(new LogMessage("A change in the 'Theme' setting has been reported. Old value - '" + Globals.Theme + "'. New value - '" + theme + "'."));
                    Globals.Theme = theme;
                }
                // Save Accent.
                if (AccentHex == null || (!Regex.IsMatch(AccentHex, @"[#][0-9A-Fa-f]{6}\b") && !AccentHex.Equals("")))
                {
                    // This is an invalid accent hex.
                    WeakReferenceMessenger.Default.Send(new LogMessage("Attempted settings change for 'Accent' was unsuccessful: invalid hexadecimal value (" + (AccentHex ?? "null") + ")", ToString(), LogLevel.WARNING));
                    MessageBox.Show("The value for 'Accent' was invalid. The setting was not changed.", "Settings - Invalid Value");
                } 
                else
                {
                    if (!AccentHex.Equals("") && !Globals.Accent.Equals(AccentHex))
                    {
                        WeakReferenceMessenger.Default.Send(new LogMessage("A change in the 'Accent' setting has been reported. Old value - '" + Globals.Accent + "'. New value - '" + AccentHex + "'."));
                        Globals.Accent = AccentHex;
                    }
                }
                // Save High Contrast.
                if (!Globals.UseHighContrast.Equals(IsHighContrast))
                {
                    WeakReferenceMessenger.Default.Send(new LogMessage("A change in the 'UseHighContrast' setting has been reported. Old value - '" + Globals.UseHighContrast + "'. New value - '" + IsHighContrast + "'."));
                    Globals.UseHighContrast = IsHighContrast;
                }
                // Save Larger Text.
                if (!Globals.UseLargeText.Equals(IsLargerText))
                {
                    WeakReferenceMessenger.Default.Send(new LogMessage("A change in the 'UseLargeText' setting has been reported. Old value - '" + Globals.UseLargeText + "'. New value - '" + IsLargerText + "'."));
                    Globals.UseLargeText = IsLargerText;
                }
                // Save Beginners Tips.
                if (!Globals.ShowBeginnersText.Equals(IsBeginnerTips))
                {
                    WeakReferenceMessenger.Default.Send(new LogMessage("A change in the 'ShowBeginnersText' setting has been reported. Old value - '" + Globals.ShowBeginnersText + "'. New value - '" + IsBeginnerTips + "'."));
                    Globals.ShowBeginnersText = IsBeginnerTips;
                }
                // Save Version Control location.
                if (IsRibbonVersionControl && !Globals.UseTabsForVersionControl.Equals(true))
                {
                    WeakReferenceMessenger.Default.Send(new LogMessage("A change in the 'UseTabsForVersionControl' setting has been reported. Old value - 'false'. New value - 'true'."));
                    Globals.UseTabsForVersionControl = true;
                }
                else if (IsDockedVersionControl && !Globals.UseTabsForVersionControl.Equals(false))
                {
                    WeakReferenceMessenger.Default.Send(new LogMessage("A change in the 'UseTabsForVersionControl' setting has been reported. Old value - 'true'. New value - 'false'."));
                    Globals.UseTabsForVersionControl = false;
                }
                // Show MessageBox confirming settings update.
                WeakReferenceMessenger.Default.Send(new LogMessage("Finished updating application settings.", ToString(), LogLevel.INFO));
                MessageBox.Show("Successfully updated settings!\nSome settings may require a restart to take effect!", "Settings - Success!");
                // Kick the user back to the home screen.
                WeakReferenceMessenger.Default.Send(new ChangeViewMessage(0));
            });
            // Assign default values for variables.
            ResetValues();
        }

        public void ResetValues()
        {
            IsBlackTheme = Globals.Theme.Equals("Black");
            IsDarkTheme = Globals.Theme.Equals("Dark");
            IsLightTheme = Globals.Theme.Equals("Light");
            IsWhiteTheme = Globals.Theme.Equals("White");
            AccentHex = Globals.Accent;
            IsHighContrast = Globals.UseHighContrast;
            IsLargerText = Globals.UseLargeText;
            IsBeginnerTips = Globals.ShowBeginnersText;
            IsRibbonVersionControl = Globals.UseTabsForVersionControl;
            IsDockedVersionControl = !Globals.UseTabsForVersionControl;
        }
    }
}
