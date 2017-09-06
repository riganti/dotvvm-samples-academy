namespace DotvvmAcademy.BL.Dtos
{
    public class ValidationErrorDto
    {
        public int EndPosition { get; internal set; }

        public bool IsGlobal { get; internal set; }

        public string Message { get; internal set; }

        public int StartPosition { get; internal set; }
    }
}