using DotvvmAcademy.Services;

namespace DotvvmAcademy.DTO
{
    public class LessonCsDTO : LessonDTO
    {
        public new string ButtonText
        {
            get
            {
                if (IsFinished)
                {
                    return "Opakovat Kurz";
                }
                if (IsVisited)
                {
                    return "Pokračovat";
                }
                if (!IsCreated)
                {
                    return "Již brzy";
                }
                return "Začít kurz";
            }
        }

    }
}