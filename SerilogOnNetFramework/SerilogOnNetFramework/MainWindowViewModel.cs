using Microsoft.Extensions.Logging;

namespace SerilogOnNetFramework
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ILogger logger)
        {
            Logger = logger;
        }

        private ILogger Logger { get; }

        public void OnInitialized(object o)
        {
            Logger.LogInformation("Begin {@param}", o);
        }
    }
}