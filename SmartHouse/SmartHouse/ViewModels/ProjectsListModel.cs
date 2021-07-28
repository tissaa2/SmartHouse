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

    public class ProjectsListModel : IconNamedListViewModel<ProjectModel>
    {
        public ProjectsListModel(ProjectsList source) : base(source.Items.Select(e => new ProjectModel(e)).ToArray(), null, null)
        {
            Target = source;
        }
    }
}
