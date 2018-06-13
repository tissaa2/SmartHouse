using System;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;

namespace SmartHouse.ViewModels
{
    public class ListPageModel<T>: INotifyPropertyChanged, INotifyPropertyChanging 
    {
        // public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItemProperty", typeof(Project), typeof(ProjectsListPage));
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

        public ListPageModel(ObservableCollection<T> items)
        {
            Items = items;
        }
    }
}
