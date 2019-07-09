#if XLUA
using Loxodon.Framework.Binding.Contexts;
using Loxodon.Framework.Binding.Converters;
using Loxodon.Framework.Binding.Proxy.Sources.Expressions;
using XLua;

namespace Loxodon.Framework.Binding.Builder
{
    [LuaCallCSharp]
    public class LuaBindingBuilder : BindingBuilderBase
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(LuaBindingBuilder));

        public LuaBindingBuilder(IBindingContext context, object target) : base(context, target)
        {
        }

        public LuaBindingBuilder For(string targetName, string updateTrigger = null)
        {
            this.description.TargetName = targetName;
            this.description.UpdateTrigger = updateTrigger;
            return this;
        }

        public LuaBindingBuilder To(string path)
        {
            this.SetMemberPath(path);
            return this;
        }

        public LuaBindingBuilder ToExpression(LuaFunction expression, params string[] paths)
        {
            if (this.description.Source != null)
                throw new BindingException("You cannot set the source path of a Fluent binding more than once");

            this.description.Source = new LuaExpressionSourceDescription()
            {
                Expression = expression,
                Paths = paths
            };

            return this;
        }

        public LuaBindingBuilder ToStatic(string path)
        {
            this.SetStaticMemberPath(path);
            return this;
        }

        public LuaBindingBuilder ToValue(object value)
        {
            this.SetLiteral(value);
            this.OneTime();
            return this;
        }

        public LuaBindingBuilder TwoWay()
        {
            this.SetMode(BindingMode.TwoWay);
            return this;
        }

        public LuaBindingBuilder OneWay()
        {
            this.SetMode(BindingMode.OneWay);
            return this;
        }

        public LuaBindingBuilder OneWayToSource()
        {
            this.SetMode(BindingMode.OneWayToSource);
            return this;
        }

        public LuaBindingBuilder OneTime()
        {
            this.SetMode(BindingMode.OneTime);
            return this;
        }

        public LuaBindingBuilder CommandParameter(object parameter)
        {
            this.SetCommandParameter(parameter);
            return this;
        }

        public LuaBindingBuilder WithConversion(string converterName)
        {
            var converter = this.ConverterByName(converterName);
            return this.WithConversion(converter);
        }

        public LuaBindingBuilder WithConversion(IConverter converter)
        {
            this.description.Converter = converter;
            return this;
        }

        public LuaBindingBuilder WithScopeKey(object scopeKey)
        {
            this.SetScopeKey(scopeKey);
            return this;
        }
    }
}
#endif