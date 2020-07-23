namespace DotvvmAcademy.Meta.Syntax
{
    public static class NameNodeExtensions
    {
        public static NameNode GetLogicalParent(this NameNode node)
        {
            return node switch
            {
                ArrayTypeNameNode array => array.ElementType.GetLogicalParent(),
                ConstructedTypeNameNode constructed => constructed.UnboundTypeName.GetLogicalParent(),
                SimpleNameNode _ => NameFactory.Simple(""),
                MemberNameNode member => member.Type,
                NestedTypeNameNode nested => nested.Left,
                PointerTypeNameNode pointer => pointer.ElementType.GetLogicalParent(),
                QualifiedNameNode qualified => qualified.Left,
                _ => NameFactory.Simple(""),
            };
        }

        public static string GetShortName(this NameNode node)
        {
            return node switch
            {
                ArrayTypeNameNode array => array.ElementType.GetShortName(),
                ConstructedTypeNameNode constructed => constructed.UnboundTypeName.GetShortName(),
                SimpleNameNode simple => simple.IdentifierToken.ToString(),
                MemberNameNode member => member.Member.IdentifierToken.ToString(),
                NestedTypeNameNode nested => nested.Right.IdentifierToken.ToString(),
                PointerTypeNameNode pointer => pointer.ElementType.GetShortName(),
                QualifiedNameNode qualified => qualified.Right.IdentifierToken.ToString(),
                _ => node.ToString(),
            };
        }
    }
}
