#if XLUA
using Loxodon.Log;
using System;
using XLua;

namespace Loxodon.Framework.Binding.Proxy
{
    public class LuaInvoker : IScriptInvoker
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LuaInvoker));

        private readonly WeakReference target;
        private LuaFunction function;
        public LuaInvoker(object target, LuaFunction function)
        {
            if (target == null)
                throw new ArgumentNullException("target", "Unable to bind to target as it's null");

            this.target = new WeakReference(target, true);
            this.function = function;
        }

        public object Target { get { return this.target != null && this.target.IsAlive ? this.target.Target : null; } }

        public object Invoke(params object[] args)
        {
            try
            {
                var target = this.Target;
                if (target == null)
                    return null;

                int length = args != null ? args.Length + 1 : 1;
                object[] parameters = new object[length];
                parameters[0] = target;
                if (args != null && args.Length > 0)
                    Array.Copy(args, 0, parameters, 1, args.Length);

                return this.function.Call(parameters);
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.WarnFormat("{0}", e);
            }
            return null;
        }
    }
}
#endif