#if XLUA
using System;
using XLua;

namespace Loxodon.Framework.Binding.Proxy.Targets
{
    public class LuaMethodTargetProxy : TargetProxyBase, IObtainable
    {
        private IScriptInvoker invoker;

        public LuaMethodTargetProxy(object target, LuaFunction function) : base(target)
        {
            if (target == null)
                throw new ArgumentNullException("target", "Unable to bind to target as it's null");

            this.invoker = new LuaInvoker(target, function);
        }

        public override BindingMode DefaultMode { get { return BindingMode.OneWayToSource; } }

        public override Type Type { get { return typeof(LuaFunction); } }

        public object GetValue()
        {
            return this.invoker;
        }

        public TValue GetValue<TValue>()
        {
            return (TValue)this.invoker;
        }
    }
}
#endif