using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ByteArrayToImageSourceConverter
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private byte[] _image;
        public byte[] Image {
            get
            {
                return _image;
            }

            set
            {
                Set(ref _image, value);
            }
        }

        public ObservableCollection<byte[]> Images = new ObservableCollection<byte[]>();

        public MainWindowViewModel()
        {
            var image = File.ReadAllBytes("report.jpg");
            for (int i = 0; i < 1000; i++)
            {
                Images.Add(image);
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Set<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value)) return;

            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
