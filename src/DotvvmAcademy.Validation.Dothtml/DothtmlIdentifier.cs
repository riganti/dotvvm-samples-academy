using System.Text;

namespace DotvvmAcademy.Validation.Dothtml
{
    public sealed class DothtmlIdentifier
    {
        public DothtmlIdentifier(string controlType, int index, DothtmlIdentifier parent)
        {
            ControlType = controlType;
            Index = index;
            Parent = parent;
        }

        public string ControlType { get; }

        public int Index { get; }

        public DothtmlIdentifier Parent { get; }

        public static bool operator !=(DothtmlIdentifier one, DothtmlIdentifier two)
        {
            return one.ToString() != two.ToString();
        }

        public static bool operator ==(DothtmlIdentifier one, DothtmlIdentifier two)
        {
            return one.ToString() == two.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is DothtmlIdentifier identifier)
            {
                return this == identifier;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Parent != null)
            {
                sb.Append(Parent);
            }
            sb.Append('/').Append(ControlType);
            sb.Append('[').Append(Index).Append(']');
            return sb.ToString();
        }
    }
}