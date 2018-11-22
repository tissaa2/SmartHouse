using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Services;

namespace SmartHouse.Views
{
    public partial class MainPage : NavigationPage
    {
        public static MainPage Instance = null;

        public Client ClientManager;

        public MainPage(Page content): base(content)
        {
            Instance = this;
        }


        public MainPage()
        {
            Instance = this;
           
            
            /* Children.Add(new ProjectsListPage() { Title = "Проекты" });
            ProjectPage pp = new ProjectPage() { Title = "Проект" };
            
            Children.Add(pp);
            Children.Add(new GroupPage() { Title = "Группа" });
            Children.Add(new ScenePage() { Title = "Сцена" });
            Children.Add(new DevicePage() { Title = "Устройство" });
            // Children.Add(new ScenesPage() { Title = "Группа" });
            Children.Add(new SettingsPage() { Title = "Настройки" });
            Children.Add(new DebugPage() { Title = "Отладка" }); */
            // Children.Add(new EditorPage() { Title = "Редактор" });
            this.InitializeComponent();
            // Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);

            // this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
        }
    }
}
