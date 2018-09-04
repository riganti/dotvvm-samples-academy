namespace DotvvmAcademy.Meta.Syntax
{
    public static class NameNodeExtensions
    {
        public static NameNode GetLogicalParent(this NameNode node)
        {
            switch (node)
            {
                case ArrayTypeNameNode array:
                    return array.ElementType;

                case ConstructedTypeNameNode constructed:
                    return constructed.UnboundTypeName;

                case SimpleNameNode generic:
                    return null;

                case MemberNameNode member:
                    return member.Type;

                case NestedTypeNameNode nested:
                    return nested.Left;

                case PointerTypeNameNode pointer:
                    return pointer.ElementType;

                case QualifiedNameNode qualified:
                    return qualified.Left;

                default:
                    return null;
            }
        }
    }
}