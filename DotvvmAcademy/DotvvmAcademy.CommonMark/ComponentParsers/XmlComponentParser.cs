using DotvvmAcademy.CommonMark.Components;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DotvvmAcademy.CommonMark.ComponentParsers
{
    public class XmlComponentParser<TComponent> : ComponentParser
        where TComponent : ICommonMarkComponent
    {
        public XmlComponentParser(ComponentParser next) : base(next)
        {
        }

        public override ICommonMarkComponent Parse(string placeholder)
        {
            var serializer = new XmlSerializer(typeof(TComponent));

            var reader = XmlReader.Create(new StringReader(placeholder));
            using (reader)
            {
                if (serializer.CanDeserialize(reader))
                {
                    return (TComponent)serializer.Deserialize(reader);
                }
                else
                {
                    return Next.Parse(placeholder);
                }
            }
        }
    }
}