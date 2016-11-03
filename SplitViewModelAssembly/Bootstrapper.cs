using Microsoft.Practices.Unity;
using Prism.Unity;
using SplitViewModelAssembly.Views;
using System.Windows;
using Prism.Mvvm;
using SplitViewModelAssembly.ViewModels;

namespace SplitViewModelAssembly
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(ViewTypeToViewModelTypeResolver.Resolve);
        }
    }
}
