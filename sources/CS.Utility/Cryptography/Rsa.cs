using System;
using System.Security.Cryptography;
using System.Text;

namespace CS.Cryptography
{
    /// <summary>
    /// RSA非对称加密
    /// <remarks>
    /// MS的默认实现有问题，即只能公钥加密，私钥解密。
    /// </remarks>
    /// </summary>
    public class Rsa
    {

        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey => _rsaProvider.ToXmlString(false);

        /// <summary>
        /// 公钥+私钥
        /// <remarks> //true 表示同时包含 RSA 公钥和私钥；false 表示仅包含公钥。</remarks>
        /// </summary>
        public string AllKey => _rsaProvider.ToXmlString(true);

        private RSACryptoServiceProvider _rsaProvider;
        /// <summary>
        /// 用公钥或私钥初始化加密对象
        /// </summary>
        /// <param name="key"></param>
        public Rsa(string key)
            : this()
        {
            _rsaProvider.FromXmlString(key);
        }
        /// <summary>
        /// 初始化实例
        /// </summary>
        public Rsa()
        {
            _rsaProvider = new RSACryptoServiceProvider();
        }

        /// <summary>
        ///     RSA 加密
        /// </summary>
        /// <param name="source">明文</param>
        /// <exception cref="ArgumentException"></exception>
        public byte[] Encrypt(byte[] source)
        {
            if (source.Length > 117) throw new ArgumentException("source", "要加密的字节流不得大于117位");
            //_rsaProvider.FromXmlString(publicKey);
            var bs = _rsaProvider.Encrypt(source, false);
            return bs;
        }

        /// <summary>
        ///     RSA 解密
        /// </summary>
        /// <param name="source">密文字节流</param>
        public byte[] Decrypt(byte[] source)
        {
            //_rsaProvider.FromXmlString(privateKey);
            var bs = _rsaProvider.Decrypt(source, false);
            return bs;
        }

        #region 静态方法

        /// <summary>
        /// 用公钥加密字符串
        /// </summary>
        /// <param name="source">源有长度限制</param>
        /// <param name="publicKey"></param>
        /// <returns>返回加密后的Base64字符串</returns>
        public static string Encrypt(string source, string publicKey)
        {
            var bs = Encoding.UTF8.GetBytes(source);
            var rsa = new Rsa(publicKey);
            var rst = rsa.Encrypt(bs);
            return Convert.ToBase64String(rst);
        }

        /// <summary>
        /// 用私钥解密经加密后的Base64字符串
        /// </summary>
        /// <param name="base64Source"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Decrypt(string base64Source, string privateKey)
        {
            var bs = Convert.FromBase64String(base64Source);
            var rsa = new Rsa(privateKey);
            var rst = rsa.Decrypt(bs);
            return Encoding.UTF8.GetString(rst);
        }

        #endregion

        #region 容器保存

        /// <summary>
        /// 保存至密钥容器
        /// </summary>
        /// <param name="containerName"></param>
        public void SaveToInContainer(string containerName)
        {
            var cp = new CspParameters { KeyContainerName = containerName };
            var rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(AllKey);
        }
        /// <summary>
        /// 从密钥容器中取出
        /// <remarks>如果没有取到则自动生成一个，此时公钥，私钥都变化了</remarks>
        /// </summary>
        /// <param name="containerName"></param>
        public void GetKeyFromContainer(string containerName)
        {
            var cp = new CspParameters { KeyContainerName = containerName };
            _rsaProvider = new RSACryptoServiceProvider(cp);
        }

        /// <summary>
        /// 删除密钥
        /// </summary>
        /// <param name="containerName"></param>
        public void DeleteKeyFromContainer(string containerName)
        {
            var cp = new CspParameters { KeyContainerName = containerName };
            var rsa = new RSACryptoServiceProvider(cp)
            {
                // Delete the key entry in the container.
                PersistKeyInCsp = false
            };
            // Call Clear to release resources and delete the key from the container.
            rsa.Clear();
        }

        #endregion

    }
}