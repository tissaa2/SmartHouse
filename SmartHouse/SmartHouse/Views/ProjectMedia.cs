using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace SmartHouse.Views
{

    public delegate void GetPhotoCallbackDelegate(string result);
    public static class ProjectMedia
    {

        // public static IPhotoPicker PhotoPicker { get; set; }
        public static async Task<string> GetPhoto(Page pg, string resourcesFilter, GetPhotoCallbackDelegate onDone)
        {
            /* if (!CrossMedia.IsSupported)
                return null; */
            // var at = await pg.DisplayActionSheet("Выберите источник изображения", "Отмена", null, "Галерея", "Камера");
            await PhotoPickerPage.ShowModal(pg, resourcesFilter, async (s, e) => {
                var at = (s as PhotoPickerPage).Result; 
                if (at == "gallery.png" || at == "camera.png")
                {
                    await CrossMedia.Current.Initialize();
                    if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                    {
                        // Supply media options for saving our photo after it's taken.
                        var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        {
                            Directory = "Receipts",
                            Name = $"{DateTime.UtcNow}.jpg"
                        };

                        // Take a photo of the business receipt.

                        // var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                        var f = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                        // callback?.Invoke(f);
                        if (onDone != null)
                            onDone(f.Path);
                    }
                }
                else
                {
                    if (onDone != null)
                        onDone(at);
                }
            });


            return null;

        }
    }
}
