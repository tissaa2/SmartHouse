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
using Android.Graphics;
using Android.Util;

[assembly: ExportRenderer(typeof(EFrame), typeof(SmartHouse.Droid.EFrameRenderer))]
namespace SmartHouse.Droid
{
    public class EFrameRenderer : FrameRenderer
    {

        // public new Context Context { get; set; }

        public EFrameRenderer(Context context) : base()
        {
            AutoPackage = false;
            // Context = context;
        }

        public EFrameRenderer()
        {
            AutoPackage = false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            // var sw = Application.Current.MainPage.Width;
            (Element as EFrame).ProcessTouchEvent(new Controls.TouchEventArgs() { TouchPosition = new Coordinates() { X = e.GetX() / Resources.DisplayMetrics.Density, Y = e.GetY() / Resources.DisplayMetrics.Density } } );
            return base.OnTouchEvent(e);
            // return true;
        }

        /* protected override Android.Views.Fr] CreateNativeControl()
        {
            return new Android.Views.View(Context);
        } */

    }
}
