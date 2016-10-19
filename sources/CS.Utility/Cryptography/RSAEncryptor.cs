using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CS.Extension;

namespace CS.Cryptography
{
    /// <summary>
    /// 类似于RSACryptoServiceProvider的RSA非对称加密
    /// <remarks>
    /// 公钥加密，私钥解密；私钥加密，公钥解密
    /// 参考：
    /// http://www.cnblogs.com/kuaileguaiwu/articles/1440471.html
    /// </remarks>
    /// </summary>
    public class RSAEncryptor
    {
        /// <summary>
        /// RSA的容器 可以解密的源字符串长度为 DWKEYSIZE/8-11 
        /// </summary>
        public const int DWKEYSIZE = 1024;

        private readonly RSACryptoServiceProvider _provider;

        /// <summary>
        /// 用公钥初始化
        /// </summary>
        /// <param name="publicKey"></param>
        public RSAEncryptor(byte[] publicKey)
        {
            PublicKey = publicKey;
            //_provider = new RSACryptoServiceProvider(DWKEYSIZE);
            //_provider.ImportCspBlob(publicKey);
            ////取得RSA容易里的各种参数//公钥不能导出参数
            //var pm = _provider.ExportParameters(true);
            //KeyModulus = pm.Modulus;
            //KeyPublicExponent = pm.Exponent;
            InitPublicKey(publicKey);
        }

        /// <summary>
        /// 通过一个容器Key来存取公钥私钥对
        /// <remarks>没有时会自动创建</remarks>
        /// </summary>
        /// <param name="keyContainerName">通过容器Key获取公私钥对</param>
        public RSAEncryptor(string keyContainerName)
        {
            //RSACryptoServiceProvider.UseMachineKeyStore = true;
            var cp = new CspParameters { KeyContainerName = keyContainerName };
            _provider = new RSACryptoServiceProvider(DWKEYSIZE, cp);

            //PublicKey = _provider.ExportCspBlob(false);
            PrivateKey = _provider.ExportCspBlob(true);
            //取得RSA容易里的各种参数
            var pm = _provider.ExportParameters(true);
            KeyModulus = pm.Modulus;
            KeyPublicExponent = pm.Exponent;
            KeyPrivateD = pm.D;

            PublicKey = GetPublicKey(pm.Modulus, pm.Exponent);
        }

        private static byte[] GetPublicKey(byte[] modulus, byte[] exponent)
        {
            //var bs = new byte[modulus.Length + exponent.Length + 4];
            var result = new List<byte>();
            result.AddRange(modulus.Length.ToBytes());
            result.AddRange(modulus);
            result.AddRange(exponent);
            return result.ToArray();
        }

        private void InitPublicKey(byte[] publicKey)
        {
            var allSize = publicKey.Length;
            var modulusSize = BitConverter.ToInt32(publicKey, 0);
            KeyModulus = new byte[modulusSize];
            KeyPublicExponent = new byte[allSize - 4 - modulusSize];
            Buffer.BlockCopy(publicKey, 4, KeyModulus, 0, modulusSize);
            Buffer.BlockCopy(publicKey, 4 + modulusSize, KeyPublicExponent, 0, allSize - 4 - modulusSize);
        }


        /// <summary>
        /// 从容器中删除当前的公私钥对
        /// </summary>
        /// <param name="keyContainerName">容器里的名称</param>
        public static void Delete(string keyContainerName)
        {
            var cp = new CspParameters { KeyContainerName = keyContainerName };
            var rsa = new RSACryptoServiceProvider(cp) { PersistKeyInCsp = false };
            rsa.Clear();
        }

        #region 自定义RSA加密用到的三个参数
        /// <summary>
        /// 公私钥都用到的Modulus（系数）
        /// </summary>
        protected byte[] KeyModulus { get; set; }
        /// <summary>
        /// 公钥用到的指数（指数）
        /// </summary>
        protected byte[] KeyPublicExponent { get; set; }
        /// <summary>
        /// 私钥用到的参数D(注意：当用PublicKey来初始化时该参数不可用)
        /// </summary>
        protected byte[] KeyPrivateD { get; set; }
        #endregion

        /// <summary>
        /// 公钥
        /// </summary>
        public byte[] PublicKey { get; set; }
        /// <summary>
        /// 私钥，不对外部公开(注意：当用PublicKey来初始化时该参数不可用)
        /// </summary>
        protected byte[] PrivateKey { get; set; }


        #region 加密与解密

