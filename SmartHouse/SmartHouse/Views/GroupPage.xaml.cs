using SmartHouse.Models.Logic;
using SmartHouse.Services;
using SmartHouse.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using System.Linq;
using SmartHouse.Models.Physics;

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
            LoadDevices(Model.InputsMode);
            // ScenePage.Instance.Refresh(this.Model);
        }

        public void LoadDevices(bool _isInput)
        {
            Model.Devices.Items = new ObservableCollection<DeviceModel>(Target.Devices.Where(i => i.IsInput == _isInput).Select(i => {
                var m = DeviceModel.CreateModel(i) as DeviceModel;
                m.Enabled = true;
                m.Group = Target;
                return m;
            }));
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
            LoadDevices(Model.InputsMode);
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

        //private void ScenesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item is Scene)
        //    {
        //        var s = e.Item as Scene;
        //        s.Activate(Target);
        //        var sp = new ScenePage() { Title = s.Name };
        //        sp.IsVisible = true;
        //        sp.SetTarget(this.Model, s);
        //        if (Model.Scenes.SelectedItem != s)
        //            Model.Scenes.SelectedItem = s;
        //        else
        //        if (Utils.IsDoubleTap())
        //            Navigation.PushAsync(sp);
        //    }
        //}

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

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (Model.ScenesMode)
                Target.Items.Add(new Scene(Scene.IntID.NewID(), "Новая сцена", "scenes_brightlight.png"));
            if (Model.DevicesMode)
            {
                var dbp = new DevicesBrowserPage();
                dbp.Disappearing += async (s, e0) =>
                {
                    var p = dbp.Model.SelectedPort;
                    var pd = dbp.Model.SelectedItem;
                    SmartHouse.Models.Logic.Device d = null;
                    if (p != null)
                    {
                        if (p is InputPort)
                        {
                            if (pd is IRPanel || pd is MSTPanel || pd is Dimmer || pd is Relay)
                                d = new SmartHouse.Models.Logic.Switch("Новый выключатель", p.value != 0);
                        }
                        else
                        if (p is OutputPort)
                        {
                            if (pd is Relay)
                                d = new Socket("Новая розетка", p.value != 0);
                            else if (pd is Dimmer)
                                d = new Lamp("Новый светильник", p.value);
                        }
                    }
                    else
                    {
                        if (pd is IRPanel || pd is MSTPanel)
                            d = new SmartHouse.Models.Logic.Panel("Новая панель", pd.Inputs.Select(i => i.Value != 0));

                    }

                    if (d == null)
                        await DisplayAlert("Ошибка", "Выбрано неправильное устройство", "Принять");
                    else
                    {
                        Target.Devices.Add(d);
                        var sdid = d.ID.ToString();
                        var dm = Model.Devices.Items.FirstOrDefault(i => i.ID == sdid);
                        var dp = new DevicePage();
                        if(dm != null)
                            dp.SetTarget(dm);
                        await Navigation.PushAsync(dp);

                    }

                };
                await Navigation.PushAsync(dbp);
            }


        }

        private void DevicesButton_Pressed(object sender, EventArgs e)
        {
            Model.DevicesMode = true;
        }

        private void ScenesButton_Pressed(object sender, EventArgs e)
        {
            Model.ScenesMode = true;
        }

        private void ScenesItemGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is BindableObject)
            {
                var bo = sender as BindableObject;
                if (bo.BindingContext is Scene)
                {
                    var s = bo.BindingContext as Scene;
                    var sp = new ScenePage() { Title = s.Name };
                    sp.IsVisible = true;
                    sp.SetTarget(this.Model, s);
                    ScenesListView.SelectedItem = Model.Scenes.SelectedItem = s;
                    Navigation.PushAsync(sp);
                }
            }

        }

        private void InputDevicesButton_Pressed(object sender, EventArgs e)
        {
            Model.InputsMode = true;
            LoadDevices(Model.InputsMode);
        }

        private void OutputDevicesButton_Pressed(object sender, EventArgs e)
        {
            Model.InputsMode = false;
            LoadDevices(Model.InputsMode);
        }
    }
}