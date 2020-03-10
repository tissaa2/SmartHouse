using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class ListPageModel<T>: IconNamedListViewModel<T> 
    {
        public ListPageModel(ObservableCollection<T> items/* , ViewEditTemplateSelector templateSelector */): base(items)
        {
            // Items = items;
        }
    }
}
