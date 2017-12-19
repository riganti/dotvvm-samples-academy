using DotvvmAcademy.BL.Dtos;

namespace DotvvmAcademy.Controls
{
    public static class MonacoLanguageExtensions
    {
        public static MonacoLanguage FromDto(this CodeLanguageDto dto)
        {
            switch (dto)
            {
                case CodeLanguageDto.CSharp:
                    return MonacoLanguage.CSharp;

                case CodeLanguageDto.Dothtml:
                    return MonacoLanguage.Html;

                default:
                    return MonacoLanguage.PlainText;
            }
        }
    }
}