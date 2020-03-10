using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class ProjectsListModel : IconNamedListViewModel<Project>
    {
        public ProjectsListModel(): base(new ObservableCollection<Project>())
        {
        }

        public ProjectsListModel(ObservableCollection<Project> items) : base(items)
        {
        }
    }
}
