#if XLUA
using XLua;
using Loxodon.Framework.Security.Cryptography;

namespace Loxodon.Framework.XLua.Loaders
{
    public class DecodableLoader : LoaderBase
    {
        private IDecryptor decryptor;
        private LuaEnv.CustomLoader loader;

        public DecodableLoader(LuaEnv.CustomLoader loader, IDecryptor decryptor)
        {
            this.decryptor = decryptor;
            this.loader = loader;
        }

        protected override byte[] Load(ref string path)
        {
            byte[] data = this.loader(ref path);
            if (data == null)
                return null;
            return this.decryptor.Decrypt(data);
        }
    }
}
#endif