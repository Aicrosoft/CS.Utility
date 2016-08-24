using System.IO;
using System.IO.Compression;
using System.Text;

namespace CS.Utils
{
    /// <summary>
    /// 压缩与解压缩 辅助类
    /// </summary>
    public class GZipHelper
    {
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Compress(string str)
        {
            var data = Encoding.UTF8.GetBytes(str);
            return Compress(data);
        }

        /// <summary>
        /// 压缩二进制流
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var compressionStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    compressionStream.Write(data, 0, data.Length);
                    compressionStream.Flush();
                }
                //必须先关了compressionStream后才能取得正确的压缩流
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 解压二进制流
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Depress(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            using (var outStream = new MemoryStream())
            {
                using (var compressionStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    compressionStream.CopyTo(outStream);
                    compressionStream.Flush();
                }
                return outStream.ToArray();
            }
        }
    }
}