using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandSample
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// 実行可能状態の変更を通知する
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 常に実行可能な新しいコマンドを作成します。
        /// </summary>
        /// <param name="execute">実行ロジック</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 新しいコマンドを作成します。
        /// </summary>
        /// <param name="execute">コマンドの実行処理</param>
        /// <param name="canExecute">実行可能状態の判定処理</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// コマンドが実行可能かどうか判定します。
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>この実行可能な場合は true、それ以外の場合は false。</returns>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _execute();

        /// <summary>
        /// コマンドの実行状態が変更されたことを通知します。
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
