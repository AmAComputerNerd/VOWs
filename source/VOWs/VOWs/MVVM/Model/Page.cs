using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOWs.MVVM.Model
{
    public class Page : ObservableRecipient
    {
        private string _size;
        private int _cursorLineNum;
        private int _cursorLinePos;

        public string Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
        public int CursorLineNum
        {
            get => _cursorLineNum;
            set => SetProperty(ref _cursorLineNum, value);
        }
    }
}
