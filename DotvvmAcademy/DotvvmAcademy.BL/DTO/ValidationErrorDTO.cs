namespace DotvvmAcademy.BL.DTO
{
    public class ValidationErrorDTO
    {
        internal ValidationErrorDTO()
        {
        }

        public int EndPosition { get; internal set; }

        public bool IsGlobal { get; internal set; }

        public string Message { get; internal set; }

        public int StartPosition { get; internal set; }
    }
}