#if XLUA
using XLua;

namespace Loxodon.Framework
{
    public interface ILuaExtendable
    {
        LuaTable GetMetatable();
    }
}
#endif