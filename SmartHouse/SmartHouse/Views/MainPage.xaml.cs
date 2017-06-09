using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Services;

namespace SmartHouse.Views
{
    public partial class MainPage : ContentPage
    {
        public static MainPage Instance = null;

        public Client ClientManager;

        public void AddToLog(LogEntry entry)
        {
            Device.BeginInvokeOnMainThread(delegate
            {
                (this.LogListView.ItemsSource as ObservableCollection<LogEntry>).Insert(0, entry);
            });
        }

        public MainPage()
        {
            MainPage.Instance = this;
            this.InitializeComponent();
            this.LogListView.ItemsSource = new ObservableCollection<LogEntry>();
            this.ClientManager = new Client(new IPEndPoint(IPAddress.Any, 0));
            this.ClientManager.Start(61576);
        }

        private void BroadcastButton_Clicked(object sender, EventArgs e)
        {
            int port;
            bool flag = int.TryParse(this.PortEntry.Text, out port);
            if (flag)
            {
                this.ClientManager.Broadcast(Utils.HexStringToBytes(this.MessageEntry.Text), port);
            }
            else
            {
                Log.Write(string.Format("Error broadcasting message '{0}': incorrect port", this.MessageEntry.Text));
            }
        }

        private void TestButton0_Clicked(object sender, EventArgs e)
        {
            this.ClientManager.ActivateScene(0);
        }

        private void TestButton1_Clicked(object sender, EventArgs e)
        {
            this.ClientManager.ActivateScene(1);
        }

        private void TestButton2_Clicked(object sender, EventArgs e)
        {
            this.ClientManager.ActivateScene(2);
        }

        private void TestButton3_Clicked(object sender, EventArgs e)
        {
            this.ClientManager.ActivateScene(3);
        }

    }
}
