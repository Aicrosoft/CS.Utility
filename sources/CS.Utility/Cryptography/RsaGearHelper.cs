using System;
using System.IO;

namespace CS.Cryptography
{
    /// <summary>
    /// Rsa密码轮加密
    /// </summary>
    public class RsaGearHelper
    {
        static readonly Random rnd = new Random();
        
        /// <summary>
        /// 将Key用RSA加密
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] RsaKey(byte[] key)
        {
            var rsa = new Rsa(PublicKey);
            return rsa.Encrypt(key);
        }

        /// <summary>
        /// 解密出原始Key
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static byte[] DeRsaKey(byte[] token)
        {
            var rsa = new Rsa(PrivateKey);
            return rsa.Decrypt(token);
        }

        private static string _privateKey;
        /// <summary>
        /// 私钥
        /// </summary>
        public static string PrivateKey {
            get
            {
                if (string.IsNullOrEmpty(_privateKey))
                {
                    var file = $"{AppDomain.CurrentDomain.BaseDirectory}private.pfx";
                    _privateKey = File.ReadAllText(file);
                }
                return _privateKey;
            }
        }

        private static string _publicKey;
        /// <summary>
        /// 公钥
        /// </summary>
        public static string PublicKey {
            get
            {
                if (string.IsNullOrEmpty(_publicKey))
                {
                    var file = $"{AppDomain.CurrentDomain.BaseDirectory}public.cer";
                    _publicKey = File.ReadAllText(file);
                }
                return _publicKey;
            }
        }


        /// <summary>
        /// 生成一个随机key
        /// </summary>
        /// <returns></returns>
        public static string GetRndKey()
        {
            var buffer = new byte[128];
            rnd.NextBytes(buffer);
            var key2 = Md5.Encrypt(buffer);
            return key2;
        }

        /// <summary>
        /// 密码轮加解密，返回加密后的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] GearEncrypt(byte[] key, byte[] source)
        {
            var keyLength = key.Length;
            var kindex = 0;
            var index = 0;
            foreach (var b in source)
            {
                var k = key[kindex];
                source[index] = Convert.ToByte(k ^ b);
                if (kindex + 1 >= keyLength) kindex = 0;
                else
                {
                    kindex++;
                }
                index++;
            }
            return source;
        }

    }
}