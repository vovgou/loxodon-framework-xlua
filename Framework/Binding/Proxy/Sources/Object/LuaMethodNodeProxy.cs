#if XLUA
using System;

using XLua;

namespace Loxodon.Framework.Binding.Proxy.Sources.Object
{
    public class LuaMethodNodeProxy : SourceProxyBase, IObtainable
    {
        private bool disposed = false;
        private IScriptInvoker invoker;
        public LuaMethodNodeProxy(LuaTable source, LuaFunction function) : base(source)
        {
            if (source == null)
                throw new ArgumentException("source");

            if (function == null)
                throw new ArgumentException("function");

            this.invoker = new LuaInvoker(source, function);
        }

        public override Type Type { get { return typeof(IScriptInvoker); } }

        public object GetValue()
        {
            return this.invoker;
        }

        public TValue GetValue<TValue>()
        {
            return (TValue)this.invoker;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (invoker != null && invoker is IDisposable)
                    {
                        (invoker as IDisposable).Dispose();
                        invoker = null;
                    }
                }
                disposed = true;
                base.Dispose(disposing);
            }
        }
    }
}
#endif
