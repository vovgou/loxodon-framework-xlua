#if XLUA
using Loxodon.Framework.Binding.Paths;
using Loxodon.Framework.Binding.Proxy.Sources;
using Loxodon.Framework.Binding.Proxy.Sources.Expressions;
using Loxodon.Framework.Binding.Proxy.Sources.Object;
using Loxodon.Framework.Binding.Proxy.Targets;
using Loxodon.Framework.Services;

namespace Loxodon.Framework.Binding
{
    public class LuaBindingServiceBundle : BindingServiceBundle
    {
        public LuaBindingServiceBundle(IServiceContainer container) : base(container)
        {
        }

        protected override void OnStart(IServiceContainer container)
        {
            base.OnStart(container);

            /* Support XLua */
            INodeProxyFactoryRegister objectSourceProxyFactoryRegistry = container.Resolve<INodeProxyFactoryRegister>();
            objectSourceProxyFactoryRegistry.Register(new LuaNodeProxyFactory(), 20);

            IPathParser pathParser = container.Resolve<IPathParser>();
            ISourceProxyFactory sourceFactoryService = container.Resolve<ISourceProxyFactory>();
            ISourceProxyFactoryRegistry sourceProxyFactoryRegistry = container.Resolve<ISourceProxyFactoryRegistry>();
            sourceProxyFactoryRegistry.Register(new LuaExpressionSourceProxyFactory(sourceFactoryService, pathParser), 20);

            ITargetProxyFactoryRegister targetProxyFactoryRegister = container.Resolve<ITargetProxyFactoryRegister>();
            targetProxyFactoryRegister.Register(new LuaTargetProxyFactory(), 30);
        }

        protected override void OnStop(IServiceContainer container)
        {
            base.OnStop(container);
        }
    }
}
#endif