#if XLUA
using UnityEngine;
using XLua;

namespace Loxodon.Framework
{
    [LuaCallCSharp]
    [ReflectionUse]
    public static class UnityObjectExtensions
    {
        public static bool IsDestroyed(this Object o)
        {
            return o == null;
        }
    }
}
#endif
