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
        public GroupModel Model { get; set; }
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
            Model.Items = Target.Items;
            Model.SelectedItem = null;
            LoadDevices(Model.InputsMode);
            // Model.Devices.Items.Add(ViewModel.CreateModel(e) as DeviceModel);
            Model.Devices.SelectedItem = null;
            return target;
        }

        public GroupPage()
        {
            // Instance = this;
            BindingContext = Model = new GroupModel();
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
                            {
                                d = new SmartHouse.Models.Logic.Switch(Socket.IntID.NewID(), "Новый выключатель", p.value != 0, pd.ID, (byte)p.ID);
                                Model.InputsMode = true;
                            }
                        }
                        else
                        if (p is OutputPort)
                        {
                            if (pd is Relay)
                                d = new Socket(Socket.IntID.NewID(), "Новая розетка", p.value != 0, pd.ID, (byte)p.ID);
                            else if (pd is Dimmer)
                                d = new Lamp(Socket.IntID.NewID(), "Новый светильник", p.value, pd.ID, (byte)p.ID);
                            Model.InputsMode = false;
                        }
                    }
                    else
                    {
                        if (pd is IRPanel || pd is MSTPanel)
                        {
                            d = new SmartHouse.Models.Logic.Panel(Socket.IntID.NewID(), "Новая панель", pd.Inputs.Select(i => i.Value != 0), pd.ID, (byte)p.ID);
                            Model.InputsMode = true;
                        }

                    }

                    if (d == null)
                        await DisplayAlert("Ошибка", "Выбрано неправильное устройство", "Принять");
                    else
                    {
                        Target.Devices.Add(d);
                        d.Init();
                        var dm = DeviceModel.CreateModel(d) as DeviceModel;
                        dm.Enabled = true;
                        dm.Group = Target;
                        var dp = new DevicePage() { Title = dm.Name };
                        dp.IsVisible = true;
                        if (dm != null)
                            dp.SetModel(dm);
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
            Model.InputsMode = false;
            LoadDevices(Model.InputsMode);
        }

        private void ScenesItemTapRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is BindableObject)
            {
                var bo = sender as BindableObject;
                if (bo.BindingContext is Scene)
                {
                    var s = bo.BindingContext as Scene;
                    s.Activate(Target);
                    ScenesListView.SelectedItem = Model.Scenes.SelectedItem = s;
                }
            }

        }

        private bool IsInactive = false;

        private async void ScenesItemDoubleTapRecognizer_DoubleTapped(object sender, EventArgs e)
        {
            if (IsInactive)
                return;
            IsInactive = true;
            if (sender is BindableObject)
            {
                var bo = sender as BindableObject;
                if (bo.BindingContext is Scene)
                {
                    var s = bo.BindingContext as Scene;
                    s.Activate(Target);

                    var sp = new ScenePage() { Title = s.Name };
                    sp.IsVisible = true;
                    sp.SetTarget(this.Model, s);
                    ScenesListView.SelectedItem = Model.Scenes.SelectedItem = s;
                    await Navigation.PushAsync(sp);
                }
            }
            IsInactive = false;
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