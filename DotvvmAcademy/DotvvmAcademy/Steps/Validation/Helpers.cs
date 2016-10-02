using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Compilation.ControlTree;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotvvmAcademy.Lessons;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DotvvmAcademy.Steps.Validation
{
    public static class Helpers
    {

        public static IEnumerable<ResolvedTreeNode> GetDescendants(this ResolvedContentNode node)
        {
            yield return node;
            foreach (var child in node.Content.SelectMany(n => n.GetDescendants()))
            {
                yield return child;
            }
        }

        public static IEnumerable<ResolvedControl> GetDescendantControls<T>(this ResolvedContentNode node)
        {
            return GetDescendants(node).OfType<ResolvedControl>().Where(c => c.Metadata.Type == typeof(T));
        }

        public static string GetValueBindingText(this ResolvedControl control, IPropertyDescriptor property)
        {
            IAbstractPropertySetter binding;
            if (!control.TryGetProperty(property, out binding) || !(binding is ResolvedPropertyBinding))
            {
                throw new CodeValidationException(string.Format(Texts.MissingPropertyError, control.Metadata.Type.Name, property.Name));
            }

            var typedBinding = ((ResolvedPropertyBinding)binding);
            if (typedBinding.Binding.BindingType != typeof(ValueBindingExpression))
            {
                throw new CodeValidationException(string.Format(Texts.ValueBindingExpected, control.Metadata.Type.Name, property.Name));
            }

            return typedBinding.Binding.Value.Trim();
        }

        public static string GetCommandBindingText(this ResolvedControl control, IPropertyDescriptor property)
        {
            IAbstractPropertySetter binding;
            if (!control.TryGetProperty(property, out binding) || !(binding is ResolvedPropertyBinding))
            {
                throw new CodeValidationException(string.Format(Texts.MissingPropertyError, control.Metadata.Type.Name, property.Name));
            }

            var typedBinding = ((ResolvedPropertyBinding)binding);
            if (typedBinding.Binding.BindingType != typeof(CommandBindingExpression))
            {
                throw new CodeValidationException(string.Format(Texts.CommandBindingExpected, control.Metadata.Type.Name, property.Name));
            }

            return typedBinding.Binding.Value.Trim();
        }

        public static string GetValue(this ResolvedControl control, IPropertyDescriptor property)
        {
            IAbstractPropertySetter value;
            if (!control.TryGetProperty(property, out value) || !(value is ResolvedPropertyValue))
            {
                throw new CodeValidationException(string.Format(Texts.MissingPropertyError, control.Metadata.Type.Name, property.Name));
            }

            return ((ResolvedPropertyValue)value).Value?.ToString();
        }

        public static Assembly CompileToAssembly(this CSharpCompilation compilation)
        {
            using (var ms = new MemoryStream())
            {
                var emitResult = compilation.Emit(ms);
                if (!emitResult.Success)
                {
                    throw new CodeValidationException("The code couldn't be compiled!\r\n" + string.Join("\r\n", emitResult.Diagnostics));
                }
                ms.Position = 0;

                return AssemblyLoadContext.Default.LoadFromStream(ms);
            }
        }

        public static void ExecuteSafe(this LessonBase lesson, Action action)
        {
            try
            {
                action();
            }
            catch (RuntimeBinderException ex)
            {
                throw new CodeValidationException(Lesson1Texts.CommandSignatureError, ex);
            }
            catch (Exception ex)
            {
                throw new CodeValidationException(Lesson1Texts.CommandResultError, ex);
            }
        }

        public static bool CheckNameAndType(this IPropertySymbol symbol, string name, string type)
        {
            return symbol.Name == name 
                && symbol.Type.ToDisplayString() == type
                && symbol.DeclaredAccessibility == Accessibility.Public
                && symbol.GetMethod != null
                && symbol.SetMethod != null;
        }

        public static bool CheckNameAndVoid(this IMethodSymbol symbol, string name)
        {
            return symbol.Name == name
                && symbol.ReturnsVoid
                && symbol.DeclaredAccessibility == Accessibility.Public;
        }

        public static bool CheckNameAndType(this IMethodSymbol symbol, string name, string type)
        {
            return symbol.Name == name
                && symbol.ReturnType.ToDisplayString() == type
                && symbol.DeclaredAccessibility == Accessibility.Public;
        }
    }
}
