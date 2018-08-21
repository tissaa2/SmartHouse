using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHouse
{
    public class ImageButton : Image
    {

        // public static readonly BindableProperty CommandProperty = BindableProperty.Create<ImageButton, ICommand>(p => p.Command, null);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("CommandProperty", typeof(BindableProperty), typeof(ImageButton));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty DataProperty = BindableProperty.Create("DataProperty", typeof(object), typeof(ImageButton));
        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }


        // public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<ImageButton, object>(p => p.CommandParameter, null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameterProperty", typeof(BindableProperty), typeof(ImageButton));
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public event EventHandler OnPressed;

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.AnchorX = 0.48;
                    this.AnchorY = 0.48;
                    await this.ScaleTo(0.8, 50, Easing.Linear);
                    await Task.Delay(100);
                    await this.ScaleTo(1, 50, Easing.Linear);
                    if (Command != null)
                    {
                        Command.Execute(CommandParameter);
                    }
                    OnPressed?.Invoke(this, new EventArgs() { });
                });
            }
        }

        public ImageButton()
        {
            Initialize();
        }


        public void Initialize()
        {
            GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = TransitionCommand
            });
        }

    }
}

