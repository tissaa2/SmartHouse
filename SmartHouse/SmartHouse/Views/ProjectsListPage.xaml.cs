using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Plugin.Media;
using SmartHouse.Services;
using SmartHouse.Models;
using SmartHouse.Models.Logic;
using SmartHouse.ViewModels;

namespace SmartHouse.Views
{
    public partial class ProjectsListPage : ContentPage
    {
        public static ProjectsListPage Instance = null;
        public ListPageModel<Project> Model { get; set;}

        public ProjectsListPage()
        {
            Instance = this;
            BindingContext = Model = new ListPageModel<Project>(ProjectsList.Instance.Items);
            this.InitializeComponent();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            ProjectsList.Instance.Items.Add(new Project() { Name= "Новый проект", ID = ProjectsList.IntID.NewID(), Icon = "home.png" });
        }

        private void ProjectsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Model.SelectedItem != e.Item)
            {
                EditorRow.Height = 48;
                Model.SelectedItem = e.Item as Project;
            }
            else
            {
                MainPage.Instance.CurrentPage = ProjectPage.Instance;
                ProjectPage.Instance.SetTarget((e.Item as Project));
            }

        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            var f = await ProjectMedia.GetPhoto(this);
            if (f != null)
                Model.SelectedItem.Icon = f.Path;
        }
    }
}
