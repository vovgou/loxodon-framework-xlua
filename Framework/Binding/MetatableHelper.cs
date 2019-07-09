#if XLUA
using XLua;

namespace Loxodon.Framework.Binding
{
    [CSharpCallLua]
    delegate LuaTable GetMetatableHandler(object target);

    [CSharpCallLua]
    delegate LuaTable SetMetatableHandler(object target, LuaTable metatable);

    public static class MetatableHelper
    {
        static GetMetatableHandler getter;
        static SetMetatableHandler setter;
        public static LuaTable GetMetatable(object target)
        {
            if (getter != null)
                return getter(target);

            getter = LuaEnvironment.LuaEnv.Global.GetInPath<GetMetatableHandler>("debug.getmetatable");
            if (getter == null)
                return null;
            return getter(target);
        }

        public static void SetMetatable(object target, LuaTable metatable)
        {
            if (setter != null)
            {
                setter(target, metatable);
                return;
            }

            setter = LuaEnvironment.LuaEnv.Global.GetInPath<SetMetatableHandler>("debug.setmetatable");
            if (setter != null)
                setter(target, metatable);
        }
    }
}
#endif