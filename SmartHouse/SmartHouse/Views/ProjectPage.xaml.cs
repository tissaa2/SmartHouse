using SmartHouse.Controls;
using SmartHouse.Models.Logic;
using SmartHouse.Models.Packets;
using SmartHouse.Services;
using SmartHouse.ViewModels;
using System;
using System.Net;
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

        //private void GroupsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item is Group)
        //    {
        //        var g = e.Item as Group;
        //        var gp = new GroupPage() {Title = g.Name};
        //        gp.IsVisible = true;
        //        gp.SetTarget(g);
        //        if (Model.SelectedItem != e.Item)
        //            Model.SelectedItem = g;
        //        else
        //        if (Utils.IsDoubleTap())
        //            Navigation.PushAsync(gp);
        //    }
        //}

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

        private byte CalcCRC(byte[] data, int offset, int size)
        {
            int res = 0;
            var bts = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(offset));
            for (int i = 0; i < bts.Length; i++)
                res = (byte)(res + bts[i]);
                // res = (res + bts[i]) & 0xFF;
            for (int i = offset; i < offset + size; i++)
                res = (byte)(res + data[i]);
                // res = (res + data[i]) & 0xFF;
            return (byte)((0x100 - res) & 0xFF);
        }

        // 0: Сохранить проект в CAN
        public void SaveProjectToCAN()
        {
            Client.CurrentServer.SaveProjectFile(Target.Zip());
        }

        // 1: Загрузить проект из CAN
        public async void LoadProjectFromCAN()
        {
            var f = await Client.CurrentServer.LoadProjectFile();
            if (f.Complete)
            {
                var ot = Target;
                var p = Project.UnZip<Project>(f.Data);
                var i = ProjectsList.Instance.Items.IndexOf(ot);
                if (i > -1)
                {
                    ProjectsList.Instance.Items[i] = p;
                    await Navigation.PopAsync();
                }
            }
        }

        // 1: Загрузить проект из CAN
        //public void LoadProjectFromCAN()
        //{
        //    var ot = Target;
        //    var p = Project.Create("Проект из CAN", "project_houseCAN.png", Project.IntID.NewID());
        //    var i = ProjectsList.Instance.Items.IndexOf(ot);
        //    if (i > -1)
        //    {
        //        ProjectsList.Instance.Items[i] = p;
        //        Navigation.PopAsync();
        //    }
        //}

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
            ProjectMenuPicker.SelectedIndex = -1;
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            ProjectMenuPicker.Focus();
        }

        private void ItemGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is BindableObject)
            {
                var bo = sender as BindableObject;
                if (bo.BindingContext is Group)
                {
                    var g = bo.BindingContext as Group;
                    var gp = new GroupPage() { Title = g.Name };
                    gp.IsVisible = true;
                    gp.SetTarget(g);
                    GroupsListView.SelectedItem = Model.SelectedItem = g;
                    Navigation.PushAsync(gp);
                }
            }
        }
    }
}