using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Models.Logic;
using SmartHouse.ViewModels;
using Plugin.Media;

namespace SmartHouse.Views
{
	// [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectPage : ContentPage
	{
        public static ProjectPage Instance = null;
        
        public ListPageModel<Group> Model { get; set; }
        public Project Target { get; set; }

        public Project SetTarget(Project target)
        {
            if (target == null)
                return null;
            Target = target;
            NameLabel.Text = target.Name;
            Model.Items = Target.Items;
            Model.SelectedItem = null;
            return target;
        }

        public ProjectPage()
        {
            Instance = this;
            BindingContext = Model = new ListPageModel<Group>(null);
            this.InitializeComponent();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Target.Items.Add(new Group() { Name = "Новая группа", ID = Group.IntID.NewID(), Icon = "room.png" });
        }

        private void GroupsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Model.SelectedItem != e.Item)
            {
                EditorRow.Height = 48;
                Model.SelectedItem = e.Item as Group;
            }
            else
            {
                MainPage.Instance.CurrentPage = GroupPage.Instance;
                GroupPage.Instance.SetTarget((e.Item as Group));
            }

        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            var f = await ProjectMedia.GetPhoto(this);
            if (f != null)
                Model.SelectedItem.Icon = f.Path;

        }
    }
}