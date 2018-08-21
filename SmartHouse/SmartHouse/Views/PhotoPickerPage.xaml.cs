using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Xamarin.Forms;
using SmartHouse.Models;


namespace SmartHouse.Views
{
	// [XamlCompilation(XamlCompilationOptions.Compile)]


	public partial class PhotoPickerPage : ContentPage
	{
        public string Result { get; set; } = null;

		public PhotoPickerPage (string resourcePrefix)
		{
			InitializeComponent ();
            BindingContext = new PhotoPickerModel(resourcePrefix);
		}

        private void ImageSourcePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public static async Task<string> ShowModal(Page parent, string prefix, EventHandler disappearing)
        {
            var p = new PhotoPickerPage(prefix);
            p.Disappearing += disappearing;
            await parent.Navigation.PushModalAsync(p);
            return p.Result;
        }

        private async void OkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            Result = null;
            await Navigation.PopModalAsync();
        }

        private void ResourceListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Result = (ResourceListView.SelectedItem as ResourceEntry).Icon;
        }

    }
}