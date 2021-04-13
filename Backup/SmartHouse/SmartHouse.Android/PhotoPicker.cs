using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Provider;
using Android.App;
using Android.Content;
using Java.IO;
using Android.Net;

using SmartHouse.Views;

namespace SmartHouse.Droid
{
    public class PhotoPicker : IPhotoPicker
    {
        public string Open()
        {
            /* String[] items = { "Take Photo", "Choose from Library", "Cancel" };

            using (var dialogBuilder = new AlertDialog.Builder(MainActivity.Instance))
            {
                dialogBuilder.SetTitle("Add Photo");
                dialogBuilder.SetItems(items, (d, args) => {
                    //Take photo
                    if (args.Which == 0)
                    
                    {
                        var mediaFile = Media.MediaPicker.TakePhotoAsync(options);
                        var intent = new Intent(MediaStore.ActionImageCapture);
                        File f = new File("", String.Format("karma_{0}.jpg", Guid.NewGuid()));
                        intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(f));
                        MainActivity.Instance.StartActivityForResult(intent, 1888);
                    }
                    //Choose from gallery
                    else if (args.Which == 1)
                    {
                        var intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
                        intent.SetType("image/*");
                        MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), SELECT_FILE);
                    }
                });

                dialogBuilder.Show();
            } */
            return "";
        }
    }
}