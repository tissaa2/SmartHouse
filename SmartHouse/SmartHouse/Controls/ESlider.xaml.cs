using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.Controls
{
    public class ESliderValueChangeEvents: EventArgs
    {
        public double Value { get; set; }
    }

    public delegate void ESliderValueChangeDelegate(object sender, ESliderValueChangeEvents args);

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ESlider : Grid
	{
        public event ESliderValueChangeDelegate ValueChanged;

        public double Min { get; set; } = 0;
        public double Max { get; set; } = 100;

        public double ClipValue(double value)
        {
            return value < Min ? Min : value > Max ? Max : value;
        }

        public static readonly BindableProperty CaptionProperty = BindableProperty.Create("Caption", 
            returnType: typeof(string), 
            declaringType: typeof(ESlider), 
            defaultValue: default(string), 
            propertyChanged: CaptionChanged, defaultBindingMode: BindingMode.OneWay);

        public string Caption {
            get {return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static void CaptionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ESlider).CaptionLabel.Text = (string)newValue;
        }


        public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(double), typeof(ESlider), default(double));

        private double val = 0;
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                UpdateValue(value);
            }
        }

        public double Delta
        {
            get { return Max - Min;  }
        }


        protected void UpdateValue(double value)
        {
            val = ClipValue(value);
            SetValue(ValueProperty, val);
            UpdateVisuals();
            ValueChanged?.Invoke(this, new ESliderValueChangeEvents() { Value = val });
        }

        protected void UpdateVisuals()
        {
            if (ProgressBox != null)
                ProgressBox.WidthRequest = (Value / (Max - Min)) * Width;
        }

        // public Command TapCommand => new Command<Foo>((fooObject) => Tapped(fooObject));
        public ESlider ()
		{
            // BindingContext = this;
            InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void EFrame_Touched(object sender, TouchEventArgs args)
        {
            CaptionLabel.Text = Caption;
            Value = args.X * Delta / (2 * Width);
            // ProgressBox.WidthRequest = args.X / 2;
            // ProgressGrid.ColumnDefinitions[1].Width = Width - args.X;
        }
    }
}