using System;
using Xamarin.Forms;
using SmartHouse.Models.Logic;
using SmartHouse.ViewModels;
using System.Collections.ObjectModel;
namespace SmartHouse.Views
{
	// [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupPage : ContentPage
	{
        public static GroupPage Instance = null;
        public GroupPageModel Model { get; set; }
        public Group Target { get; set; }

        public Group SetTarget(Group target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;
            Model.Scenes.Items = Target.Items;
            Model.Scenes.SelectedItem = null;

            Model.Devices.Items = new ObservableCollection<DeviceModel>();
            foreach (var e in target.Devices)
                Model.Devices.Items.Add(ViewModel.CreateModel(e) as DeviceModel);
            Model.Devices.SelectedItem = null;
            return target;
        }

        public GroupPage()
        {
            Instance = this;
            BindingContext = Model = new GroupPageModel();
            this.InitializeComponent();
        }

        private void ScenesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Model.Scenes.SelectedItem != e.Item)
            {
                Model.Scenes.SelectedItem = e.Item as Scene;
                Model.Scenes.SelectedItem.Activate();
            }
            else
            {
                MainPage.Instance.CurrentPage = ScenePage.Instance;
                ScenePage.Instance.SetTarget(this.Model, (e.Item as Scene));
            }
        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            await ProjectMedia.GetPhoto(this, "group_", (r)=> {
                if (r != null)
                    (Model.Target as Group).Icon = r;
            });
            
        }

        private void DevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void AddSceneButton_Clicked(object sender, EventArgs e)
        {
            Target.Items.Add(new Scene(Scene.IntID.NewID(), "Новая сцена", "scenes_brightlight.png"));
        }

        private void SceneIconButton_Clicked(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {

        }

        private void ImageButton_OnPressed(object sender, EventArgs e)
        {

        }

        private void DeleteDeviceButton_OnPressed(object sender, EventArgs e)
        {

        }

        private void ESlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }
    }
}