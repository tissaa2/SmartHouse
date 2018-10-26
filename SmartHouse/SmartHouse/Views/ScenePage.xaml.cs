using SmartHouse.Models.Logic;
using SmartHouse.ViewModels;
using System;
using System.Collections.ObjectModel;
using SmartHouse.Controls;
using Xamarin.Forms;
using Device = SmartHouse.Models.Logic.Device;

namespace SmartHouse.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScenePage : ContentPage
    {
        public static ScenePage Instance = null;
        public ListPageModel<DeviceModel> Model { get; set; }
        public Scene Target { get; set; }

        public void Refresh(GroupPageModel group)
        {
            if (Target == null)
                return;

            Model.Items = new ObservableCollection<DeviceModel>();
            foreach (var e in group.Devices.Items)
                Model.Items.Add(e.Clone() as DeviceModel);
            Model.SelectedItem = null;
        }

        public Scene SetTarget(GroupPageModel group, Scene target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;

            Model.Items = new ObservableCollection<DeviceModel>();
            foreach (var e in group.Devices.Items)
                Model.Items.Add(e.Clone() as DeviceModel);
            Model.SelectedItem = null;
            return target;
        }

        public ScenePage()
        {
            Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new ListPageModel<DeviceModel>(null/* , this.Resources["viewEditTemplateSelector"] as ViewEditTemplateSelector */);
        }

        public async void DeleteItem(Device item)
        {
            var answer = await DisplayAlert("Удалить", "Вы действительно хотите удалить устройство?", "Да", "Нет");
            if (answer)
            {
            }
        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            await ProjectMedia.GetPhoto(this, "scene_", (r) =>
            {
                if (r != null)
                {
                    if (Model.Target is Scene)
                    {
                        (Model.Target as Scene).Icon = r;
                    }
                }
            });
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton)
            {
                var b = sender as ImageButton;
                var d = b.Data as Device;
                if (d != null)
                    DeleteItem(d);
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
