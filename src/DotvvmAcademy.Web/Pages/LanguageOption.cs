using System;

namespace DotvvmAcademy.Web.Pages
{
    public class LanguageOption
    {
        public LanguageOption(string moniker, string name)
        {
            Moniker = moniker ?? throw new ArgumentNullException(nameof(moniker));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Moniker { get; }

        public string Name { get; }
    }
}