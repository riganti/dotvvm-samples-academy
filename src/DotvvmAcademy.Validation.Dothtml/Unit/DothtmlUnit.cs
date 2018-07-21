using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlUnit : IUnit
    {
        public DothtmlUnit(IServiceProvider provider)
        {
            Provider = provider;
        }

        public ConcurrentDictionary<string, IQuery> Queries { get; }
            = new ConcurrentDictionary<string, IQuery>();

        public IServiceProvider Provider { get; }

        public DothtmlQuery<ValidationControl> GetControls(string xpath)
            => AddQuery<ValidationControl>(xpath);

        public DothtmlQuery<ValidationDirective> GetDirectives(string xpath)
            => AddQuery<ValidationDirective>(xpath);

        public DothtmlQuery<ValidationPropertySetter> GetProperties(string xpath)
            => AddQuery<ValidationPropertySetter>(xpath);

        private DothtmlQuery<TNode> AddQuery<TNode>(string xpath)
            where TNode : ValidationTreeNode
        {
            return (DothtmlQuery<TNode>)Queries.GetOrAdd(xpath, n =>
                new DothtmlQuery<TNode>(xpath));
        }
    }
}