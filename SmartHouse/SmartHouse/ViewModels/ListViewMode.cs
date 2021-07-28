using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using SmartHouse.Models;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SmartHouse.ViewModels
{
    public class ListViewModel<T>: ViewModel
    {

        private T selectedItem;
        public T SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                OnPropertyChanging("SelectedItem");
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<T> items;
        public ObservableCollection<T> Items
        {
            get
            {
                return items;
            }

            set
            {
                OnPropertyChanging("Items");
                if (items != value)
                {
                    Setup(items);
                }
                OnPropertyChanged("Items");
            }
        }

        public bool IsChanged { get; set; } = false;

        private void CollectionChangeHandler(object s, NotifyCollectionChangedEventArgs e)
        {
            IsChanged = true;
        }

        private void Setup(ObservableCollection<T> value)
        {
            if (items != value)
            {
                if (items != null)
                    items.CollectionChanged -= CollectionChangeHandler;
                items = value;
                items.CollectionChanged -= CollectionChangeHandler;
                items.CollectionChanged += CollectionChangeHandler;
            }
        }

        public ListViewModel()
        {
        }

        public ListViewModel(ICollection<T> items)
        {
            if (items == null)
                Items = new ObservableCollection<T>();
            else
                Items = new ObservableCollection<T>(items);
        }
    }
}
