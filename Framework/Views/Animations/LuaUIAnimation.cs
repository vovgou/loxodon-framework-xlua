#if XLUA
using Loxodon.Framework.Views.Variables;
using System;
using UnityEngine;
using XLua;

namespace Loxodon.Framework.Views.Animations
{
    [CSharpCallLua]
    public delegate void LuaAnimationAction(LuaUIAnimation target, IUIView view, Action startCallback, Action endCallback);

    [LuaCallCSharp]
    public class LuaUIAnimation : UIAnimation, ILuaExtendable
    {
        public ScriptReference script;
        public VariableArray variables;

        protected LuaTable scriptEnv;
        protected LuaTable metatable;
        protected LuaAnimationAction play;
        protected IUIView view;

        public virtual LuaTable GetMetatable()
        {
            return this.metatable;
        }

        protected virtual void Initialize()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            string scriptText = (script.Type == ScriptReferenceType.TextAsset) ? script.Text.text : string.Format("require(\"framework.System\");local cls = require(\"{0}\");return extends(target,cls);", script.Filename);
            object[] result = luaEnv.DoString(scriptText, string.Format("{0}({1})", "LuaUIAnimation", this.name), scriptEnv);
            if (result.Length != 1 || !(result[0] is LuaTable))
                throw new Exception("");

            metatable = (LuaTable)result[0];
            if (variables != null && variables.Variables != null)
            {
                foreach (var variable in variables.Variables)
                {
                    var name = variable.Name.Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    metatable.Set(name, variable.GetValue());
                }
            }

            this.play = metatable.Get<LuaAnimationAction>("play");
        }

        protected virtual void Awake()
        {
            this.Initialize();
        }

        protected virtual void OnEnable()
        {
            this.view = this.GetComponent<IUIView>();
            switch (this.AnimationType)
            {
                case AnimationType.EnterAnimation:
                    this.view.EnterAnimation = this;
                    break;
                case AnimationType.ExitAnimation:
                    this.view.ExitAnimation = this;
                    break;
                case AnimationType.ActivationAnimation:
                    if (this.view is IWindowView)
                        (this.view as IWindowView).ActivationAnimation = this;
                    break;
                case AnimationType.PassivationAnimation:
                    if (this.view is IWindowView)
                        (this.view as IWindowView).PassivationAnimation = this;
                    break;
            }
        }

        public override IAnimation Play()
        {
            if (this.play != null)
                this.play(this, this.view, OnStart, OnEnd);
            return this;
        }

        protected virtual void OnDestroy()
        {
            if (metatable != null)
            {
                metatable.Dispose();
                metatable = null;
            }

            if (scriptEnv != null)
            {
                scriptEnv.Dispose();
                scriptEnv = null;
            }
        }
    }
}
#endif