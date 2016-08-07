using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace NotifyProcessingSample
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool _processing;
        public bool Processing
        {
            get { return _processing; }
            set { Set(ref _processing, value); }
        }
        public ICommand HeavyCommand { get; }

        public MainWindowViewModel()
        {
            HeavyCommand = new RelayCommand(async () =>
            {
                Processing = true;
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                finally
                {
                    Processing = false;
                }
            });
        }
    }
}
