using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Services;
using SmartHouse.Models;
using SmartHouse.Models.Core;

namespace SmartHouse.Views
{
    public partial class LightsPage : ContentPage
    {
        public static LightsPage Instance = null;


        public LightsPage()
        {
            Instance = this;
            this.InitializeComponent();
            GroupsListView.ItemsSource = House.Instance.Groups;
        }

    }
}
