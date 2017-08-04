namespace DotvvmAcademy.DAL.Base
{
    public sealed class StepIdentifier
    {
        public StepIdentifier(int lessonIndex, string language, int index)
        {
            LessonIndex = lessonIndex;
            Language = language;
            Index = index;
        }

        public int Index { get; set; }

        public string Language { get; }

        public int LessonIndex { get; }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(StepIdentifier))
            {
                return Equals((StepIdentifier)obj);
            }

            return false;
        }

        public bool Equals(StepIdentifier identifier)
        {
            return Language == identifier.Language
                && LessonIndex == identifier.LessonIndex
                && Index == identifier.Index;
        }

        public override int GetHashCode()
        {
            return Language.GetHashCode() ^ LessonIndex.GetHashCode() ^ Index.GetHashCode();
        }
    }
}