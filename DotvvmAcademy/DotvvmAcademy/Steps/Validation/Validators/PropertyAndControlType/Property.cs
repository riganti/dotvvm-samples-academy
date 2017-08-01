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

        public string CsharpType { get; }

        public string Name { get; set; }

        public ControlBindName TargetControlBindName { get; set; }
    }
}