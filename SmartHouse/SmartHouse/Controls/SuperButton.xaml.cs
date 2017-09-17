using Xamarin.Forms;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SmartHouse
{
    public partial class SuperButton : Grid
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
                                                                propertyName: "ImageSource",
                                                                returnType: typeof(string),
                                                                declaringType: typeof(SuperButton),
                                                                defaultValue: "",
                                                                defaultBindingMode: BindingMode.TwoWay,
                                                                propertyChanged: ImageSourcePropertyChanged);

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SuperButton)bindable;
            control.ImageSource = newValue.ToString();
        }

        private Label TextControl
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        private ImageButton ImageControl
        {
            get { return this.Icon; }
            set { this.Icon = value; }
        }

        public ImageSource ImageSource
        {
            get
            {
                return this.Icon.Source;
            }
            set
            {
                this.Icon.Source = value;
            }
        }

        public string Caption
        {
            get
            {
                return Text.Text;
            }
            set
            {
                Text.Text = value;
            }
        }

        public SuperButton()
        {
            this.InitializeComponent();
        }
    }
}
