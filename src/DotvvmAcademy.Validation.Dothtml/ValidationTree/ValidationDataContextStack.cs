using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.ControlTree;

namespace DotvvmAcademy.Validation.Dothtml.ValidationTree
{
    [DebuggerDisplay("DataContextStack: {DataContextType.FullName,nq}")]
    public class ValidationDataContextStack : IDataContextStack
    {
        public ValidationDataContextStack(
            ValidationTypeDescriptor dataContextType,
            ValidationDataContextStack parent,
            ImmutableArray<NamespaceImport> namespaceImports,
            ImmutableArray<BindingExtensionParameter> extensionParameters)
        {
            DataContextType = dataContextType;
            Parent = parent;
            NamespaceImports = namespaceImports;
            ExtensionParameters = extensionParameters;
        }

        public ValidationTypeDescriptor DataContextType { get; }

        public ValidationDataContextStack Parent { get; }

        public ImmutableArray<NamespaceImport> NamespaceImports { get; }

        public ImmutableArray<BindingExtensionParameter> ExtensionParameters { get; }

        ITypeDescriptor IDataContextStack.DataContextType => DataContextType;

        IDataContextStack IDataContextStack.Parent => Parent;

        IReadOnlyList<NamespaceImport> IDataContextStack.NamespaceImports => NamespaceImports;

        IReadOnlyList<BindingExtensionParameter> IDataContextStack.ExtensionParameters => ExtensionParameters;

        public IEnumerable<(int dataContextLevel, BindingExtensionParameter parameter)> GetCurrentExtensionParameters()
        {
            var blackList = new HashSet<string>();
            var current = this;
            int level = 0;
            while (current != null)
            {
                if (!current.ExtensionParameters.IsDefaultOrEmpty)
                {
                    var relevantParameters = current.ExtensionParameters
                        .Where(p => blackList.Add(p.Identifier) && (current == this || p.Inherit));
                    foreach (var parameter in relevantParameters)
                    {
                        yield return (level, parameter);
                    }
                }

                current = current.Parent;
                level++;
            }
        }
    }
}