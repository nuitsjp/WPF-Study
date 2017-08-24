using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTemplateSelectorSample.Models;

namespace DataTemplateSelectorSample.ViewModels
{
    public static class EntryViewModelFactory
    {
        public static IEntryViewModel CreateViewModel(Entry entry)
        {
            switch (entry.EntryClass)
            {
                case EntryClass.Address:
                    return new AddressEntryViewModel();
                case EntryClass.Name:
                    return new NameEntryViewModel();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
