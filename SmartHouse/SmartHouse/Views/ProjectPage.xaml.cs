using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectPage : ContentPage
	{
        public static ProjectPage Instance = null;


        public ProjectPage()
        {
            Instance = this;
            this.InitializeComponent();
            // ProjectsListView.ItemsSource = Projects.Instance;
        }
    }
}