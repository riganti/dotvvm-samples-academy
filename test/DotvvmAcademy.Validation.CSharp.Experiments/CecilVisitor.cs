using Mono.Cecil;
using System;

namespace DotvvmAcademy.Validation.CSharp.Experiments
{
    public abstract class CecilVisitor
    {
        public virtual void DefaultVisit(IMemberDefinition definition)
        {
        }

        public virtual void Visit(IMemberDefinition definition)
        {
            switch (definition)
            {
                case EventDefinition eventDefinition:
                    VisitEvent(eventDefinition);
                    break;

                case FieldDefinition fieldDefinition:
                    VisitField(fieldDefinition);
                    break;

                case MethodDefinition methodDefinition:
                    VisitMethod(methodDefinition);
                    break;

                case PropertyDefinition propertyDefinition:
                    VisitProperty(propertyDefinition);
                    break;

                case TypeDefinition typeDefinition:
                    VisitType(typeDefinition);
                    break;

                default:
                    throw new ArgumentException($"{nameof(CecilVisitor)} doesn't support member type {definition.GetType().Name}", nameof(definition));
            }
        }

        public virtual void Visit(AssemblyDefinition definition)
        {
            VisitAssembly(definition);
        }

        public virtual void VisitAssembly(AssemblyDefinition definition)
        {
        }

        public virtual void VisitEvent(EventDefinition definition)
        {
            DefaultVisit(definition);
        }

        public virtual void VisitField(FieldDefinition definition)
        {
            DefaultVisit(definition);
        }

        public virtual void VisitMethod(MethodDefinition definition)
        {
            DefaultVisit(definition);
        }

        public virtual void VisitProperty(PropertyDefinition definition)
        {
            DefaultVisit(definition);
        }

        public virtual void VisitType(TypeDefinition definition)
        {
            DefaultVisit(definition);
        }
    }
}