using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewerSample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private byte[] _selectedImage;

        public byte[] SelectedImage
        {
            get { return _selectedImage; }
            set { Set(ref _selectedImage, value);}
        }

        private ObservableCollection<byte[]> _images;

        public ReadOnlyObservableCollection<byte[]> Images { get; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Images = new ReadOnlyObservableCollection<byte[]>(_images);
        }

        protected virtual void Set<T>(ref T parameter, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(parameter, value)) return;

            parameter = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
