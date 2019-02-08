using DotVVM.Framework.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Dothtml
{
    internal class ValidationHeaderCollection : Dictionary<string, string[]>, IHeaderCollection
    {
        public new string this[string key]
        {
            get
            {
                Dictionary<string, string[]> dictionary = this;
                var result = dictionary[key];
                if (result.Length == 0)
                {
                    return null;
                }
                else if (result.Length == 1)
                {
                    return result[0];
                }
                else
                {
                    return string.Join(",", result);
                }
            }
            set
            {
                Dictionary<string, string[]> dictionary = this;
                dictionary[key] = new[] { value };
            }
        }

        public void Append(string key, string value)
        {
            ((Dictionary<string, string[]>)this)[key] = ((Dictionary<string, string[]>)this)[key].Concat(new[] { value })
                .ToArray();
        }
    }
}