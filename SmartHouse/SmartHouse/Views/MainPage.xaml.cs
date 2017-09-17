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
            Children.Add(new LightsPage() { Title = "Свет" });
            Children.Add(new DebugPage() { Title = "Отладка" });
            Children.Add(new EditorPage() { Title = "Редактор" });
            this.InitializeComponent();
        }
    }
}
