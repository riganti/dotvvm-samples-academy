using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Web
{
    public class EVControlTreeResolver : DefaultControlTreeResolver
    {
        public EVControlTreeResolver(
            IControlResolver controlResolver,
            EVResolvedTreeBuilder treeBuilder,
            IBindingExpressionBuilder expressionBuilder)
            : base(controlResolver, treeBuilder, expressionBuilder)
        {
        }
    }
}