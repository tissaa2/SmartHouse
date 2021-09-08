using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class ProjectsListModel : IconNamedModel
    {
        private ListViewModel<ProjectModel> scenes = new ListViewModel<ProjectModel>(null);
        public ListViewModel<ProjectModel> Scenes
        {
            get
            {
                return scenes;
            }
            set
            {
                OnPropertyChanging("Scenes");
                scenes = value;
                OnPropertyChanged("Scenes");
            }
        }

        public ProjectsListModel(ProjectsList target) : base(target.Name, target.Icon, target)
        {
        }
    }
}
