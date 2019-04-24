using System;
using System.Threading.Tasks;
using PropertyChanged;
using Reactive.Bindings;

namespace ProcessingStudy
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        public bool IsNotProcessing { get; set; } = true;

        public AsyncReactiveCommand HeavyCommand { get; }

        public MainWindowViewModel()
        {
            HeavyCommand = new AsyncReactiveCommand();
            HeavyCommand.Subscribe(HeavyProcess);
        }

        private async Task HeavyProcess()
        {
            IsNotProcessing = false;
            await Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                IsNotProcessing = true;
            });
        }
    }
}