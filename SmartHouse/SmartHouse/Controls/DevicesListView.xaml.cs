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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicesListView : ListView
	{
        public DeviceModel CurrentItem { get; set; }

        public DevicesListView ()
		{
			InitializeComponent ();
		}

        private void DevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (CurrentItem != e.Item)
            {
                CurrentItem = e.Item as DeviceModel;
            }
            else
            if (Utils.IsDoubleTap())
            {
                DevicePage.Instance.IsVisible = true;
                MainPage.Instance.CurrentPage = DevicePage.Instance;
                DevicePage.Instance.SetTarget((e.Item as DeviceModel));
            }
        }

        private void DeleteDeviceButton_OnPressed(object sender, EventArgs e)
        {

        }
    }
}