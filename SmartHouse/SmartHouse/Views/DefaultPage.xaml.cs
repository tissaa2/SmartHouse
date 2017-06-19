using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Services;

namespace SmartHouse.Views
{
    public partial class DefaultPage : ContentPage
    {
        public static DefaultPage Instance = null;

        public DefaultPage()
        {
            Instance = this;
            this.InitializeComponent();
        }

    }
}
