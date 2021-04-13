using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ViewModels;
using SmartHouse.Views;
using SmartHouse.Services;

namespace SmartHouse.Controls
{
    public delegate void DeviceModelChangedDelegate(DeviceModel sender);


	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicesListView : ListView
	{
        public bool ShowDeleteButtons { get; set; } = true;
        public int DeleteButtonColumnWidth { get { return ShowDeleteButtons ? 24 : 0; } }

        public DeviceModel CurrentItem { get; set; }

        public event DeviceModelChangedDelegate DeviceStateChanged;
        public event DeviceModelChangedDelegate DeviceDeleted;

        public DevicesListView ()
		{
            InitializeComponent ();
            ItemTemplate = Resources["deviceTemplateSelector"] as DataTemplate;
        }

        //private void DevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item is DeviceModel)
        //    {
        //        var dm = e.Item as DeviceModel;
        //        var dp = new DevicePage() { Title = dm.Name };
        //        dp.IsVisible = true;
        //        dp.SetTarget((e.Item as DeviceModel));
        //        CurrentItem = e.Item as DeviceModel;
        //        if (Utils.IsDoubleTap())
        //            Navigation.PushAsync(dp);
        //    }
        //}

        private void DeleteDeviceButton_OnPressed(object sender, EventArgs e)
        {
            DeviceDeleted?.Invoke((sender as BindableObject).BindingContext as DeviceModel);
        }

        private void ESlider_ValueChanged(object sender, ESliderValueChangeEvents args)
        {
            //// var dm = (sender as Slider).BindingContext as DeviceModel;
            //var dm = (sender as ESlider).BindingContext as DeviceModel;
            //// dm.Device.ApplyState(args.Value.ToString());
            //DeviceStateChanged?.Invoke(dm);
        }

        private void ESocketSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            //var dm = (sender as ESocketSwitch).BindingContext as DeviceModel;
            //// dm.Device.ApplyState(e.Value.ToString());
            //DeviceStateChanged?.Invoke(dm);
        }

        private void EnabledSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            //var sw = sender as Switch;
            //DeviceStateChanged?.Invoke(sw.BindingContext as DeviceModel);
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
                if (bo.BindingContext is DeviceModel)
                {
                    var dm = bo.BindingContext as DeviceModel;
                    var dp = new DevicePage() { Title = dm.Name };
                    dp.IsVisible = true;
                    dp.SetModel(dm);
                    CurrentItem = dm;
                    SelectedItem = dm;
                    await Navigation.PushAsync(dp);
                }
            }
            IsInactive = false;
        }
    }
}