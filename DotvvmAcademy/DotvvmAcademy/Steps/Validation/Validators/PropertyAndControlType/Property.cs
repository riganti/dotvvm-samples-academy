namespace DotvvmAcademy.Steps.Validation.Validators.PropertyAndControlType
{
    public class Property
    {
        public Property(string name, string csharpType, ControlBindName targetControlBindName)
        {
            Name = name;
            CsharpType = csharpType;
            TargetControlBindName = targetControlBindName;
        }
        public ControlBindName TargetControlBindName { get; }
        public string Name { get; set; }
        public string CsharpType { get; }
    }
}