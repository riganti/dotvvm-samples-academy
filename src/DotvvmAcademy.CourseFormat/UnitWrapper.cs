using DotvvmAcademy.Validation.Unit;

namespace DotvvmAcademy.CourseFormat
{
    public class UnitWrapper<TUnit>
        where TUnit : IValidationUnit
    {
        public UnitWrapper(TUnit unit)
        {
            Unit = unit;
        }

        public TUnit Unit { get; }
    }
}