using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace DotvvmAcademy.Validation.CSharp
{
    public abstract class ValidationDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        public ValidationDiagnosticAnalyzer(MetadataCollection metadata)
        {
            Metadata = metadata;
        }

        public MetadataCollection Metadata { get; }

        public bool TryGetSymbol(Compilation compilation, MetadataName name, out ISymbol symbol)
        {
            var typeName = name.IsType ? name : name.Owner;
            var type = compilation.GetTypeByMetadataName(typeName.ReflectionName);
            if (name.IsType)
            {
                symbol = type;
                return true;
            }

            var members = type.GetMembers(name.Name);
            if (members.Length == 1)
            {
                symbol = members[0];
                return true;
            }
            else if (members.Length > 1)
            {
                var methods = members.CastArray<IMethodSymbol>();
                foreach (var method in methods)
                {
                    if (method.TypeParameters.Length != name.GenericParameters.Length)
                    {
                        continue;
                    }

                    if (method.Parameters.Length != name.Parameters.Length)
                    {
                        continue;
                    }

                    symbol = method;
                    return true;
                }
            }

            symbol = null;
            return false;
        }
    }
}