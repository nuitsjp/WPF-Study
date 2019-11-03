using System;
using System.Windows;
using System.Windows.Input;

namespace ChromeWindowWithMaterialDesignThemes
{
    public class ChromeWindowCommands
    {
        public static ICommand MinimizeCommand => new Command(OnMinimize);

        public static ICommand ChangeWindowStateCommand => new Command(OnChangeWindowState);

        public static ICommand CloseWindowCommand => new Command(OnClose);


        private static void OnChangeWindowState(object obj)
        {
            var parentWindow = Window.GetWindow((DependencyObject)obj);
            if (parentWindow != null)
            {
                switch (parentWindow.WindowState)
                {
                    case WindowState.Maximized:
                        parentWindow.WindowState = WindowState.Normal;
                        break;
                    case WindowState.Normal:
                        parentWindow.WindowState = WindowState.Maximized;
                        break;
                    case WindowState.Minimized:
                        parentWindow.WindowState = WindowState.Normal;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private static void OnMinimize(object obj)
        {
            var parentWindow = Window.GetWindow((DependencyObject)obj);
            if(parentWindow != null)
                parentWindow.WindowState = WindowState.Minimized;
        }


        private static void OnClose(object obj)
        {
            var parentWindow = Window.GetWindow((DependencyObject)obj);
            parentWindow?.Close();
        }

        public class Command : ICommand
        {
            private static readonly Func<object, bool> ReturnTrue = _ => true;

            private readonly Action<object> _execute;

            private readonly Func<object, bool> _canExecute;

            public Command(Action<object> execute) : this(execute, ReturnTrue)
            {
            }

            public Command(Action<object> execute, Func<object, bool> canExecute)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute(parameter);

            public void Execute(object parameter) => _execute(parameter);

            public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            public event EventHandler CanExecuteChanged;
        }
    }
}