using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Models.Logic;
using SmartHouse.ViewModels;

namespace SmartHouse.Views
{
	// [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupPage : ContentPage
	{
        public static GroupPage Instance = null;
        public ListViewModel<Scene> Model { get; set; }
        public Group Target { get; set; }

        public Group SetTarget(Group target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;
            Model.Items = Target.Items;
            Model.SelectedItem = null;
            return target;
        }

        public GroupPage()
        {
            Instance = this;
            BindingContext = Model = new ListViewModel<Scene>(new System.Collections.ObjectModel.ObservableCollection<Scene>()/* , new ViewEditTemplateSelector() */);
            this.InitializeComponent();
        }

        private void ScenesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Model.SelectedItem != e.Item)
            {
                Model.SelectedItem = e.Item as Scene;
                Model.SelectedItem.Activate();
            }
            else
            {
                // 2DO:
                // MainPage.Instance.CurrentPage = ScenePage.Instance;
                // ScenePage.Instance.SetTarget((e.Item as Scene));
            }
        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            await ProjectMedia.GetPhoto(this, "group_", (r)=> {
                if (r != null)
                    Model.SelectedItem.Icon = r;
            });
            
        }

        private void DevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void AddSceneButton_Clicked(object sender, EventArgs e)
        {
            Target.Items.Add(new Scene(Scene.IntID.NewID(), "Новая сцена", "light.png"));
        }

        private void SceneIconButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}