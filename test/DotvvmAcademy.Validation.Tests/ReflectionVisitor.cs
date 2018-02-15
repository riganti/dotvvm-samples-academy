using System;
using System.Reflection;

namespace DotvvmAcademy.Validation.Tests
{
    public abstract class ReflectionVisitor
    {
        public virtual void DefaultVisit(MemberInfo member)
        {
        }

        public virtual void Visit(MemberInfo member)
        {
            switch (member)
            {
                case Type type:
                    VisitType(type);
                    break;

                case EventInfo eventInfo:
                    VisitEvent(eventInfo);
                    break;

                case FieldInfo fieldInfo:
                    VisitField(fieldInfo);
                    break;

                case MethodBase methodBase:
                    VisitMethod(methodBase);
                    break;

                case PropertyInfo propertyInfo:
                    VisitProperty(propertyInfo);
                    break;

                default:
                    throw new ArgumentException($"{nameof(ReflectionVisitor)} doesn't support member type {member.GetType().Name}", nameof(member));
            }
        }

        public virtual void Visit(Assembly assembly)
        {
            VisitAssembly(assembly);
        }

        public virtual void VisitAssembly(Assembly assembly)
        {

        }

        public virtual void VisitEvent(EventInfo member)
        {
            DefaultVisit(member);
        }

        public virtual void VisitField(FieldInfo member)
        {
            DefaultVisit(member);
        }

        public virtual void VisitMethod(MethodBase member)
        {
            DefaultVisit(member);
        }

        public virtual void VisitProperty(PropertyInfo member)
        {
            DefaultVisit(member);
        }

        public virtual void VisitType(Type member)
        {
            DefaultVisit(member);
        }
    }
}