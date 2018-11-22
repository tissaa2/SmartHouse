using Device = SmartHouse.Models.Logic.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ViewModels;
using System;

namespace SmartHouse.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicePage : ContentPage
	{
        // public static DevicePage Instance = null;
        public DeviceModel Target { get; set; }
        public DevicePageModel Model { get; set; }
        public DeviceModel SetTarget(DeviceModel target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;
            return target;
        }

        public DevicePage()
        {
            // Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new DevicePageModel();
        }

        private void IconButton_OnPressed(object sender, System.EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DeviceTypesPicker.Focus();
        }

        private void ApplyButton_Pressed(object sender, EventArgs e)
        {
            Target.Apply();
            Title = Target.Name;
        }
    }
}