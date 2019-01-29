using System;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartHouse.Controls
{
    public class ESliderValueChangeEvents : EventArgs
    {
        public double Value { get; set; }
    }

    public delegate void ESliderValueChangeDelegate(object sender, ESliderValueChangeEvents args);

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ESlider : AbsoluteLayout
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

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static void CaptionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ESlider).CaptionLabel.Text = (string)newValue;
        }


        // public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(double), typeof(ESlider), default(double), BindingMode.TwoWay);
        public static readonly BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(double), typeof(ESlider), 0d, BindingMode.TwoWay,
            coerceValue: (bindable, value) =>
            {
                var slider = (ESlider)bindable;
                return ((double)value).Clamp(slider.Min, slider.Max);
            },
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var slider = (ESlider)bindable;
                slider.Value = (double)newValue;
                // slider.ValueChanged?.Invoke(slider, new ESliderValueChangeDelegate() (double)oldValue, (double)newValue));
            });

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                UpdateValue(value);
            }
        }

        // public static readonly BindableProperty ProgressBarWidthProperty = BindableProperty.Create("ProgressBarWidth", typeof(double), typeof(ESlider), 0d);
        public double ProgressBarWidth
        {
            get { return (Value / Delta) * Width;  } 
            set
            {
                OnPropertyChanged("ProgressBarWidth");
            }
        }

        public double Delta
        {
            get { return Max - Min; }
        }


        protected void UpdateValue(double value)
        {
            double val = ClipValue(value);
            SetValue(ValueProperty, val);
            UpdateVisuals();
            ValueChanged?.Invoke(this, new ESliderValueChangeEvents() { Value = val });
        }

        protected void UpdateVisuals()
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (ProgressBox != null)
                //ProgressBox.WidthRequest = (Value / (Delta)) * Width;
                {
                    AbsoluteLayout.SetLayoutBounds(ProgressBox, new Rectangle(0, 0, (Value / (Delta)) * Width, 1));
                    AbsoluteLayout.SetLayoutFlags(ProgressBox, AbsoluteLayoutFlags.HeightProportional);
                    // ForceLayout();
                    InvalidateLayout();
                }
            });
        }

        // public Command TapCommand => new Command<Foo>((fooObject) => Tapped(fooObject));
        public ESlider()
        {
            // BindingContext = this;
            InitializeComponent();
            SizeChanged += DoSizeChanged;
        }

        public void DoSizeChanged(object sender, EventArgs e)
        {
            UpdateVisuals();
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var coords = Frame.LastTouchPosition;
            // CaptionLabel.Text = Caption;
            Value = coords.X * Delta / (Width);
            // ProgressBox.WidthRequest = args.X;
            // ProgressGrid.ColumnDefinitions[1].Width = Width - args.X;

        }

        private void EFrame_Touched(object sender, TouchEventArgs args)
        {
            CaptionLabel.Text = Caption;
            Value = args.TouchPosition.X * Delta / (Width);
        }
    }
}