using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotvvmAcademy.Meta
{
    internal class MemberInfoVisitor<TResult>
    {
        public virtual TResult Visit(MemberInfo info)
        {
            return info switch
            {
                EventInfo eventInfo => VisitEvent(eventInfo),
                FieldInfo fieldInfo => VisitField(fieldInfo),
                PropertyInfo propertyInfo => VisitProperty(propertyInfo),
                ConstructorInfo constructorInfo => VisitConstructor(constructorInfo),
                MethodInfo methodInfo => VisitMethod(methodInfo),
                Type type => VisitType(type),
                _ => throw new InvalidOperationException($"\"{info.GetType()}\" is not a supported MemberInfo."),
            };
        }

        public virtual TResult DefaultVisit(MemberInfo info)
        {
            return default;
        }

        public virtual TResult VisitEvent(EventInfo info)
        {
            return DefaultVisit(info);
        }
        
        public virtual TResult VisitField(FieldInfo info)
        {
            return DefaultVisit(info);
        }

        public virtual TResult VisitProperty(PropertyInfo info)
        {
            return DefaultVisit(info);
        }

        public virtual TResult VisitConstructor(ConstructorInfo info)
        {
            return DefaultVisit(info);
        }

        public virtual TResult VisitMethod(MethodInfo info)
        {
            return DefaultVisit(info);
        }

        public virtual TResult VisitType(Type info)
        {
            return DefaultVisit(info);
        }
    }
}
