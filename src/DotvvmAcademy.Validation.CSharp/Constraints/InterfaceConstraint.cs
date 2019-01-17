using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class InterfaceConstraint
    {
        public InterfaceConstraint(NameNode node, NameNode @interface)
        {
            Node = node;
            Interface = @interface;
        }

        public NameNode Interface { get; }

        public NameNode Node { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var interfaceSymbol = converter.ToRoslyn(Interface)
                .OfType<ITypeSymbol>()
                .Single();
            var symbols = converter.ToRoslyn(Node)
                .OfType<ITypeSymbol>();
            foreach (var typeSymbol in symbols)
            {
                if (!typeSymbol.AllInterfaces.Contains(interfaceSymbol))
                {
                    reporter.Report(
                        message: Resources.ERR_MissingInterfaceImplementation,
                        arguments: new object[] { typeSymbol, interfaceSymbol },
                        symbol: typeSymbol);
                }
            }
        }
    }
}