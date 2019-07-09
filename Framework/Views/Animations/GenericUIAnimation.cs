#if XLUA
using System;
using XLua;

namespace Loxodon.Framework.Views.Animations
{
    [CSharpCallLua]
    public delegate void AnimationAction(IUIView view, Action startCallback, Action endCallback);

    [LuaCallCSharp]
    public class GenericUIAnimation : IAnimation
    {
        protected IUIView view;
        protected AnimationAction animation;

        protected Action _onStart;
        protected Action _onEnd;

        public GenericUIAnimation(IUIView view, AnimationAction animation)
        {
            this.view = view;
            this.animation = animation;
        }

        protected virtual void OnStart()
        {
            try
            {
                if (this._onStart != null)
                {
                    this._onStart();
                    this._onStart = null;
                }
            }
            catch (Exception) { }
        }

        protected virtual void OnEnd()
        {
            try
            {
                if (this._onEnd != null)
                {
                    this._onEnd();
                    this._onEnd = null;
                }
            }
            catch (Exception) { }
        }

        public IAnimation OnStart(Action onStart)
        {
            this._onStart += onStart;
            return this;
        }

        public IAnimation OnEnd(Action onEnd)
        {
            this._onEnd += onEnd;
            return this;
        }

        public virtual IAnimation Play()
        {
            if (this.animation != null)
                this.animation(this.view, OnStart, OnEnd);
            return this;
        }
    }
}
#endif