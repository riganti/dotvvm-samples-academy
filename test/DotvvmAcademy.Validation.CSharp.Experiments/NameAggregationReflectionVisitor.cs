using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public class NameAggregationReflectionVisitor : ReflectionVisitor
    {
        public List<string> FullNames { get; set; } = new List<string>();

        public override void VisitAssembly(Assembly assembly)
        {
            FullNames.Add(assembly.FullName);
            foreach (var type in assembly.GetTypes())
            {
                Visit(type);
            }
        }

        public override void VisitEvent(EventInfo member)
        {
            FullNames.Add(member.Name);
            if (member.AddMethod != null)
            {
                Visit(member.AddMethod);
            }

            if (member.RemoveMethod != null)
            {
                Visit(member.RemoveMethod);
            }
        }

        public override void VisitField(FieldInfo member)
        {
            FullNames.Add(member.Name);
        }

        public override void VisitMethod(MethodBase member)
        {
            FullNames.Add(member.Name);
        }

        public override void VisitProperty(PropertyInfo member)
        {
            FullNames.Add(member.Name);
            if (member.GetMethod != null)
            {
                Visit(member.GetMethod);
            }

            if (member.SetMethod != null)
            {
                Visit(member.SetMethod);
            }
        }

        public override void VisitType(Type member)
        {
            FullNames.Add(member.FullName);
            foreach (var descendant in member.GetMembers(BindingFlags.DeclaredOnly
                | BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Static))
            {
                Visit(descendant);
            }
        }
    }
}