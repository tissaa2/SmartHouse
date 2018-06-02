using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Services;

namespace SmartHouse.Views
{
    public partial class MainPage : TabbedPage
    {
        public static MainPage Instance = null;

        public Client ClientManager;

        public MainPage()
        {
            Instance = this;
            Children.Add(new ProjectsPage() { Title = "Проекты" });
            ProjectPage pp = new ProjectPage() { Title = "Проект" };
            
            Children.Add(pp);
            Children.Add(new GroupPage() { Title = "Группа" });
            // Children.Add(new ScenesPage() { Title = "Группа" });
            Children.Add(new DebugPage() { Title = "Отладка" });
            // Children.Add(new EditorPage() { Title = "Редактор" });
            this.InitializeComponent();
        }
    }
}
