using SmartHouse.Controls;
using SmartHouse.Models.Logic;
using SmartHouse.Services;
using SmartHouse.ViewModels;
using System;
using Xamarin.Forms;

namespace SmartHouse.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectPage : ContentPage
    {
        // public static ProjectPage Instance = null;
        public ListPageModel<Group> Model { get; set; }
        public Project Target { get; set; }

        public Project SetTarget(Project target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;
            Model.Items = Target.Items;
            Model.SelectedItem = null;
            return target;
        }

        public ProjectPage()
        {
            // Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new ListPageModel<Group>(null/* , this.Resources["viewEditTemplateSelector"] as ViewEditTemplateSelector */);
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Target.Items.Add(new Group(Project.IntID.NewID(), "Новая группа {0}", "room.png"));
        }

        private void GroupsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Group)
            {
                var g = e.Item as Group;
                var gp = new GroupPage() {Title = g.Name};
                gp.IsVisible = true;
                gp.SetTarget(g);
                // ScenePage.Instance.IsVisible = false;
                // DevicePage.Instance.IsVisible = false;
                if (Model.SelectedItem != e.Item)
                    Model.SelectedItem = g;
                else
                if (Utils.IsDoubleTap())
                    Navigation.PushAsync(gp);
                    // MainPage.Instance.CurrentPage = GroupPage.Instance;
            }
        }
        public async void DeleteItem(Group item)
        {
            var answer = await DisplayAlert("Удалить", "Вы действительно хотите удалить группу?", "Да", "Нет");
            if (answer)
            {
            }
        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            await ProjectMedia.GetPhoto(this, "project_", (r) =>
            {
                if (r != null)
                {
                    if (Model.Target is Project)
                    {
                        (Model.Target as Project).Icon = r;
                    }
                }
            });
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                var b = sender as ImageButton;
                var d = b.Data as Group;
                if (d != null)
                    DeleteItem(d);
            }
        }

        private void MenuButton_Pressed(object sender, EventArgs e)
        {
            ProjectMenuPicker.Focus();
        }

        // 0: Сохранить проект в CAN
        public void SaveProjectToCAN()
        {

        }

        // 1: Загрузить проект из CAN
        public void LoadProjectFromCAN()
        {
            var ot = Target;
            var p = Project.Create("Проект из CAN", "project_houseCAN.png", Project.IntID.NewID());
            var i = ProjectsList.Instance.Items.IndexOf(ot);
            if (i > -1)
            {
                ProjectsList.Instance.Items[i] = p;
                Navigation.PopAsync();
            }
        }

        private void ProjectMenuPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ProjectMenuPicker.SelectedIndex)
            {
                case (0):
                    SaveProjectToCAN();
                    break;
                case (1):
                    LoadProjectFromCAN();
                    break;
            }
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            ProjectMenuPicker.Focus();
        }
    }
}