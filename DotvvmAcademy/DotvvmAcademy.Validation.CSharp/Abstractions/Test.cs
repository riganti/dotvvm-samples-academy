using System;
using System.Collections.Generic;
using System.Text;

namespace DotvvmAcademy.Validation.CSharp.Abstractions
{
    public interface Watjhf<in Ttype>
    {

    }

    public struct Test<TType, TTypeT>
    {
        public Test(string s)
        {

        }

        ~Test()
        {

        }

        public enum TestEnum
        {
            Wat
        }


        public void MthodSdas<Tsdfasdf>()
        {

        }

        public delegate int TestDelegate(int value);

        internal object this[int index, string s, Test t]
        {
            get { /* return the specified index here */ }
            set { /* set the specified index to value here */ }
        }

        internal object this[int index, string s]
        {
            get { /* return the specified index here */ }
            set { /* set the specified index to value here */ }
        }

        public static object operator +(Test one, Test two)
        {
            return null;
        }

        public static implicit operator String(Test t)
        {
            return t.ToString();
        }

        public static explicit operator Test(string s)
        {
            return null;
        }

        public class NestedClass
        {

        }

        public struct NestedStruct
        {

        }

        public event EventHandler TestEventField;

        public event EventHandler TestEventProperty { add; remove; }

        public int MyProperty { get; set; }

        public void Method()
        {

        }

        public string Field;
    }
}
