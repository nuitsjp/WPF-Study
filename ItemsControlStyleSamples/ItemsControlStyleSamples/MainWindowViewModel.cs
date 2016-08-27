using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ItemsControlStyleSamples
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ImageSource> Images { get;  } = new ObservableCollection<ImageSource>();
    }
}
