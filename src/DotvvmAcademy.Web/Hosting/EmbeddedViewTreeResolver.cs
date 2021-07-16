using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.ResourceManagement;

namespace DotvvmAcademy.Web.Hosting
{
    public class EmbeddedViewTreeResolver : DefaultControlTreeResolver
    {
        public EmbeddedViewTreeResolver(
            IControlResolver controlResolver,
            IControlBuilderFactory controlBuilderFactory,
            EmbeddedViewTreeBuilder treeBuilder,
            DotvvmResourceRepository resourceRepo)
            : base(controlResolver, controlBuilderFactory, treeBuilder, resourceRepo)
        {
        }
    }
}
