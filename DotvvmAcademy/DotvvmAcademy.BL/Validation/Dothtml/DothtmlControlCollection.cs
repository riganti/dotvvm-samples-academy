using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotvvmAcademy.BL.Validation.Dothtml
{
    public sealed class DothtmlControlCollection : IEnumerable<DothtmlControl>
    {

        public DothtmlControlCollection(IEnumerable<DothtmlControl> controls)
        {
            Controls = controls;
        }
        public static DothtmlControlCollection Inactive => new DothtmlControlCollection(Enumerable.Empty<DothtmlControl>()) { IsActive = false};

        public IEnumerable<DothtmlControl> Controls { get; private set; } = new List<DothtmlControl>();

        public bool IsActive { get; private set; } = true;

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
    }
}