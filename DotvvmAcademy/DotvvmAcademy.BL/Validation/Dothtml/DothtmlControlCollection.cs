using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlControlCollection : ValidationObject<DothtmlValidate>, IEnumerable<DothtmlControl>
    {
        internal DothtmlControlCollection(DothtmlValidate validate, IEnumerable<DothtmlControl> controls, bool isActive = true) : base(validate, isActive)
        {
            Controls = controls ?? Enumerable.Empty<DothtmlControl>();
        }

        public static DothtmlControlCollection Inactive => new DothtmlControlCollection(null, Enumerable.Empty<DothtmlControl>(), false);

        public IEnumerable<DothtmlControl> Controls { get; internal set; }

        public DothtmlControl this[int i]
        {
            get
            {
                return IsActive ? Controls.ElementAt(i) : DothtmlControl.Inactive;
            }
        }

        public IEnumerator<DothtmlControl> GetEnumerator()
        {
            return Controls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Controls.GetEnumerator();
        }

        protected override void AddError(string message) => AddGlobalError(message);
    }
}