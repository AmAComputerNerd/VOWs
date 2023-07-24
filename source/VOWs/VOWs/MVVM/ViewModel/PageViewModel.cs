using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using VOWs.MVVM.Model;
using VOWs.MVVM.Model.Data;

namespace VOWs.MVVM.ViewModel
{
    public partial class PageViewModel : ObservableRecipient
    {
        // Copies of global resources relevant to the PageView.
        /// <summary>
        /// The <c>Globals</c> property refers to an object full of shared variables across multiple classes.
        /// These may be read-only, or be able to be edited across classes. The <c>Globals</c> class is an <c>ObservableObject</c>.
        /// </summary>
        public Globals Globals { get => Globals.Default; }

        public string Example_Type
        {
            get
            {
                if (Globals.CommandLineArgs.SourcePath == null)
                {
                    return "null - you opened the application by itself!";
                }
                else if (Globals.CommandLineArgs.SourcePath.AbsolutePath.EndsWith(".vdoc"))
                {
                    return "Document! (.vdoc)";
                }
                else if (Globals.CommandLineArgs.SourcePath.AbsolutePath.EndsWith(".vwsp"))
                {
                    return "Workspace! (.vwsp)";
                }
                else
                {
                    return "Something random! (" + new FileInfo(Globals.CommandLineArgs.SourcePath.AbsolutePath).Extension + ")";
                }
            }
        }
        public string Example_Location { get => Globals.CommandLineArgs.SourcePath == null ? "Nowhere!" : Globals.CommandLineArgs.SourcePath.AbsolutePath; }
        public RelayCommand Example_DefaultAccentCommand;
        public RelayCommand Example_RedAccentCommand;
        public RelayCommand Example_GreenAccentCommand;
        public RelayCommand Example_YellowAccentCommand;

        /// <summary>
        /// The constructor for <c>PageViewModel</c> initialises variables relevant to <c>PageView</c>.
        /// </summary>
        public PageViewModel()
        {
            Example_DefaultAccentCommand = new(() =>
            {
                Globals.Accent = "#5da1c0";
            });
            Example_RedAccentCommand = new(() =>
            {
                Globals.Accent = "#f47174";
            });
            Example_GreenAccentCommand = new(() =>
            {
                Globals.Accent = "#bfe3b4";
            });
            Example_YellowAccentCommand = new(() =>
            {
                Globals.Accent = "#f4f186";
            });
        }
    }
}
