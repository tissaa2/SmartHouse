using SmartHouse.Models.Logic;
using SmartHouse.Services;
using SmartHouse.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace SmartHouse.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        // public static GroupPage Instance = null;
        public GroupPageModel Model { get; set; }
        public Group Target { get; set; }

        public void DevicesListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LoadDevices();
            // ScenePage.Instance.Refresh(this.Model);
        }

        public void LoadDevices()
        {
            Model.Devices.Items = new ObservableCollection<DeviceModel>();
            foreach (var e in Target.Devices)
            {
                var m = ViewModel.CreateModel(e) as DeviceModel;
                m.Enabled = true;
                m.Group = Target;
                Model.Devices.Items.Add(m);
            }
        }

        public Group SetTarget(Group target)
        {
            if (target == null)
                return null;
            if (Target != null)
            {
                Target.Devices.CollectionChanged -= DevicesListChanged;
            }
            Target = target;
            Target.Devices.CollectionChanged += DevicesListChanged;
            Model.Target = target;
            Model.Scenes.Items = Target.Items;
            Model.Scenes.SelectedItem = null;
            LoadDevices();
            // Model.Devices.Items.Add(ViewModel.CreateModel(e) as DeviceModel);
            Model.Devices.SelectedItem = null;
            return target;
        }

        public GroupPage()
        {
            // Instance = this;
            BindingContext = Model = new GroupPageModel();
            this.InitializeComponent();
        }

        private void ScenesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Scene)
            {
                var s = e.Item as Scene;
                s.Activate(Target);
                var sp = new ScenePage() { Title = s.Name };
                sp.IsVisible = true;
                sp.SetTarget(this.Model, s);
                // DevicePage.Instance.IsVisible = false;
                if (Model.Scenes.SelectedItem != s)
                    Model.Scenes.SelectedItem = s;
                else
                if (Utils.IsDoubleTap())
                    Navigation.PushAsync(sp);
                    // MainPage.Instance.CurrentPage = ScenePage.Instance;
            }
        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            await ProjectMedia.GetPhoto(this, "group_", (r) =>
            {
                if (r != null)
                    (Model.Target as Group).Icon = r;
            });

        }

        private void DevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ImageButton_OnPressed(object sender, EventArgs e)
        {

        }

        private void DeleteDeviceButton_OnPressed(object sender, EventArgs e)
        {

        }

        private void ESlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            if (Model.ScenesMode)
                Target.Items.Add(new Scene(Scene.IntID.NewID(), "Новая сцена", "scenes_brightlight.png"));
            if (Model.DevicesMode)
                Target.Devices.Add(new Lamp("Новое устройство", 50));
        }

        private void DevicesButton_Pressed(object sender, EventArgs e)
        {
            Model.DevicesMode = true;
        }

        private void ScenesButton_Pressed(object sender, EventArgs e)
        {
            Model.ScenesMode = true;
        }
    }
}