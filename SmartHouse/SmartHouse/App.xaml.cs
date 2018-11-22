using SmartHouse.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using SmartHouse.Models.Physics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SmartHouse
{
    public partial class App : Application
    {
        public static Services.Client Client;

        public App()
        {
            InitializeComponent();
            SetMainPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new MainPage(new ProjectsListPage());
            Client = Services.Client.Instance;
            var ds = PDevice.All;
            // CrossCurrentActivity.Current.Init(this, bundle);
            /* Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                }
            }; */
        }
    }
}
