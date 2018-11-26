using Device = SmartHouse.Models.Logic.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ViewModels;
using System;
using SmartHouse.Controls;
using SmartHouse.Models.Physics;

namespace SmartHouse.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicesBrowserPage : ContentPage
	{
        // public static DevicePage Instance = null;
        public DevicesBrowserPageModel Model { get; set; }
        public DevicesBrowserPage()
        {
            // Instance = this;
            var all = PDevice.All;
            // System.Threading.Thread.Sleep(5000);
            this.InitializeComponent();
            BindingContext = Model = new DevicesBrowserPageModel(PDevice.All);
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {
            MenuPicker.Focus();
        }

        private void ShowDebug()
        {
            
            Navigation.PushAsync(new DebugPage());
        }

        private void MenuPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (MenuPicker.SelectedIndex)
            {
                case (0):
                    PDevice.LoadAllAsync();
                    break;
                case (1):
                    ShowDebug();
                    break;
            }
        }

        private void SelectButton_Pressed(object sender, EventArgs e)
        {

        }

        private void ESlider_ValueChanged(object sender, ESliderValueChangeEvents args)
        {
            var o = (sender as ESlider).BindingContext;
            (o as Port).SetValue(args.Value);
        }

        private void ESocketSwitch_Toggled(object sender, ToggledEventArgs e)
        {
        }

        private void EnabledSwitch_Toggled(object sender, ToggledEventArgs e)
        {
        }

    }
}