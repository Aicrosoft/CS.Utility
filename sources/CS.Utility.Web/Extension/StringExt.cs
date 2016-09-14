using System.Text.RegularExpressions;

namespace CS.Extension
{
    public static class StringExt
    {

        /// <summary>
        /// 将客户端的Canvas的toDataURL()方法获得的数据转为Base64编码的图片数据
        /// </summary>
        /// <param name="imageDataUrl"></param>
        /// <returns></returns>
        public static string ToImageBase64String(this string imageDataUrl)
        {
            return Regex.Replace(imageDataUrl, @"^data:image\/(png|jpg);base64,", "");
        }


    }
}