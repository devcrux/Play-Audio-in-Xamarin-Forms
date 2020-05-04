using MusicApp.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Image), typeof(CustomImageRenderer))]
namespace MusicApp.UWP
{
    public class CustomImageRenderer : ImageRenderer
    {
        private string _imagePrefix = "Assets\\";

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if (Element.Source is FileImageSource)
                {
                    ImageSource source = Element.Source;

                    var fileSource = source as FileImageSource;

                    if (fileSource != null)
                    {
                        var filePath = fileSource.File;
                        if (!filePath.StartsWith(_imagePrefix))
                            filePath = _imagePrefix + filePath;

                        if (!filePath.EndsWith(".png"))
                            filePath += ".png";

                        if (filePath != fileSource.File)
                            fileSource.File = filePath;

                        Element.Source = filePath;

                        RefreshImage();
                    }
                }
            }
        }

        void RefreshImage()
        {
            ((IVisualElementController)Element)?.InvalidateMeasure(InvalidationTrigger.RendererReady);
        }
    }
}
