using DotvvmAcademy.Web.Resources.Localization;

namespace DotvvmAcademy.Web.Pages
{
    public struct LanguageOption
    {
        public string Moniker { get; set; }

        public string Name { get; set; }

        public static LanguageOption Create(string moniker)
        {
            var name = UIResources.ResourceManager.GetString($"Base_Language_{moniker}");
            return new LanguageOption { Moniker = moniker, Name = name };
        }
    }
}