using System;

namespace SplitViewModelAssembly.ViewModels
{
    public static class ViewTypeToViewModelTypeResolver
    {
        public static Type Resolve(Type viewType)
        {
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));

            // ReSharper disable once PossibleNullReferenceException
            var vmTypeName = $"{viewType.Namespace.Replace("Views", "ViewModels")}.{viewType.Name}ViewModel";
            var vmType = typeof(ViewTypeToViewModelTypeResolver).Assembly.GetType(vmTypeName);
            return vmType;
        }
    }
}
