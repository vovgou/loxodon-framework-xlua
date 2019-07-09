#if XLUA
using Loxodon.Framework.Binding.Binders;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Binding.Contexts;
using Loxodon.Framework.Contexts;
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Loxodon.Framework.Binding.Lua
{
    [LuaCallCSharp]
    public static class LuaBehaviourBindingExtension
    {
        private static IBinder binder;
        public static IBinder Binder
        {
            get
            {
                if (binder == null)
                    binder = Context.GetApplicationContext().GetService<IBinder>();

                if (binder == null)
                    throw new Exception("Data binding service is not initialized,please create a LuaBindingServiceBundle service before using it.");

                return binder;
            }
        }

        public static IBindingContext BindingContext(this Behaviour behaviour)
        {
            if (behaviour == null || behaviour.gameObject == null)
                return null;

            BindingContextLifecycle bindingContextLifecycle = behaviour.GetComponent<BindingContextLifecycle>();
            if (bindingContextLifecycle == null)
                bindingContextLifecycle = behaviour.gameObject.AddComponent<BindingContextLifecycle>();

            IBindingContext bindingContext = bindingContextLifecycle.BindingContext;
            if (bindingContext == null)
            {
                bindingContext = new BindingContext(Binder);
                bindingContextLifecycle.BindingContext = bindingContext;
            }
            return bindingContext;
        }

        public static LuaBindingSet CreateBindingSet(this Behaviour behaviour)
        {
            IBindingContext context = behaviour.BindingContext();
            return new LuaBindingSet(context, behaviour);
        }

        public static void SetDataContext(this Behaviour behaviour, object dataContext)
        {
            behaviour.BindingContext().DataContext = dataContext;
        }

        public static void AddBinding(this Behaviour behaviour, BindingDescription bindingDescription)
        {
            behaviour.BindingContext().Add(behaviour, bindingDescription);
        }

        public static void AddBindings(this Behaviour behaviour, IEnumerable<BindingDescription> bindingDescriptions)
        {
            behaviour.BindingContext().Add(behaviour, bindingDescriptions);
        }

        public static void AddBinding(this Behaviour behaviour, object target, BindingDescription bindingDescription, object key = null)
        {
            behaviour.BindingContext().Add(target, bindingDescription, key);
        }

        public static void AddBindings(this Behaviour behaviour, object target, IEnumerable<BindingDescription> bindingDescriptions, object key = null)
        {
            behaviour.BindingContext().Add(target, bindingDescriptions, key);
        }

        public static void ClearBindings(this Behaviour behaviour, object key)
        {
            behaviour.BindingContext().Clear(key);
        }

        public static void ClearAllBindings(this Behaviour behaviour)
        {
            behaviour.BindingContext().Clear();
        }
    }
}
#endif