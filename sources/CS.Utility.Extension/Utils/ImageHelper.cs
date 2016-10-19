using System;
using System.Drawing;
using System.IO;
using System.Net.Mime;

namespace CS.Utils
{
    /// <summary>
    /// 图像简单辅助
    /// </summary>
    public class ImageHelper
    {

        /// <summary>
        /// 将图像进行缩放
        /// </summary>
        /// <param name="base64Image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap Zoom(string base64Image, int width, int height)
        {
            return Zoom(Convert.FromBase64String(base64Image), width, height);
        }

        /// <summary>
        /// 将图像进行缩放
        /// </summary>
        /// <returns></returns>
        public static Bitmap Zoom(byte[] imageSource,int width,int height)
        {
            using (var imgStream = new MemoryStream())
            {
                imgStream.Write(imageSource, 0, imageSource.Length);
                using (var imamge = Image.FromStream(imgStream))
                {
                    using (var bitmap = new Bitmap(width, height, imamge.PixelFormat))
                    {
                        using (var g = Graphics.FromImage(bitmap))
                        {
                            g.Clear(Color.Transparent);
                            g.DrawImage(imamge, new RectangleF(0, 0, width, height));
                            return Image.FromHbitmap(bitmap.GetHbitmap());
                        }
                    }
                }
            }
        }
    }
}