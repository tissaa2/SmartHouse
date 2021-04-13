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

    public class IconNamedListViewModel<T>: IconNamedModel
    {

        // public ViewEditTemplateSelector TemplateSelector { get; set; }
        private T selectedItem;
        public T SelectedItem
        {
            get
            {
                return selectedItem;
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {
                OnPropertyChanging("SelectedItem");
                selectedItem = value;
                // TemplateSelector?.SetEditedItem(value);
                OnPropertyChanged("SelectedItem");
                // SetValue(SelectedItemProperty, value); 
            }
        }

        private ObservableCollection<T> items;
        public ObservableCollection<T> Items
        {
            get
            {
                return items;
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {
                OnPropertyChanging("Items");
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public IconNamedListViewModel()
        {
        }

        public IconNamedListViewModel(ICollection<T> items, string icon, string name): base(icon, name)
        {
            if (items == null)
                Items = new ObservableCollection<T>();
            else    
                Items = new ObservableCollection<T>(items);
            // TemplateSelector = templateSelector;
        }
    }
}
