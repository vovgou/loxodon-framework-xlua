#if XLUA
using XLua;

namespace Loxodon.Framework.Binding.Proxy.Sources.Expressions
{
    public class LuaExpressionSourceDescription : SourceDescription
    {
        private LuaFunction expression;

        private string[] paths;

        public LuaExpressionSourceDescription() : this(false)
        {
        }

        public LuaExpressionSourceDescription(bool isStatic)
        {
            this.IsStatic = isStatic;
        }

        public LuaFunction Expression
        {
            get { return this.expression; }
            set { this.expression = value; }
        }

        public string[] Paths
        {
            get { return this.paths; }
            set { this.paths = value; }
        }

        public override string ToString()
        {
            return this.expression == null ? "Expression:null" : "Expression:" + this.expression.ToString();
        }
    }
}
#endif