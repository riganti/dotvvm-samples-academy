using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DotvvmAcademy.Meta
{
    public class PropertyEqualityComparer : IEqualityComparer<object>, IEqualityComparer
    {
        public new bool Equals(object one, object two)
        {
            var oneType = one.GetType();
            var twoType = two.GetType();
            var onesProperties = oneType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var onesProperty in onesProperties)
            {
                var twosProperty = twoType.GetProperty(onesProperty.Name, BindingFlags.Instance | BindingFlags.Public);
                var onesValue = onesProperty.GetValue(one);
                var twosValue = twosProperty.GetValue(two);
                if (!onesValue.Equals(twosValue))
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}