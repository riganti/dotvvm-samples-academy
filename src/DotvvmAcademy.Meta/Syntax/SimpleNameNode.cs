namespace DotvvmAcademy.Meta.Syntax
{
    public abstract class SimpleNameNode : NameNode
    {
        public SimpleNameNode(NameToken identifierToken)
        {
            IdentifierToken = identifierToken;
        }

        public NameToken IdentifierToken { get; }
    }
}