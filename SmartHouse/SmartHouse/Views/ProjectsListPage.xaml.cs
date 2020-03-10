using SmartHouse.Controls;
using SmartHouse.Models.Logic;
using SmartHouse.Models.Physics;
using SmartHouse.Services;
using SmartHouse.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using ImageButton = SmartHouse.Controls.ImageButton;


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
        // public static ProjectsListPage Instance = null;
        public ProjectsListModel Model { get; set; }
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
                Model.Items.Remove(item);
            }
        }

        public ProjectsListPage()
        {
            // Instance = this;
            // this.EditItemCommand = new Command<Project>(EditItem);
            this.DeleteItemCommand = new Command<Project>(DeleteItem);
            this.InitializeComponent();
            // BindingContext = Model = new ListPageModel<Project>(ProjectsList.Instance.Items/* , TemplateSelector */);
            BindingContext = Model = new ProjectsListModel(ProjectsList.Instance.Items);
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            ProjectsList.Instance.Items.Add(new Project(ProjectsList.IntID.NewID(), "Новый проект", "home.png"));
        }

        private void ProjectsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Project)
            {
                var p = e.Item as Project;
                var pp = new ProjectPage() { Title = p.Name };
                pp.IsVisible = true;
                pp.SetModel(p);
                // GroupPage.Instance.IsVisible = false;
                // ScenePage.Instance.IsVisible = false;
                // DevicePage.Instance.IsVisible = false;
                if (Model.SelectedItem != e.Item)
                    Model.SelectedItem = p;
                else
                if (Utils.IsDoubleTap())
                {
                    Navigation.PushAsync(pp);
                }
                    // MainPage.Instance.CurrentPage = ProjectPage.Instance;
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

        // 0: Сохранить данные на устройство
        public void SaveDataToDevice()
        {
            ProjectsList.Instance.Save();
        }
        // 1: Загрузить данные с устройства
        public void LoadDataFromDevice()
        {
            ProjectsList.Instance = ProjectsList.Load<ProjectsList>(ProjectsList.FileName);
            Model.Items = ProjectsList.Instance.Items;
            UpdateTabs();
        }
        // 2: Загрузить тестовые данные
        public void LoadTestData()
        {
            ProjectsList.LoadTestData();
            Model.Items = ProjectsList.Instance.Items;
            UpdateTabs();
        }

        public void UpdateTabs()
        {

            // MainPage.Instance.CurrentPage = ProjectsListPage.Instance;
            //ProjectPage.Instance.IsVisible = false;
            //GroupPage.Instance.IsVisible = false;
            //ScenePage.Instance.IsVisible = false;
            //DevicePage.Instance.IsVisible = false;

        }

        private void ProjectMenuPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ProjectMenuPicker.SelectedIndex)
            {
                case (0):
                    LoadDataFromDevice();
                    break;
                case (1):
                    SaveDataToDevice();
                    break;
                case (2):
                    LoadTestData();
                    break;
                case (3):
                    ShowSettings();
                    break;
                case (4):
                    ShowDebug();
                    break;
                case (5):
                    ShowDeviceBrowser();
                    break;
                case (6):
                    TestWriteScenes();
                    break;
            }
            ProjectMenuPicker.SelectedIndex = -1;
        }

        private void ShowDeviceBrowser()
        {
            Navigation.PushAsync(new DevicesBrowserPage());
        }

        private void ShowSettings()
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void ShowDebug()
        {
            Navigation.PushAsync(new DebugPage());
        }

        private void TestWriteScenes()
        {
            var d = new Dimmer() {ID = new Models.UID(17)};
            d.Scenes = new Dictionary<int, PScene>();
            d.Scenes.Add(0, new PScene(0, new Models.UID(17), 5, 4));

            d.Init(new Models.UID(17), 8, 8, 2);
            d.WriteScenes();
        }

        private void MenuButton_Pressed(object sender, EventArgs e)
        {
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            ProjectMenuPicker.Focus();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private bool IsInactive = false;

        private async void ItemGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (IsInactive)
                return;
            IsInactive = true;
            if (sender is BindableObject)
            {
                var bo = sender as BindableObject;
                if (bo.BindingContext is Project)
                {
                    var p = bo.BindingContext as Project;
                    var pp = new ProjectPage() { Title = p.Name };
                    pp.IsVisible = true;
                    pp.SetModel(p);
                    ProjectsListView.SelectedItem = p;
                    await Navigation.PushAsync(pp);
                }
            }
            IsInactive = false;
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
