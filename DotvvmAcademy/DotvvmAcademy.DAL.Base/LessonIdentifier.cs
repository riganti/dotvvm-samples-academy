namespace DotvvmAcademy.DAL.Base
{
    public sealed class LessonIdentifier
    {
        public LessonIdentifier(int index, string language)
        {
            Index = index;
            Language = language;
        }

        public string Language { get; }

        public int Index { get; }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(LessonIdentifier))
            {
                return Equals((LessonIdentifier)obj);
            }

            return false;
        }

        public bool Equals(LessonIdentifier identifier)
        {
            return Language == identifier.Language && Index == identifier.Index;
        }

        public override int GetHashCode()
        {
            return Language.GetHashCode() ^ Index.GetHashCode();
        }
    }
}