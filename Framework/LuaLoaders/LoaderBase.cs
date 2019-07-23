#if XLUA
using XLua;

namespace Loxodon.Framework.XLua.Loaders
{
    public abstract class LoaderBase
    {
        protected abstract byte[] Load(ref string path);

        public static implicit operator LuaEnv.CustomLoader(LoaderBase loader)
        {
            return loader.Load;
        }
    }
}
#endif