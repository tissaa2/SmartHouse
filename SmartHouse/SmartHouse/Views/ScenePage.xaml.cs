using System;
using Xamarin.Forms;
using SmartHouse.Models.Logic;
using SmartHouse.ViewModels;
using Device = SmartHouse.Models.Logic.Device;

namespace SmartHouse.Views
    {
        // [XamlCompilation(XamlCompilationOptions.Compile)]
        public partial class ScenePage : ContentPage
        {
            public static ScenePage Instance = null;
            public ListPageModel<Device> Model { get; set; }
            public Scene Target { get; set; }

            public Scene SetTarget(Scene target)
            {
                if (target == null)
                    return null;
                Target = target;
                Model.Target = target;
                Model.Items = Target.Items;
                Model.SelectedItem = null;
                return target;
            }

            public ScenePage()
            {
                Instance = this;
                this.InitializeComponent();
                BindingContext = Model = new ListPageModel<SmartHouse.Models.Logic.Device>(null/* , this.Resources["viewEditTemplateSelector"] as ViewEditTemplateSelector */);
            }

            private void AddButton_Clicked(object sender, EventArgs e)
            {
                Target.Items.Add(new Device(Scene.UIDID.NewID(), "Новое устройство {0}", ".png"));
            }

            private void GroupsListView_ItemTapped(object sender, ItemTappedEventArgs e)
            {
                if (Model.SelectedItem != e.Item)
                {
                    Model.SelectedItem = e.Item as Device;
                }
                else
                {
                    MainPage.Instance.CurrentPage = DevicePage.Instance;
                    DevicePage.Instance.SetTarget((e.Item as Device));
                }

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
                await ProjectMedia.GetPhoto(this, "scene_", (r) => {
                    if (r != null)
                    {
                        if (Model.Target is Project)
                        {
                            (Model.Target as Project).Icon = r;
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
