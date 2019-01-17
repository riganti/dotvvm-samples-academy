using DotvvmAcademy.Meta;
using DotvvmAcademy.Meta.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace DotvvmAcademy.Validation.CSharp.Constraints
{
    internal class TypeKindConstraint
    {
        public TypeKindConstraint(NameNode node, AllowedTypeKind allowedTypeKind)
        {
            Node = node;
            AllowedTypeKind = allowedTypeKind;
        }

        public AllowedTypeKind AllowedTypeKind { get; }

        public NameNode Node { get; }

        public void Validate(IValidationReporter reporter, MetaConverter converter)
        {
            var symbols = converter.ToRoslyn(Node)
                .OfType<ITypeSymbol>();
            foreach (var symbol in symbols)
            {
                if (!AllowedTypeKind.HasFlag(GetAllowedTypeKind(symbol.TypeKind)))
                {
                    reporter.Report(
                        message: Resources.ERR_WrongTypeKind,
                        arguments: new object[] { symbol, AllowedTypeKind },
                        symbol: symbol);
                }
            }
        }

        private AllowedTypeKind GetAllowedTypeKind(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return AllowedTypeKind.Class;

                case TypeKind.Delegate:
                    return AllowedTypeKind.Delegate;

                case TypeKind.Enum:
                    return AllowedTypeKind.Enum;

                case TypeKind.Interface:
                    return AllowedTypeKind.Interface;

                case TypeKind.Struct:
                    return AllowedTypeKind.Struct;

                default:
                    return AllowedTypeKind.None;
            }
        }
    }
}