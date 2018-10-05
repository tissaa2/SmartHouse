using System;
using System.Windows.Input;
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
        /* private ViewEditTemplateSelector templateSelector = null;
        public ViewEditTemplateSelector TemplateSelector
        {
            get
            {
                if (templateSelector == null)
                    templateSelector = this.Resources["viewEditTemplateSelector"] as ViewEditTemplateSelector;
                return templateSelector;
            }
        } */
        public static ProjectsListPage Instance = null;
        public ListViewModel<Project> Model { get; set;}
        // public ICommand EditItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        /* public void EditItem(Project item)
        {
            ProjectsListView.BeginRefresh();
            TemplateSelector.SetEditedItem(item);
            ProjectsListView.EndRefresh();
        } */

        public async void DeleteItem(Project item)
        {
            var answer = await DisplayAlert("Удалить", "Вы действительно хотите удалить проект?", "Да", "Нет");
            if (answer)
            {
            }
        }

        public ProjectsListPage()
        {
            Instance = this;
            // this.EditItemCommand = new Command<Project>(EditItem);
            this.DeleteItemCommand = new Command<Project>(DeleteItem);
            this.InitializeComponent();
            BindingContext = Model = new ListViewModel<Project>(ProjectsList.Instance.Items/* , TemplateSelector */);
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            ProjectsList.Instance.Items.Add(new Project(ProjectsList.IntID.NewID(), "Новый проект", "home.png"));
        }

        private void ProjectsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Model.SelectedItem != e.Item)
            {
                // EditorRow.Height = 48;
                Model.SelectedItem = e.Item as Project;
            }
            else
            {
                MainPage.Instance.CurrentPage = ProjectPage.Instance;
                ProjectPage.Instance.SetTarget((e.Item as Project));
            }

        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                var b = sender as ImageButton;
                var d = b.Data as Project;
                if (d != null)
                    DeleteItem(d);
            }
        }

        /* private void EditItem_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                var b = sender as ImageButton;
                var d = b.Data as Project;
                if (d != null)
                    EditItem(d);
            }

        } */
    }
}
