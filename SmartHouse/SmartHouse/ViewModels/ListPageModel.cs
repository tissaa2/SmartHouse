using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class ListPageModel<T>: INotifyPropertyChanged, INotifyPropertyChanging 
    {
        private object target;
        public object Target
        {
            get
            {
                return target;
            }
            set
            {
                OnPropertyChanging("Target");
                target = value;
                OnPropertyChanged("Target");
            }
        }

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

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnPropertyChanging(string name)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        }

        public ListPageModel(ObservableCollection<T> items/* , ViewEditTemplateSelector templateSelector */)
        {
            Items = items;
            // TemplateSelector = templateSelector;
        }
    }
}
