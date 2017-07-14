using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.Services;

namespace SmartHouse.Views
{
    public partial class EditorPage : ContentPage
    {
        public static EditorPage Instance = null;

        public EditorPage()
        {
            Instance = this;
            // this.InitializeComponent();
        }

    }
}
