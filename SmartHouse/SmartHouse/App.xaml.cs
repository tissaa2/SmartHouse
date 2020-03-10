using SmartHouse.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using SmartHouse.Models.Physics;
using System.Threading;
using SmartHouse.Models.Logic;

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
            SaveDataToDeviceThread = new Thread(SaveDataToDeviceThreadProc);
            SaveDataToDeviceThread.Start();
        }

        public bool SaveDataToDeviceThreadTerminated;
        private Thread SaveDataToDeviceThread = null;
        private void SaveDataToDeviceThreadProc()
        {
            SaveDataToDeviceThreadTerminated = false;
            while(!SaveDataToDeviceThreadTerminated)
            {
                if (ProjectsList.Instance.IsDirty)
                {
                    ProjectsList.Instance.Save();
                    ProjectsList.Instance.IsDirty = false;
                }
                Thread.Sleep(1000);
            }
        }

        public static void SetMainPage()
        {
            Client = Services.Client.Instance;
            Current.MainPage = new MainPage(new ProjectsListPage());
            // Current.MainPage = new MainPage(new DevicesBrowserPage());
            // var ds = PDevice.All;
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
