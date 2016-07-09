using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommandSample.Annotations;

namespace CommandSample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _value1;

        public string Value1
        {
            get { return _value1; }
            set { _value1 = value; }
        }

        private string _value2;

        public string Value2
        {
            get { return _value2; }
            set { _value2 = value; }
        }

        private int _result;

        public int Result
        {
            get { return _result; }
            set
            {
                if (!Object.Equals(_result, value))
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ExecuteCommand { get; }

        public MainWindowViewModel()
        {
            ExecuteCommand = 
                new RelayCommand(
                    () => Result = int.Parse(Value1) + int.Parse(Value2),
                    CanExecute);
        }

        private bool CanExecute()
        {
            int temp;
            return int.TryParse(Value1, out temp) && int.TryParse(Value2, out temp);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
