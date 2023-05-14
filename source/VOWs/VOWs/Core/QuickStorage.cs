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
        private StorageWrapper<Image> _imageContext;
        private StorageWrapper<VideoDrawing> _videoContext;
        public string Theme 
        { 
            get { return _theme.Value; } 
            set 
            {
                if (_theme.Intent == StorageIntent.LocalNone || _theme.Intent == StorageIntent.LocalReadOnly) return;
                _theme = new StorageWrapper<string>(this, _theme.Key, value);
            } 
        }
        public Image ImageContext
        {
            get { return _imageContext.Value; }
            set
            {
                if (_imageContext.Intent == StorageIntent.LocalNone || _imageContext.Intent == StorageIntent.LocalReadOnly) return;
                _imageContext = new StorageWrapper<Image>(this, _imageContext.Key, value);
            }
        }
        public VideoDrawing VideoContext
        {
            get { return _videoContext.Value; }
            set
            {
                if (_videoContext.Intent == StorageIntent.LocalNone || _videoContext.Intent == StorageIntent.LocalReadOnly) return;
                _videoContext = new StorageWrapper<VideoDrawing>(this, _videoContext.Key, value);
            }
        }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public QuickStorage() 
        {
            LoadValues();
        }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public void LoadValue(string identifier)
        {
            // TODO: Retrieve a specific value from the database based off the provided identifier.
        }

        public void LoadValues()
        {
            // TODO: Retrieve data values from the database to assign to all settings here.
            // Set Theme value.
            _theme = new StorageWrapper<string>(this, "Theme", "Dark", StorageIntent.All);
            // Set ImageContext to null - until an image is selected, this shouldn't be a thing.
            _imageContext = new StorageWrapper<Image>(this, "ImageContext", null, StorageIntent.LocalOnly);
            // Set VideoContext to null - until a video is selected, this shouldn't be a thing.
            _videoContext = new StorageWrapper<VideoDrawing>(this, "VideoContext", null, StorageIntent.LocalOnly);
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
