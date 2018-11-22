using Device = SmartHouse.Models.Logic.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ViewModels;
using SmartHouse.Models;
using System;

namespace SmartHouse.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        public static SettingsPage Instance = null;
        public SettingsPageModel Model { get; set; }

        public SettingsPage()
        {
            Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new SettingsPageModel(Settings.Instance);
        }

        private void ApplyButton_Pressed(object sender, EventArgs e)
        {
            Model.Apply(Settings.Instance);
        }

        private void RevertButton_Pressed(object sender, EventArgs e)
        {
            Model.Assign(Settings.Instance);
        }
    }
}