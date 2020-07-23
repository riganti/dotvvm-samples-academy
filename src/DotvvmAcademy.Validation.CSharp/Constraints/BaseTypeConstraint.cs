﻿using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class BaseTypeConstraint
    {
        public BaseTypeConstraint(NameNode node, NameNode baseType)
        {
            Node = node;
            BaseType = baseType;
        }

        public NameNode BaseType { get; }

        public NameNode Node { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var baseType = converter.ToRoslyn(BaseType)
                .OfType<ITypeSymbol>()
                .SingleOrDefault();
            if (baseType == null)
            {
                return;
            }

            var symbols = converter.ToRoslyn(Node)
                .OfType<ITypeSymbol>();
            foreach (var symbol in symbols)
            {
                if (!SymbolEqualityComparer.Default.Equals(symbol.BaseType, baseType))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongBaseType,
                        arguments: new object[] { symbol, baseType },
                        symbol: symbol);
                }
            }
        }
    }
}
