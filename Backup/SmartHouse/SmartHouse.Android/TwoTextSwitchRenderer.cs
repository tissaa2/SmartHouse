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

[assembly: ExportRenderer(typeof(TwoTextSwitch), typeof(SmartHouse.Droid.TwoTextSwitchRenderer))]
namespace SmartHouse.Droid
{
    public class TwoTextSwitchRenderer : SwitchRenderer
    {
        // public new Context Context { get; set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetThumbResource(Resource.Drawable.esocketswitch_thumb);
                Control.SetTrackResource(Resource.Drawable.esocketswitch_track);
                var tts = e?.NewElement as TwoTextSwitch;
                if (tts != null)
                {
                    Control.ShowText = true;
                    Control.SplitTrack = false;
                    Control.TextOn = tts.TextOn;
                    Control.TextOff = tts.TextOff;
                }
            }
        }

    }
}
