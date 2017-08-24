using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace DataTemplateSelectorSample.ViewModels
{
    public class MainWindowViewModel
    {
        public IReadOnlyCollection<IEntryViewModel> Entries { get; set; }

        public ICommand RegisterCommand => new DelegateCommand(() =>
        {
            foreach (var entry in Entries)
            {
                switch (entry)
                {
                    case AddressEntryViewModel addressEntryViewModel:
                        Debug.WriteLine($"PostalCode:{addressEntryViewModel.PostalCode}");
                        break;
                    case NameEntryViewModel nameEntryViewModel:
                        Debug.WriteLine($"FirstName:{nameEntryViewModel.FirstName} LastName:{nameEntryViewModel.LastName}");
                        break;
                }
            }
        });

        public MainWindowViewModel()
        {
            var entries = new List<IEntryViewModel>();
            entries.Add(new AddressEntryViewModel { PostalCode = "100-1234" });
            entries.Add(new NameEntryViewModel { FirstName = "太郎", LastName = "日本" });
            Entries = entries;
        }
    }
}
