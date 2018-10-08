using Device = SmartHouse.Models.Logic.Device;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHouse.ViewModels;

namespace SmartHouse.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicePage : ContentPage
	{
        public static DevicePage Instance = null;
        public Device Target { get; set; }
        public DevicePageModel Model { get; set; }
        public Device SetTarget(Device target)
        {
            if (target == null)
                return null;
            Target = target;
            Model.Target = target;
            return target;
        }

        public DevicePage()
        {
            Instance = this;
            this.InitializeComponent();
            BindingContext = Model = new DevicePageModel();
        }

        private void IconButton_OnPressed(object sender, System.EventArgs e)
        {

        }
    }
}