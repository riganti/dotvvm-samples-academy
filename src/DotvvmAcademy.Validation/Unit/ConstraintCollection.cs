using System;
using System.Collections;
using System.Collections.Generic;

namespace DotvvmAcademy.Validation.Unit
{
    public class ConstraintCollection : ICollection<IConstraint>
    {
        private readonly HashSet<IConstraint> set = new HashSet<IConstraint>(new ConstraintComparer());

        public int Count { get; }

        public bool IsReadOnly { get; }

        public void Add(IConstraint item)
        {
            // This does seem stupid, but it ensures the last overwritable contstraint remains
            set.Remove(item);
            set.Add(item);
        }

        public void Clear()
        {
            set.Clear();
        }

        public bool Contains(IConstraint item)
        {
            return set.Contains(item);
        }

        public void CopyTo(IConstraint[] array, int arrayIndex)
        {
            set.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IConstraint> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        public bool Remove(IConstraint item)
        {
            return set.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return set.GetEnumerator();
        }
    }
}