        #region 检查明文的有效性 DWKEYSIZE/8-11 长度之内为有效 中英文都算一个字符
        /// <summary>
        /// 检查明文的有效性 DWKEYSIZE/8-11 长度之内为有效
        /// <remarks>太长了太耗时间了</remarks>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsValidate(byte[] source)
        {
            return (DWKEYSIZE / 8 - 11) >= source.Length;
        }
        #endregion

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">加密的源内容</param>
        /// <param name="usePrivateKey">true为私钥加密(用公钥解密)；false为公钥加密(用私解密)</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] source, bool usePrivateKey)
        {
            if (!IsValidate(source)) throw new ArgumentException("the source context is too long.", "source");
            return usePrivateKey ? EncryptWithPrivateKey(source) : EncryptWithPublicKey(source);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="usePublicKey">true为公钥解密(私钥加密的)；false为私钥解密t(公钥加密的)</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] ciphertext, bool usePublicKey)
        {
            return usePublicKey ? DecryptWithPublicKey(ciphertext) : DecryptWithPrivateKey(ciphertext);
        }

        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private byte[] EncryptWithPublicKey(byte[] source)
        {
            var biD = new BigInteger(KeyPublicExponent);
            var biN = new BigInteger(KeyModulus);
            var result = Encrypt(source, biD, biN);
            return result;
        }
        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private byte[] EncryptWithPrivateKey(byte[] source)
        {
            var biD = new BigInteger(KeyPrivateD);
            var biN = new BigInteger(KeyModulus);
            var result = Encrypt(source, biD, biN);
            return result;
        }

        /// <summary>
        /// 用指定的密匙加密 
        /// </summary>
        /// <param name="source">明文</param>
        /// <param name="d">密钥大素数（可以是RSACryptoServiceProvider生成的D或是Exponent）</param>
        /// <param name="n">大整数N（可以是RSACryptoServiceProvider生成的Modulus）</param>
        /// <returns>返回密文</returns>
        private static byte[] Encrypt(byte[] source, BigInteger d, BigInteger n)
        {
            var len = source.Length;
            int len1;
            if ((len % 120) == 0)
                len1 = len / 120;
            else
                len1 = len / 120 + 1;
            var tempbytes = new List<byte>();
            for (int i = 0; i < len1; i++)
            {
                var blockLen = len >= 120 ? 120 : len;
                var oText = new byte[blockLen];
                Array.Copy(source, i * 120, oText, 0, blockLen);
                //string res = Encoding.UTF8.GetString(oText);
                var biText = new BigInteger(oText);
                var biEnText = biText.ModPow(d, n);
                //补位
                //byte[] testbyte = null;
                var resultStr = biEnText.ToHexString();
                if (resultStr.Length < 256)
                {
                    while (resultStr.Length != 256)
                    {
                        resultStr = "0" + resultStr;
                    }
                }
                var returnBytes = new byte[128];
                for (var j = 0; j < returnBytes.Length; j++)
                    returnBytes[j] = Convert.ToByte(resultStr.Substring(j * 2, 2), 16);
                tempbytes.AddRange(returnBytes);
                len -= blockLen;
            }
            return tempbytes.ToArray();
        }


        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        private byte[] DecryptWithPublicKey(byte[] ciphertext)
        {
            var biE = new BigInteger(KeyPublicExponent);
            var biN = new BigInteger(KeyModulus);
            var result = Decrypt(ciphertext, biE, biN);
            return result;
        }

        /// <summary>
        /// 私钥解密
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        private byte[] DecryptWithPrivateKey(byte[] ciphertext)
        {
            var biE = new BigInteger(KeyPrivateD);
            var biN = new BigInteger(KeyModulus);
            var result = Decrypt(ciphertext, biE, biN);
            return result;
        }


        /// <summary>
        /// 用指定的密匙解密 
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="e">密钥大素数(可以是RSACryptoServiceProvider生成的Exponent或者D)</param>
        /// <param name="n">大整数N(可以是RSACryptoServiceProvider生成的Modulus)</param>
        /// <returns>返回明文</returns>
        private static byte[] Decrypt(byte[] ciphertext, BigInteger e, BigInteger n)
        {
            var len = ciphertext.Length;
            int len1;
            if (len % 128 == 0)
                len1 = len / 128;
            else
                len1 = len / 128 + 1;
            var result = new List<byte>();
            for (int i = 0; i < len1; i++)
            {
                var blockLen = len >= 128 ? 128 : len;
                var oText = new byte[blockLen];
                Array.Copy(ciphertext, i * 128, oText, 0, blockLen);
                var biText = new BigInteger(oText);
                var biEnText = biText.ModPow(e, n);
                result.AddRange(biEnText.GetBytes());
                len -= blockLen;
            }
            return result.ToArray();

        }

        #endregion



    }
}