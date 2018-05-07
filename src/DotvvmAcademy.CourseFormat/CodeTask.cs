namespace DotvvmAcademy.CourseFormat
{
    internal class CodeTask : ICodeTask
    {
        public CodeTask(CodeTaskId id)
        {
            Id = id;
        }

        public string Code { get; set; }

        public CodeTaskId Id { get; }

        public string ValidationScript { get; set; }
    }
}