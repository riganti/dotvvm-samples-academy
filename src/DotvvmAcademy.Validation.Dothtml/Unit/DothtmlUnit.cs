using DotvvmAcademy.Validation.Dothtml.ValidationTree;
using DotvvmAcademy.Validation.Unit;
using System.Collections.Concurrent;

namespace DotvvmAcademy.Validation.Dothtml.Unit
{
    public class DothtmlUnit : IUnit
    {
        public string CorrectCode { get; set; }

        public string DefaultCode { get; set; }

        public ConcurrentDictionary<string, IQuery> Queries { get; }
            = new ConcurrentDictionary<string, IQuery>();

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