#if XLUA
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Examples;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Localizations;
using Loxodon.Framework.Messaging;
using Loxodon.Framework.Observables;
using Loxodon.Framework.Prefs;
using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using XLua;

namespace Loxodon.Framework.Editors
{
    public static class LuaFrameworkGenConfig
    {
        [LuaCallCSharp]
        public static List<Type> lua_call_cs_list = new List<Type>()
        {
            typeof(Executors),
            typeof(Context),
            typeof(ApplicationContext),
            typeof(PlayerContext),

            typeof(Preferences),

            typeof(Localization),

            typeof(Messenger),

            typeof(SimpleCommand),
            typeof(AsyncResult),
            typeof(AsyncResult<IViewModel>),
            typeof(ObservableDictionary<object,object>),
            typeof(ObservableList<object>),
            typeof(NotifyCollectionChangedEventArgs),
            typeof(NotifyCollectionChangedEventHandler),

            //typeof(Transition),
            typeof(WindowContainer),
            typeof(ProgressTask<float, Window>),
            typeof(ProgressTask<float, IView>),
            typeof(IProgressTask<float, Window>),
            typeof(IProgressTask<float, IView>),
            typeof(IView),
            typeof(Window),
            typeof(WindowManager),
            typeof(IUIViewLocator),
            typeof(ResourcesViewLocator),
            typeof(Type)
        };

        [CSharpCallLua]
        public static List<Type> cs_call_lua_list = new List<Type>()
        {
            typeof(IEnumerator),
            typeof(Action),
            typeof(Action<LuaTable>),
            typeof(Action<MonoBehaviour>),
            typeof(Action<IWindow,WindowState>),
            typeof(Action<LuaWindow>),
            typeof(Action<LuaWindow,IBundle>),
            typeof(NotifyCollectionChangedEventHandler),
            typeof(Action<Account>),
            typeof(Action<float>),
            typeof(Action<int>),
            typeof(Action<string>),
            typeof(Action<object>),
            typeof(Action<Exception>),
            typeof(Action<Asynchronous.IAsyncResult>),
            typeof(EventHandler<WindowStateEventArgs>),
            typeof(EventHandler),
            typeof(Func<object>),
            typeof(IViewModel)
        };
    }
}
#endif