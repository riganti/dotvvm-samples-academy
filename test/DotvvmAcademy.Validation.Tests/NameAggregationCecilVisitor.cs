using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.Validation.Tests
{
    public class NameAggregationCecilVisitor : CecilVisitor
    {
        public List<string> FullNames { get; set; } = new List<string>();

        public override void DefaultVisit(IMemberDefinition definition)
        {
            FullNames.Add(definition.FullName);
        }

        public override void VisitAssembly(AssemblyDefinition definition)
        {
            FullNames.Add(definition.FullName);
            foreach (var type in definition.MainModule.Types)
            {
                VisitType(type);
            }
        }

        public override void VisitType(TypeDefinition definition)
        {
            base.VisitType(definition);
            foreach (var nestedType in definition.NestedTypes)
            {
                VisitType(nestedType);
            }

            var nestedTypes = (IEnumerable<IMemberDefinition>)definition.NestedTypes;
            var events = (IEnumerable<IMemberDefinition>)definition.Events;
            var methods = (IEnumerable<IMemberDefinition>)definition.Methods;
            var properties = (IEnumerable<IMemberDefinition>)definition.Properties;
            var fields = (IEnumerable<IMemberDefinition>)definition.Fields;
            var members = nestedTypes.Concat(events)
                            .Concat(methods)
                            .Concat(properties)
                            .Concat(fields);
            foreach (var member in members)
            {
                Visit(member);
            }
        }
    }
}