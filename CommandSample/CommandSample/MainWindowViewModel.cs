using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommandSample.Annotations;
using Prism.Commands;

namespace CommandSample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (OnPropertyChanged(ref _name, value))
                    ExecuteCommand.RaiseCanExecuteChanged();
            }
        }
        public string Address { get; set; }
        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                OnPropertyChanged(ref _result, value);
            }
        }

        public DelegateCommand ExecuteCommand { get; }

        public MainWindowViewModel()
        {
            ExecuteCommand = 
                new DelegateCommand(
                    () =>　Result = "登録されました",
                    CanExecute);
        }

        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual bool OnPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
