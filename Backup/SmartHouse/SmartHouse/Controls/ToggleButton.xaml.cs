using Xamarin.Forms;
using System;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SmartHouse.Controls
{
    public partial class ToggleButton : Grid
    {
        public static readonly BindableProperty OnImageSourceProperty = BindableProperty.Create(
                                                                propertyName: "OnImageSource",
                                                                returnType: typeof(string),
                                                                declaringType: typeof(ToggleButton),
                                                                defaultValue: "",
                                                                defaultBindingMode: BindingMode.TwoWay,
                                                                propertyChanged: OnImageSourcePropertyChanged);

        private static void OnImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ToggleButton)bindable;
            control.OnImageSource = newValue.ToString();
        }

        public ImageSource OnImageSource
        {
            get
            {
                return this.OnImage.Source;
            }
            set
            {
                this.OnImage.Source = value;
            }
        }

        public static readonly BindableProperty OffImageSourceProperty = BindableProperty.Create(
                                                                propertyName: "OffImageSource",
                                                                returnType: typeof(string),
                                                                declaringType: typeof(ToggleButton),
                                                                defaultValue: "",
                                                                defaultBindingMode: BindingMode.TwoWay,
                                                                propertyChanged: OffImageSourcePropertyChanged);

        private static void OffImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ToggleButton)bindable;
            control.OffImageSource = newValue.ToString();
        }

        public ImageSource OffImageSource
        {
            get
            {
                return this.OffImage.Source;
            }
            set
            {
                this.OffImage.Source = value;
            }
        }

        public static readonly BindableProperty ToggledProperty = BindableProperty.Create(
                                                                propertyName: "Toggled",
                                                                returnType: typeof(string),
                                                                declaringType: typeof(ToggleButton),
                                                                defaultValue: "",
                                                                defaultBindingMode: BindingMode.TwoWay,
                                                                propertyChanged: ToggledPropertyChanged);

        private static void ToggledPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ToggleButton)bindable;
            bool t;
            if (bool.TryParse(newValue as string, out t))
                control.Toggled = t;
        } 

        private bool toggled = false;
        public bool Toggled
        {
            get => toggled;
            set {
                toggled = value;
                SetValue(ToggledProperty, value.ToString());
                this.OnImage.IsVisible = toggled;
                this.OffImage.IsVisible = !toggled;
            }
        }

        public event EventHandler ToggledChanged;

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Toggled = !Toggled;
            ToggledChanged?.Invoke(this, null);
        }

            public ToggleButton()
        {
            this.InitializeComponent();
        }
    }
}
