using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.CourseFormat
{
    public class ValidationScript : Source
    {
        public ValidationScript(string path, IUnit unit) : base(path)
        {
            Unit = unit;
        }

        public IUnit Unit { get; }

        public override long GetSize()
        {
            return 1;
        }
    }
}