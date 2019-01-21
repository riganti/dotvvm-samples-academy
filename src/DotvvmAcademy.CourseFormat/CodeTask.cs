using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.CourseFormat
{
    public class CodeTask : Source
    {
        public CodeTask(string path, IValidationUnit unit)
            : base(path)
        {
            Unit = unit;
        }

        public IValidationUnit Unit { get; }

        public override long GetSize()
        {
            return 1;
        }
    }
}