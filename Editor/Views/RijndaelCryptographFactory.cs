using System;
using System.Text;
using Loxodon.Framework.Security.Cryptography;
using UnityEngine;

namespace Loxodon.Framework.XLua.Editors
{
    [Serializable]
    public class RijndaelCryptographFactory : EncryptorFactory
    {
        [SerializeField]
        private Algorithm algorithm = Algorithm.AES128_CBC_PKCS7;

        [Multiline(2)]
        [SerializeField]
        private string iv = "5Hh2390dQlVh0AqC";

        [Multiline(5)]
        [SerializeField]
        private string key = "E4YZgiGQ0aqe5LEJ";

        public Algorithm Algorithm
        {
            get { return this.algorithm; }
            set { this.algorithm = value; }
        }

        public string IV
        {
            get { return this.iv; }
            set { this.iv = value; }
        }

        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public override IEncryptor Create()
        {
            int keySize = 128;
            switch (this.Algorithm)
            {
                case Algorithm.AES128_CBC_PKCS7:
                    keySize = 128;
                    break;
                case Algorithm.AES192_CBC_PKCS7:
                    keySize = 192;
                    break;
                case Algorithm.AES256_CBC_PKCS7:
                    keySize = 256;
                    break;
            }
            return new RijndaelCryptograph(keySize, Encoding.ASCII.GetBytes(this.Key), Encoding.ASCII.GetBytes(this.IV));
        }
    }

    public enum Algorithm
    {
        AES128_CBC_PKCS7,
        AES192_CBC_PKCS7,
        AES256_CBC_PKCS7
    }
}
