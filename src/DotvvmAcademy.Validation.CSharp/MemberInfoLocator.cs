using System;
using System.Linq;
using System.Reflection;

namespace DotvvmAcademy.Validation.CSharp
{
    public class MemberInfoLocator : ILocator<MemberInfo>
    {
        private readonly Assembly assembly;
        private readonly ReflectionMetadataNameProvider nameProvider;

        public MemberInfoLocator(Assembly assembly, ReflectionMetadataNameProvider nameProvider)
        {
            this.assembly = assembly;
            this.nameProvider = nameProvider;
        }

        public bool TryLocate(MetadataName name, out MemberInfo metadataSource)
        {
            metadataSource = name.Kind.HasFlag(MetadataNameKind.Type)
                ? GetTypeMember(name)
                : GetOtherMember(name);
            return metadataSource == null;
        }

        private MemberInfo GetOtherMember(MetadataName name)
        {
            var declaringType = GetTypeMember(name.Owner);
            var flags = BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.DeclaredOnly;
            return declaringType.GetMember(name.Name, flags)
                .SingleOrDefault(m => name.Equals(nameProvider.GetName(m)));
        }

        private Type GetTypeMember(MetadataName name)
        {
            return assembly.GetType(
                name: name.ReflectionName,
                throwOnError: false);
        }
    }
}