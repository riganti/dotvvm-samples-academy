namespace DotvvmAcademy.BL.Validation
{
    public class ValidationError
    {
        public ValidationError()
        {
        }

        public ValidationError(string message, int startPosition, int endPosition)
        {
            Message = message;
            EndPosition = endPosition;
            StartPosition = startPosition;
        }

        public int EndPosition { get; }

        public string Message { get; }

        public int StartPosition { get; }
    }
}