//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Text;

//namespace DotvvmAcademy.Validation.Dothtml
//{
//    internal static class XPathDothtmlHelper
//    {
//        public static ImmutableArray<XPathDothtmlAttribute> SetupAttributes(ImmutableArray<XPathDothtmlAttribute>.Builder builder)
//        {
//            if (builder.Count == 0)
//            {
//                return default(ImmutableArray<XPathDothtmlAttribute>);
//            }
//            var attributes = builder.ToImmutable();
//            for (int i = 0; i < attributes.Length; i++)
//            {
//                var attribute = attributes[i];
//                attribute.Root = Root;
//                attribute.Parent = this;
//                attribute.FirstSibling = attributes[0];
//                if (i > 0)
//                {
//                    attribute.PreviousSibling = attributes[i - 1];
//                }
//                if (i < attributes.Length - 1)
//                {
//                    attribute.NextSibling = attributes[i + 1];
//                }
//            }
//        }

//        public void SetChildren(ImmutableArray<XPathDothtmlElement>.Builder builder)
//        {
//            if (builder.Count == 0)
//            {
//                return;
//            }
//            Children = builder.ToImmutable();
//            for (int i = 0; i < Children.Length; i++)
//            {
//                var child = Children[i];
//                child.Root = Root;
//                child.Parent = this;
//                child.FirstSibling = Children[0];
//                if (i > 0)
//                {
//                    child.PreviousSibling = Children[i - 1];
//                }
//                if (i < Children.Length - 1)
//                {
//                    child.NextSibling = Children[i + 1];
//                }
//            }
//        }
//    }
//}
