using SmartHouse.Controls;
using SmartHouse.Models.Storage;
using SmartHouse.Models.Packets;
using SmartHouse.Services;
using SmartHouse.ViewModels;
using SmartHouse.Models.Physics;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using SmartHouse.Models;
using System.Threading.Tasks;
using ImageButton = SmartHouse.Controls.ImageButton;

namespace SmartHouse.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectPage : ContentPage
    {
        // public static ProjectPage Instance = null;
        //public ListPageModel<Group> Model { get; set; }
        // public Project Target { get; set; }
        // public ListPageModel<Group> Model { get; set; }
        public ProjectModel Model { get; set; }

        public Project SetModel(Project target)
        {
            if (target == null)
                return null;
            // Target = target;

            
            BindingContext = Model = new ListPageModel<Group>(target.Items, target.Icon, target.Name);
            //Model.Target = target;
            //Model.Items = Target.Items;
            Model.SelectedItem = null;
            return target;
        }

        public ProjectPage()
        {
            // Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new ListPageModel<Group>(null, null, null);
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Model.Items.Add(new Group(Project.IntID.NewID(), "Новая комната {0}", "room.png"));
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
        public async Task<bool> SaveProjectToCAN()
        // public bool SaveProjectToCAN()
        {
            await Client.CurrentServer.SaveProjectFile((Model.Target as Project).Zip());

            var ad = new List<Models.Logic.Device>();
            foreach (var g in Model.Items)
                foreach (var d in g.Devices)
                    if (!ad.Contains(d))
                        ad.Add(d);

            var pds = new List<PDevice>();

            foreach (var g in Model.Items)
                foreach (var s in g.Items)
                {
                    foreach (var ds in s.Items)
                    {
                        //Models.Logic.Device d = null;
                        //foreach(var e in ad)
                        //{
                        //    if (e.ID == ds.ID)
                        //    {
                        //        d = e;
                        //        break;
                        //    }
                        //}

                        var d = ad.FirstOrDefault(e => e.ID == ds.ID);
                        var pd = PDevice.All.FirstOrDefault(e => e.ID == d.UID);
                        var td = new Dictionary<UID, string>();
                        //foreach (var e0 in PDevice.All)
                        //    td.Add(e0.ID, e0.Name);
                        if (!pds.Contains(pd))
                        {
                            pd.Scenes = new Dictionary<int, PScene>();
                            pds.Add(pd);
                        }
                        double v = 0;
                        if (double.TryParse(ds.Value, out v))
                        {

                        }
                        else
                        {
                            bool b;
                            if (bool.TryParse(ds.Value, out b))
                                v = b ? 0 : 1;
                        }
                        if (!pd.Scenes.ContainsKey(s.ID))
                            pd.Scenes.Add(s.ID, new PScene(s.ID, s.Event.GetUID(g), s.Event.InputID, s.Event.TypeID));
                            // pd.Scenes.Add(s.ID, new PScene(s.ID, s.Event is UIDEvent ? (s.Event as UIDEvent).UID : new UID(0, 0, (s.Event as GroupEvent).GroupID), s.Event.InputID, s.Event.TypeID));
                        var ps = pd.Scenes[s.ID];
                        ps.OutputStates.Add(d.PortID, v);
                    }
                }

            bool r = true;
            foreach (var pd in pds)
            {
                r = await pd.WriteScenes();
                if (!r)
                    return false;
            }
            return true;
        }

        // 1: Загрузить проект из CAN
        public async void LoadProjectFromCAN()
        {
            var f = await Client.CurrentServer.LoadProjectFile();
            if (f.Complete)
            {
                var ot = Model.Target as Project;
                // string s = Encoding.ASCII.GetString(f.Data);
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

        private async void ProjectMenuPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ProjectMenuPicker.SelectedIndex)
            {
                case (0):
                    await SaveProjectToCAN();
                    break;
                case (1):
                    LoadProjectFromCAN();
                    break;
                case (2):
                    ShowDebug();
                    break;
                case (3):
                    ClearProject();
                    break;
            }
            ProjectMenuPicker.SelectedIndex = -1;
        }

        private void ClearProject()
        {
            (Model.Target as Project).Clear();
        }

        private void ShowDebug()
        {
            Navigation.PushAsync(new DebugPage());
        }



        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            ProjectMenuPicker.Focus();
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
                if (bo.BindingContext is Group)
                {
                    var g = bo.BindingContext as Group;
                    var gp = new GroupPage() { Title = g.Name };
                    gp.IsVisible = true;
                    gp.SetTarget(g);
                    GroupsListView.SelectedItem = Model.SelectedItem = g;
                    await Navigation.PushAsync(gp);
                }
            }
            IsInactive = false;
        }
    }
}