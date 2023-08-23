using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows;
using VOWs.Events;
using VOWs.MVVM.Model;
using VOWs.MVVM.Model.Data;

namespace VOWs.MVVM.ViewModel.SubWindows
{
    public class InfoWindowViewModel : ObservableObject
    {
        public Globals Globals { get => Globals.Default; }

        public string Name { get => WeakReferenceMessenger.Default.Send(new RequestDataObjectMessage()).Response.Info.Name; }
        public string FullLocation { get => Globals.CommandLineArgs.SourcePath?.AbsolutePath ?? "null"; }
        public string WorkEnvironment { get => ExtensionUtils.GetMeta(Globals.CommandLineArgs.SourcePathExtensionType).IsWorkspaceExtension ? "Workplace (" + Globals.CommandLineArgs.SourcePathExtensionType.ToString() + ")" : "Document (" + Globals.CommandLineArgs.SourcePathExtensionType.ToString() + ")"; }
        public Visibility NoRestrictionsTextVisibility { get => CompatibilityTextVisibility.Equals(Visibility.Collapsed) && TextOnlyTextVisibility.Equals(Visibility.Collapsed) && ReadOnlyTextVisibility.Equals(Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed; }
        public Visibility CompatibilityTextVisibility { get => WeakReferenceMessenger.Default.Send(new RequestEditorModesMessage()).CompatibilityMode ? Visibility.Visible : Visibility.Collapsed; }
        public Visibility TextOnlyTextVisibility { get => WeakReferenceMessenger.Default.Send(new RequestEditorModesMessage()).TextOnlyMode ? Visibility.Visible : Visibility.Collapsed; }
        public Visibility ReadOnlyTextVisibility { get => WeakReferenceMessenger.Default.Send(new RequestEditorModesMessage()).ReadOnlyMode ? Visibility.Visible : Visibility.Collapsed; }

        public DynamicURIResolver Logo { get; }

        public InfoWindowViewModel()
        {
            Logo = new("pack://application:,,,/Resources/Images/VOWsuite-logos_white.png",
                "pack://application:,,,/Resources/Images/VOWsuite-logos_black.png",
                new Uri("/Resources/Images/VOWsuite-logos_black.png",
                UriKind.Relative));
        }
    }
}
