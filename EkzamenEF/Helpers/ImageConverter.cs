﻿ using System; 
using System.IO; 
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EkzamenEF.Helpers
{
    public static class ImageConverter  
    {
       
        public static byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageC));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
            catch
            {

             }
            return null;
        }

        public static Image ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            if(bytes != null)
            {
                var stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                Image image1 = new Image();
                image1.Source = image;
                return image1;
            }
            return null;
        }
        public static ImageBrush ToImageBrushConvert(Byte[] img)
        {
            ImageBrush brush;
            BitmapImage bi;
            using (var ms = new MemoryStream(img))
            {
                brush = new ImageBrush();

                bi = new BitmapImage();
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.None;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();
            }

            brush.ImageSource = bi;
            return brush;
        }

    }

}
