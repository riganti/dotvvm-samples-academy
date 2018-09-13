namespace DotvvmAcademy.CourseFormat
{
    public class CodeTask
    {
        public CodeTask(string validationScriptPath)
        {
            ValidationScriptPath = validationScriptPath;
        }

        public string ValidationScriptPath { get; }
    }
}