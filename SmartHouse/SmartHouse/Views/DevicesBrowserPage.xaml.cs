﻿using Device = SmartHouse.Models.Logic.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ViewModels;
using System;
using SmartHouse.Controls;
using SmartHouse.Models.Physics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHouse.Views
{
    // public delegate void PortActionDelegate(Port port);
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicesBrowserPage : ContentPage
	{
        private void PushValue<T>(List<T> ports) where T : Port
        {
            foreach (var p in ports)
            {
                p.PushValue();
            }
        }

        private void SetValue<T>(List<T> ports, double value) where T: Port
        {
            foreach(var p in ports)
            {
                p.Value = value;
                System.Threading.Thread.Sleep(33);
            }
        }

        private void PopValue<T>(List<T> ports) where T: Port
        {
            foreach (var p in ports)
            {
                p.PopValue();
                // p.SetValue(p.Value);
            }
        }

        public async Task<Port> FindPort<T>(List<T> ports) where T: Port
        {
            if (ports.Count < 1)
            {
                return null;
            }
            if (ports.Count == 1)
            {
                ports[0].Value = 100;
                return ports[0];
            }

            int hc = ports.Count / 2;
            var r = ports.GetRange(0, hc);
            // PushValue<T>(r);
            SetValue<T>(r, 0);
            await Task.Delay(500);
            SetValue<T>(r, 100);
            var result = await this.DisplayAlert("Система", "Активно ли устройство?", "Да", "Нет");
            SetValue<T>(r, 0);
            await Task.Delay(500);
            // PopValue<T>(r);
            if (result)
                return await FindPort<T>(r);
            r = ports.GetRange(hc, ports.Count - hc);
            return await FindPort<T>(r);
        }


        
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
                case (2):
                    FindPort();
                    break;
            }
            MenuPicker.SelectedIndex = -1;
        }

        public async void FindPort()
        {
            List<OutputPort> ps = new List<OutputPort>(PDevice.AllOutputs);
            SetValue<OutputPort>(ps, 0);
            SelectedPort = await FindPort<OutputPort>(ps);
        }

        private void SelectButton_Pressed(object sender, EventArgs e)
        {

        }

        private void ESlider_ValueChanged(object sender, ESliderValueChangeEvents args)
        {
            //var o = (sender as ESlider).BindingContext;
            //var v = args.Value;
            //if (v < 10)
            //    v = 0;
            //if (v > 90)
            //    v = 100;
            //(o as Port).SetValue(v);
        } 

        private void ESocketSwitch_Toggled(object sender, ToggledEventArgs e)
        {
        }

        private void EnabledSwitch_Toggled(object sender, ToggledEventArgs e)
        {
        }

        private Port selectedPort = null;
        public Port SelectedPort
        {
            get => selectedPort;
            set
            {
                if (selectedPort != null)
                    selectedPort.BGColor = Color.Transparent;
                selectedPort = value;
                selectedPort.BGColor = Color.FromHex("DDDDEE");
                DevicesListView.SelectedItem = selectedPort.Parent;
                selectedPort.Parent.Fold = false;
                DevicesListView.ScrollTo(DevicesListView.SelectedItem, ScrollToPosition.Start, true);
            }
        }
        private void InputsTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout)
            {
                SelectedPort = (sender as StackLayout).BindingContext as Port;
            }
        }
    }
}