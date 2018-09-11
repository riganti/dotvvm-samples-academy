using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationScript : Source
    {
        public ValidationScript(string path, IValidationUnit unit) : base(path)
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