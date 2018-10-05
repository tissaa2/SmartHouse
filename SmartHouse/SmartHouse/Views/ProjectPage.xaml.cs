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
        public ListViewModel<Group> Model { get; set; }
        public Project Target { get; set; }

        public Project SetTarget(Project target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;
            Model.Items = Target.Items;
            Model.SelectedItem = null;
            return target;
        }

        public ProjectPage()
        {
            Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new ListViewModel<Group>(null/* , this.Resources["viewEditTemplateSelector"] as ViewEditTemplateSelector */);
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Target.Items.Add(new Group(Project.IntID.NewID(), "Новая группа {0}", "room.png"));
        }

        private void GroupsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Model.SelectedItem != e.Item)
            {
                Model.SelectedItem = e.Item as Group;
            }
            else
            {
                MainPage.Instance.CurrentPage = GroupPage.Instance;
                GroupPage.Instance.SetTarget((e.Item as Group));
            }

        }

        public async void DeleteItem(Group item)
        {
            var answer = await DisplayAlert("Удалить", "Вы действительно хотите удалить группу?", "Да", "Нет");
            if (answer)
            {
            }
        }

        private async void IconButton_Clicked(object sender, EventArgs e)
        {
            await ProjectMedia.GetPhoto(this, "project_", (r) => {
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
                var d = b.Data as Group;
                if (d != null)
                    DeleteItem(d);
            }
        }


    }
}