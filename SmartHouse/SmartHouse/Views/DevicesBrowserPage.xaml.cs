using Device = SmartHouse.Models.Logic.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ViewModels;
using System;
using SmartHouse.Controls;

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
            this.InitializeComponent();
            // BindingContext = Model = new DevicesBrowserPageModel();
        }

        private void ToolbarItem_Activated(object sender, EventArgs e)
        {

        }

        private void MenuPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Activated_1(object sender, EventArgs e)
        {

        }

        private void SelectButton_Pressed(object sender, EventArgs e)
        {

        }

        private void ESlider_ValueChanged(object sender, ESliderValueChangeEvents args)
        {
        }

        private void ESocketSwitch_Toggled(object sender, ToggledEventArgs e)
        {
        }

        private void EnabledSwitch_Toggled(object sender, ToggledEventArgs e)
        {
        }
    }
}