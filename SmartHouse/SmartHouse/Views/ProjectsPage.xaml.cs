using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Services;
using SmartHouse.Models;
using SmartHouse.Models.Logic;

namespace SmartHouse.Views
{
    public partial class ProjectsPage : ContentPage
    {
        public static ProjectsPage Instance = null;

        public ProjectsPage()
        {
            Instance = this;
            this.InitializeComponent();
            ProjectsListView.ItemsSource = Projects.Instance;
        }

        private void ProjectsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MainPage.Instance.CurrentPage = ProjectPage.Instance;
            ProjectPage.Instance.
            // ProjectPage.Instance.
        }
    }
}
