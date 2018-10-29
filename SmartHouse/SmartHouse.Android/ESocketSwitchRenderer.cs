using Android.Graphics.Drawables;
using SmartHouse.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Color = Android.Graphics.Color;
using Android.Content;
using static Android.Views.View;
using Android.Views;
using System.ComponentModel;
using Android.OS;

[assembly: ExportRenderer(typeof(ESocketSwitch), typeof(SmartHouse.Droid.ESocketSwitchRenderer))]
namespace SmartHouse.Droid
{
    public class ESocketSwitchRenderer : SwitchRenderer
    {
        // public new Context Context { get; set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetThumbResource(Resource.Drawable.esocketswitch_thumb);
                Control.SetTrackResource(Resource.Drawable.esocketswitch_track);
                Control.TextOn = "Вкл";
                Control.TextOff = "Выкл";
            }

        }

    }
}
