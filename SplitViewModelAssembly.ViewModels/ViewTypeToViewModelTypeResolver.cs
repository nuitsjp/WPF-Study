using System;
using System.Reflection;

namespace SplitViewModelAssembly.ViewModels
{
    public static class ViewTypeToViewModelTypeResolver
    {
        private static readonly Assembly LocalAssembly = typeof(ViewTypeToViewModelTypeResolver).Assembly;
        public static Type Resolve(Type viewType)
        {
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));

            // ReSharper disable once PossibleNullReferenceException
            var vmTypeName = $"{viewType.Namespace.Replace("Views", "ViewModels")}.{viewType.Name}ViewModel";
            return LocalAssembly.GetType(vmTypeName);
        }
    }
}
