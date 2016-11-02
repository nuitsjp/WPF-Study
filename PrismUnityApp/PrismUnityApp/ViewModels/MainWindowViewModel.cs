using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismUnityApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand InitializeCommand => new DelegateCommand(Initialize);

        private void Initialize()
        {
            _regionManager.RequestNavigate("ContentRegion", "FirstPage");
        }

        private readonly IRegionManager _regionManager;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
