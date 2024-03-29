﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VOWsLauncher.Events;
using VOWsLauncher.MVVM.Model;

namespace VOWsLauncher.MVVM.ViewModel.SubWindows
{
    public class NewDocumentWindowViewModel : ObservableObject
    {
        public Globals Globals { get => Globals.Default; }

        public RelayCommand BrowseDirCommand { get; }
        public RelayCommand CreateCommand { get; }
        public RelayCommand CancelCommand { get; }

        #region XAMLBindings

        // Fields
        private string _documentName = "Unnamed Document";
        private string _rawDirectory = "";
        private bool _isVerticalSelected = true;
        private bool _isHorizontalSelected = false;

        // Modifiable properties
        /// <summary>
        /// The <c>DocumentName</c> property refers to the name for this document.
        /// This does not neccessarily need to follow Windows file name rules and will remain the name of the document in the Info tab,
        /// but the file name will be adjusted accordingly.
        /// </summary>
        public string DocumentName { get => _documentName; set => SetProperty(ref _documentName, value); }
        /// <summary>
        /// The <c>RawDirectory</c> property refers to the path leading to the directory for this new document.
        /// This path should be <c>absolute</c> and follow Windows File Directory format.
        /// </summary>
        public string RawDirectory { get => _rawDirectory; set => SetProperty(ref _rawDirectory, value); }
        /// <summary>
        /// The <c>IsVerticalSelected</c> property is binded to the <c>IsChecked</c> property of the 'Vertical' button.
        /// This is more for XAML use rather than general use, use <c>PageOrientation</c> instead.
        /// </summary>
        public bool IsVerticalSelected { get => _isVerticalSelected; set => SetProperty(ref _isVerticalSelected, value); }
        /// <summary>
        /// The <c>IsHorizontalSelected</c> property is binded to the <c>IsChecked</c> property of the 'Horizontal' button.
        /// This is more for XAML use rather than general use, use <c>PageOrientation</c> instead.
        /// </summary>
        public bool IsHorizontalSelected { get => _isHorizontalSelected; set => SetProperty(ref _isHorizontalSelected, value); }

        // Unmodifiable / helper properties
        /// <summary>
        /// The <c>Directory</c> property refers to the converted Uri of the directory for this new document.
        /// If <c>RawDirectory</c> is not a valid Uri, null will instead be returned.
        /// </summary>
        public Uri Directory
        {
            get
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Uri.TryCreate(RawDirectory, UriKind.Absolute, out Uri uri);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
                return uri;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
        /// <summary>
        /// The <c>Template</c> property refers to the current template for this new document.
        /// It is retrieved using inter-app messaging, and will default to an empty Template object if none is set.
        /// </summary>
        public Template Template
        {
            get
            {
                Template currentTemplate = WeakReferenceMessenger.Default.Send(new RetrieveSelectedTemplateMessage());
                return currentTemplate.IsWorkplace ? Template.Empty : currentTemplate;
            }
        }
        /// <summary>
        /// The <c>PageSize</c> property refers to the default page size for this new document.
        /// By default, this will match the Template, if selected, else the currently selected value of the ComboBox.
        /// </summary>
        public string PageSize { get => (string)PageSize_Selected.Content; }
        /// <summary>
        /// The <c>PageSize_Selected</c> property refers to the <c>ComboBoxItem</c> representing the default page size for this new document.
        /// By default, this will match the Template, if selected, else the currently selected value of the ComboBox.
        /// </summary>
        public ComboBoxItem PageSize_Selected { get; set; }
        /// <summary>
        /// The <c>PageSize_Elements</c> property refers to a list of <c>ComboBoxItem</c> objects that should be part of the
        /// Page Size combo box. This includes all of the default options, as well as the <c>Template</c> option if one is
        /// currently selected.
        /// </summary>
        public ObservableCollection<ComboBoxItem> PageSize_Elements
        {
            get
            {
                ObservableCollection<ComboBoxItem> comboBoxItems = new()
                {
                    QuickMake("Letter"),
                    QuickMake("A4"),
                    QuickMake("A3"),
                    QuickMake("Custom")
                };
                if(Template.PageSize != null)
                {
                    comboBoxItems.Insert(3, QuickMake("Template"));
                }
                return comboBoxItems;
            }
        }
        /// <summary>
        /// The <c>PageOrientation</c> property refers to the default page orientation for this new document.
        /// This will return either "vertical" or "horizonal" depending on which button is selected (default "vertical").
        /// </summary>
        public string PageOrientation
        {
            get
            {
                if (IsVerticalSelected) return "vertical";
                else return "horizontal";
            }
        }

        /// <summary>
        /// The <c>QuickMake</c> function handles the logic behind making a <c>ComboBoxItem</c> with content attached.
        /// This is a helper method that simplifies that process from 3 to 1 line.
        /// </summary>
        /// <param name="content">The content this <c>ComboBoxItem</c> will display.</param>
        /// <returns>The created <c>ComboBoxItem</c>.</returns>
        private ComboBoxItem QuickMake(string content)
        {
            ComboBoxItem item = new()
            {
                Content = content
            };
            return item;
        }

        #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public NewDocumentWindowViewModel(Window w)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            BrowseDirCommand = new(() =>
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                RawDirectory = WeakReferenceMessenger.Default.Send(new DisplayOpenSubmenuMessage(TargetType.DIRECTORY)) ?? "";
#pragma warning restore CS8601 // Possible null reference assignment.
            });
            CreateCommand = new(() =>
            {
                // TODO: Validation of data.
                w.DialogResult = true;
                w.Close();
            });
            CancelCommand = new(() =>
            {
                w.DialogResult = false;
                w.Close();
            });
        }
    }
}
