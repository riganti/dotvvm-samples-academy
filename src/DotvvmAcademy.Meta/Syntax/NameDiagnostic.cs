namespace DotvvmAcademy.Meta.Syntax
{
    public class NameDiagnostic
    {
        public NameDiagnostic(NameNode node, string message)
        {
            Node = node;
            Message = message;
        }

        public string Message { get; }

        public NameNode Node { get; set; }
    }
}