using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace VOWs.MVVM.Model.Data
{
    public abstract class DataObject : ObservableRecipient
    {
        // Fields.
        private bool _compatibilityMode;
        private bool _textOnlyMode;
        private bool _readOnlyMode;

        // Properties.
        /// <summary>
        /// The abstract <c>Info</c> property represents the <c>DataObjectInfo</c> for the implementing class.
        /// </summary>
        public abstract DataObjectInfo Info { get; }
        /// <summary>
        /// The abstract <c>Location</c> property represents the path to the location of the data object.
        /// </summary>
        public abstract Uri Location { get; set; }
        /// <summary>
        /// The <c>CompatibilityMode</c> property represents whether Compatibility Mode is enabled for this DataObject.
        /// In Compatibility Mode, custom VOWs features such as version control will be disabled.
        /// </summary>
        public bool CompatibilityMode { get => _compatibilityMode; set => SetProperty(ref _compatibilityMode, value); }
        /// <summary>
        /// The <c>TextOnlyMode</c> property represents whether Text-Only Mode is enabled for this DataObject.
        /// In Text-Only Mode, all media-related features will be disabled.
        /// </summary>
        public bool TextOnlyMode { get => _textOnlyMode; set => SetProperty(ref _textOnlyMode, value); }
        /// <summary>
        /// The <c>ReadOnlyMode</c> property represents whether Read-Only Mode is enabled for this DataObject.
        /// In Read-Only Mode, all editing features will be disabled - version control may still be available in some
        /// respect, presuming <c>CompatibilityMode</c> is not enabled.
        /// </summary>
        public bool ReadOnlyMode { get => _readOnlyMode; set => SetProperty(ref _readOnlyMode, value); }
    }

    public class DataObjectInfo : ObservableObject
    {
        // Fields.
        private string _name;
        private ExtensionType _extension;

        // Properties.
        /// <summary>
        /// The <c>Name</c> property represents the display-friendly name for the implementing object.
        /// </summary>
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        /// <summary>
        /// The <c>Extension</c> property represents an <c>ExtensionType</c> object containing default
        /// details on an extension type (e.g. matching extensions, compatibility mode, etc.).
        /// </summary>
        public ExtensionType Extension { get => _extension; set => SetProperty(ref _extension, value); }
    }
}
