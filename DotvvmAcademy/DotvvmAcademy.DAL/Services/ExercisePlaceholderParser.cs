using DotvvmAcademy.CommonMark;
using DotvvmAcademy.CommonMark.Parsers;
using DotvvmAcademy.CommonMark.Segments;
using DotvvmAcademy.DAL.Loadees;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotvvmAcademy.DAL.Services
{
    public class ExercisePlaceholderParser : IPlaceholderParser
    {
        private const string Suffix = "Segment";
        private readonly PathConverter converter;
        private readonly List<Type> exerciseTypes;
        private readonly ExerciseNamingStrategy namingStrategy;

        public ExercisePlaceholderParser(PathConverter converter, ExerciseNamingStrategy namingStrategy)
        {
            this.converter = converter;
            this.namingStrategy = namingStrategy;
            this.exerciseTypes = GetExerciseTypes().ToList();
        }

        public Task<bool> Parse(IPlaceholderParsingContext context)
        {
            try
            {
                var element = XElement.Parse(context.Placeholder);
                var type = GetExerciseType(element.Name.LocalName);
                if (type == null)
                {
                    return Task.FromResult(false);
                }

                var json = JsonConvert.SerializeXNode(element, Formatting.None, true);
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(converter);
                settings.ContractResolver = new DefaultContractResolver { NamingStrategy = namingStrategy };
                var exercise = JsonConvert.DeserializeObject(json, type, settings);
                context.AddSegment((ISegment)exercise);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        private Type GetExerciseType(string elementName)
        {
            return GetKinds().SingleOrDefault(tuple => tuple.Kind == elementName).Type;
        }

        private IEnumerable<Type> GetExerciseTypes()
        {
            return typeof(ExercisePlaceholderParser).Assembly.GetTypes().Where(t => typeof(IExerciseSegment).IsAssignableFrom(t));
        }

        private IEnumerable<(string Kind, Type Type)> GetKinds()
        {
            foreach (var type in exerciseTypes)
            {
                var typeName = type.Name;
                if (typeName.EndsWith(Suffix))
                {
                    typeName = typeName.Substring(0, typeName.Length - Suffix.Length);
                }

                yield return (typeName, type);
            }
        }
    }
}