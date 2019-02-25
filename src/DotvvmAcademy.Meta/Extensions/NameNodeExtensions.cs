namespace DotvvmAcademy.Meta.Syntax
{
    public static class NameNodeExtensions
    {
        public static NameNode GetLogicalParent(this NameNode node)
        {
            switch (node)
            {
                case ArrayTypeNameNode array:
                    return array.ElementType.GetLogicalParent();

                case ConstructedTypeNameNode constructed:
                    return constructed.UnboundTypeName.GetLogicalParent();

                case SimpleNameNode simple:
                    return NameFactory.Simple("");

                case MemberNameNode member:
                    return member.Type;

                case NestedTypeNameNode nested:
                    return nested.Left;

                case PointerTypeNameNode pointer:
                    return pointer.ElementType.GetLogicalParent();

                case QualifiedNameNode qualified:
                    return qualified.Left;

                default:
                    return NameFactory.Simple("");
            }
        }

        public static string GetShortName(this NameNode node)
        {
            switch (node)
            {
                case ArrayTypeNameNode array:
                    return array.ElementType.GetShortName();

                case ConstructedTypeNameNode constructed:
                    return constructed.UnboundTypeName.GetShortName();

                case SimpleNameNode simple:
                    return simple.IdentifierToken.ToString();

                case MemberNameNode member:
                    return member.Member.IdentifierToken.ToString();

                case NestedTypeNameNode nested:
                    return nested.Right.IdentifierToken.ToString();

                case PointerTypeNameNode pointer:
                    return pointer.ElementType.GetShortName();

                case QualifiedNameNode qualified:
                    return qualified.Right.IdentifierToken.ToString();

                default:
                    return node.ToString();
            }
        }
    }
}