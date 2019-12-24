﻿using Device = SmartHouse.Models.Logic.Device;
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
            target.IsDirty = false;
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
            // DeviceTypesPicker.Focus();
        }

        private void ApplyButton_Pressed(object sender, EventArgs e)
        {
            Target.Apply();
            Title = Target.Name;
        }
        //2DO:
        //пофиксить баги слайдера
        //добавить активацию сцен
        //проверить программирование сцен в сети CAN

        private async void FindDeviceButton_Clicked(object sender, EventArgs e)
        {
            var dbp = new DevicesBrowserPage();
            dbp.Disappearing += (s, e0) =>
            {
                var p = dbp.Model.SelectedPort;
                if (p != null)
                {
                    Target.UID = p.Parent.ID.ToString();
                    Target.PortID = p.ID.ToString();
                }
            };
            await Navigation.PushAsync(dbp);
        }
    }
}