using System;
using System.Threading;
using System.Threading.Tasks;
using PropertyChanged;
using Reactive.Bindings;

namespace NonBrockingProcessingStudy
{
    public class MainWindowViewModel
    {
        private AsyncTaskService AsyncTaskService { get; } = new AsyncTaskService();
        public ReactiveProperty<bool> IsProcessing { get; } = new ReactiveProperty<bool>();

        public ReactiveCommand HeavyCommand { get; } = new ReactiveCommand();

        public ReactiveCommand ClosingCommand { get; } = new ReactiveCommand();

        public MainWindowViewModel()
        {
            HeavyCommand.Subscribe(HeavyProcess);
            ClosingCommand.Subscribe(() =>
            {
                AsyncTaskService.WaitAll();
            });
        }

        private void HeavyProcess()
        {
            IsProcessing.Value = true;
            AsyncTaskService.Run(() =>
            {
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                IsProcessing.Value = false;
            });
        }
    }
}