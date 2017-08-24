using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataTemplateSelectorSample.Models;
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
            // 引数で渡される前提とするがここでは省略
            var entries = new List<Entry>();
            entries.Add(new Entry { EntryClass = EntryClass.Address });
            entries.Add(new Entry { EntryClass = EntryClass.Name });

            var entryViewModels = new List<IEntryViewModel>();
            foreach (var entry in entries)
            {
                entryViewModels.Add(EntryViewModelFactory.CreateViewModel(entry));
            }
            Entries = entryViewModels;
        }
    }
}
