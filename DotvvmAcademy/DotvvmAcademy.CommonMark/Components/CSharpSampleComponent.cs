using System.Collections.Generic;

namespace DotvvmAcademy.CommonMark.Components
{
    public class CSharpSampleComponent : BasicSampleBase, IComponent
    {
        public IEnumerable<string> DependencyPaths { get; set; }
    }
}