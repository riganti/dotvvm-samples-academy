using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewTreeResolver : DefaultControlTreeResolver
    {
        public EmbeddedViewTreeResolver(
            IControlResolver controlResolver,
            EmbeddedViewTreeBuilder treeBuilder,
            IBindingExpressionBuilder expressionBuilder)
            : base(controlResolver, treeBuilder, expressionBuilder)
        {
        }
    }
}