using System;
using System.Security.Cryptography;
using System.Text;

namespace CS.Cryptography
{
    /// <summary>
    ///     AES Advanced Encryption Standard
    ///     <remarks>
    ///         这个标准用来替代原先的DES
    ///     </remarks>
    /// </summary>
    public class AES
    {
        /// <summary>
        ///     有密码的AES加密
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            var keyArray = Encoding.UTF8.GetBytes(key);
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            var rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            var cTransform = rDel.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///     AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            var keyArray = Encoding.UTF8.GetBytes(key);
            var toEncryptArray = Convert.FromBase64String(toDecrypt);

            var rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            var cTransform = rDel.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        ///     随机生成16位AESkey
        /// </summary>
        /// <returns></returns>
        public static string GeyRandomKey()
        {
            var str = string.Empty;
            var rnd1 = new Random();
            var r = rnd1.Next(10, 100);
            var num2 = DateTime.Now.Ticks + r;
            var random = new Random((int) ((ulong) num2 & 0xffffffffL) | (int) (num2 >> r));
            for (var i = 0; i < 16; i++)
            {
                char ch;
                var num = random.Next();
                if (num%2 == 0)
                {
                    ch = (char) (0x30 + (ushort) (num%10));
                }
                else
                {
                    ch = (char) (0x41 + (ushort) (num%0x1a));
                }
                str = str + ch;
            }
            return str;
        }
    }
}