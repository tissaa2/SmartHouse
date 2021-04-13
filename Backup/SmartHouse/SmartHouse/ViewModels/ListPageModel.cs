using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{

    public class ListPageModel<T>: IconNamedListViewModel<T>  
    {
        public ListPageModel(ICollection<T> items, string icon, string name): base(items, icon, name)
        {
        }
    }
}
