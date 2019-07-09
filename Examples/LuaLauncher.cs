#if XLUA
using System;
using UnityEngine;
using System.Globalization;
using XLua;

using Loxodon.Log;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Views;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Localizations;
using Loxodon.Framework.Services;

namespace Loxodon.Framework.Examples
{
    public class LuaLauncher : MonoBehaviour
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(LuaLauncher));

        public ScriptReference script;

        private LuaTable scriptEnv;
        private LuaTable metatable;
        private Action<MonoBehaviour> onAwake;
        private Action<MonoBehaviour> onEnable;
        private Action<MonoBehaviour> onDisable;
        private Action<MonoBehaviour> onStart;
        private Action<MonoBehaviour> onDestroy;

        private ApplicationContext context;
        void Awake()
        {
            GlobalWindowManager windowManager = FindObjectOfType<GlobalWindowManager>();
            if (windowManager == null)
                throw new NotFoundException("Can't find the GlobalWindowManager.");

            context = Context.GetApplicationContext();

            IServiceContainer container = context.GetContainer();

            /* Initialize the data binding service */
            LuaBindingServiceBundle bundle = new LuaBindingServiceBundle(context.GetContainer());
            bundle.Start();

            /* Initialize the ui view locator and register UIViewLocator */
            container.Register<IUIViewLocator>(new ResourcesViewLocator());

            /* Initialize the localization service */
            //		CultureInfo cultureInfo = Locale.GetCultureInfoByLanguage (SystemLanguage.English);
            CultureInfo cultureInfo = Locale.GetCultureInfo();
            Localization.Current = Localization.Create(new ResourcesDataProvider("LocalizationExamples", new XmlDocumentParser()), cultureInfo);

            /* register Localization */
            container.Register<Localization>(Localization.Current);

            /* register AccountRepository */
            IAccountRepository accountRepository = new AccountRepository();
            container.Register<IAccountService>(new AccountService(accountRepository));

            InitLuaEnv();

            if (onAwake != null)
                onAwake(this);

        }

        void InitLuaEnv()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("target", this);

            string scriptText = (script.Type == ScriptReferenceType.TextAsset) ? script.Text.text : string.Format("require(\"framework.System\");local cls = require(\"{0}\");return extends(target,cls);", script.Filename);
            object[] result = luaEnv.DoString(scriptText, string.Format("{0}({1})", "Launcher", this.name), scriptEnv);

            if (result.Length != 1 || !(result[0] is LuaTable))
                throw new Exception();

            metatable = (LuaTable)result[0];

            onAwake = metatable.Get<Action<MonoBehaviour>>("awake");
            onEnable = metatable.Get<Action<MonoBehaviour>>("enable");
            onDisable = metatable.Get<Action<MonoBehaviour>>("disable");
            onStart = metatable.Get<Action<MonoBehaviour>>("start");
            onDestroy = metatable.Get<Action<MonoBehaviour>>("destroy");
        }

        void OnEnable()
        {
            if (onEnable != null)
                onEnable(this);
        }

        void OnDisable()
        {
            if (onDisable != null)
                onDisable(this);
        }

        void Start()
        {
            if (onStart != null)
                onStart(this);
        }

        void OnDestroy()
        {
            if (onDestroy != null)
                onDestroy(this);

            metatable = null;
            onDestroy = null;
            onStart = null;
            onEnable = null;
            onDisable = null;
            onAwake = null;
            if (scriptEnv != null)
            {
                scriptEnv.Dispose();
                scriptEnv = null;
            }
        }
    }
}
#endif