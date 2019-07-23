using Loxodon.Framework.Security.Cryptography;
using UnityEngine;

namespace Loxodon.Framework.XLua.Editors
{
    public abstract class EncryptorFactory : ScriptableObject
    {
        public abstract IEncryptor Create();
    }
}
