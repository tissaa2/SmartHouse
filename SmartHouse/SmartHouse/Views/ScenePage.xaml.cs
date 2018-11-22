using SmartHouse.Models.Logic;
using SmartHouse.ViewModels;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using SmartHouse.Controls;
using Xamarin.Forms;
using Device = SmartHouse.Models.Logic.Device;

namespace SmartHouse.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScenePage : ContentPage
    {
        // public static ScenePage Instance = null;
        public ListPageModel<DeviceModel> Model { get; set; }
        public Scene Target { get; set; }

        public void Refresh(GroupPageModel group)
        {
            if (Target == null)
                return;

            Model.Items = new ObservableCollection<DeviceModel>();
            foreach (var i in group.Devices.Items)
            {
                var dm = i.Clone() as DeviceModel;
                var st = Target.Items.FirstOrDefault(e => e.ID == dm.Device.ID);
                dm.Enabled = st != null;
                dm.ShowDeleteButton = false;
                Model.Items.Add(dm);
            }
            Model.SelectedItem = null;
        }

        public Scene SetTarget(GroupPageModel group, Scene target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;
            Refresh(group);

            return target;
        }

        public ScenePage()
        {
            // Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new ListPageModel<DeviceModel>(null/* , this.Resources["viewEditTemplateSelector"] as ViewEditTemplateSelector */);
            DevicesListView.DeviceStateChanged += DevicesListView_DeviceStateChanged;
        }

        private void DevicesListView_DeviceStateChanged(DeviceModel sender)
        {
            //if (sender == null)
            //    return;
            //if (sender.Enabled)
            //{
            //    var st = Target.Items.FirstOrDefault(e => e.ID == sender.Device.ID);
            //    if (st == null)
            //    {
            //        st = new DeviceState() { ID = sender.Device.ID };
            //        Target.Items.Add(st);
            //    }
            //    sender.Device.SetState(st);
            //}
            //else
            //{
            //    var st = Target.Items.FirstOrDefault(e => e.ID == sender.Device.ID);
            //    if (st != null)
            //        Target.Items.Remove(st);
            //}
        }

        public async void DeleteItem(Device item)
        {
            var answer = await DisplayAlert("Удалить", "Вы действительно хотите удалить устройство?", "Да", "Нет");
            if (answer)
            {
            }
        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            await ProjectMedia.GetPhoto(this, "scene_", (r) =>
            {
                if (r != null)
                {
                    if (Model.Target is Scene)
                    {
                        (Model.Target as Scene).Icon = r;
                    }
                }
            });
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                var b = sender as ImageButton;
                var d = b.Data as Device;
                if (d != null)
                    DeleteItem(d);
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ApplyButton_Pressed(object sender, EventArgs e)
        {
            Target.Items.Clear();
            foreach(var dm in Model.Items)
                if (dm.Enabled)
                {
                    var st = new DeviceState() { ID = dm.Device.ID };
                    dm.Device.SetState(st);
                    Target.Items.Add(st);
                }
        }
    }
}
