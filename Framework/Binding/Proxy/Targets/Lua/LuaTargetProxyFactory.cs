#if XLUA
using XLua;

namespace Loxodon.Framework.Binding.Proxy.Targets
{
    public class LuaTargetProxyFactory : ITargetProxyFactory
    {
        public ITargetProxy CreateProxy(object target, BindingDescription description)
        {
            if (target == null || !(target is ILuaExtendable))
                return null;

            LuaTable metatable = (target as ILuaExtendable).GetMetatable();
            if (metatable == null || !metatable.ContainsKey(description.TargetName))
                return null;

            LuaFunction function = metatable.Get<LuaFunction>(description.TargetName);
            if (function == null)
                return null;

            return new LuaMethodTargetProxy(target, function);
        }
    }
}
#endif