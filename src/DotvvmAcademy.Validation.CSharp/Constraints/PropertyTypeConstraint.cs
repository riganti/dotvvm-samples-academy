﻿using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class PropertyTypeConstraint
    {
        public PropertyTypeConstraint(NameNode node, NameNode type)
        {
            Node = node;
            Type = type;
        }

        public NameNode Node { get; }

        public NameNode Type { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var type = converter.ToRoslyn(Type)
                .OfType<ITypeSymbol>()
                .SingleOrDefault();
            if (type == null)
            {
                return;
            }

            var symbols = converter.ToRoslyn(Node)
                .OfType<IPropertySymbol>();
            foreach (var property in symbols)
            {
                if (!SymbolEqualityComparer.Default.Equals(property.Type, type))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongPropertyType,
                        arguments: new object[] { property, type },
                        symbol: property);
                }
            }
        }
    }
}
