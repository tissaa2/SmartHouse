//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("SmartHouse.iOS.ScenePage.xaml", "ScenePage.xaml", typeof(global::SmartHouse.Views.ScenePage))]

namespace SmartHouse.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("C:\\HouseLink\\SmartHouse\\SmartHouse\\SmartHouse\\Views\\ScenePage.xaml")]
    public partial class ScenePage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Grid EditorGrid;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::SmartHouse.Controls.DevicesListView DevicesListView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Button ApplyButton;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(ScenePage));
            EditorGrid = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Grid>(this, "EditorGrid");
            DevicesListView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::SmartHouse.Controls.DevicesListView>(this, "DevicesListView");
            ApplyButton = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "ApplyButton");
        }
    }
}
