using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp.AssemblyAnalysis
{
    public class DefaultAssemblyRewriter : IAssemblyRewriter
    {
        public const string RewriterNamespace = "DotvvmAcademy.Validation.Rewritten";
        public const string ContainingClassPrefix = "SafeguardContainer";
        public const string SafeguardFieldName = "Safeguard";

        public Task Rewrite(Stream source, Stream target)
        {
            var rewriteId = Guid.NewGuid();
            return Task.Run(() =>
            {
                var assembly = AssemblyDefinition.ReadAssembly(source);
                foreach (var module in assembly.Modules)
                {
                    var safeguardField = InjectSafeguardField(rewriteId, module);
                    foreach (var type in module.Types)
                    {
                        foreach (var method in type.Methods)
                        {
                            InjectSafeguardIntoMethod(safeguardField, method, module);
                        }
                    }
                }
                assembly.Write(target);
            });
        }

        private FieldDefinition InjectSafeguardField(Guid rewriteId, ModuleDefinition module)
        {
            // Create SafeContainer{Guid} class
            var containingClassName = $"{ContainingClassPrefix}{rewriteId.ToString("N")}";
            var typeAttributes = TypeAttributes.Class | TypeAttributes.Abstract | TypeAttributes.Sealed;
            var containingClass = new TypeDefinition(RewriterNamespace, containingClassName, typeAttributes, module.Import(typeof(object)));

            // Create Safeguard field
            var fieldAttributes = FieldAttributes.Static | FieldAttributes.Assembly;
            var field = new FieldDefinition(SafeguardFieldName, fieldAttributes, module.Import(typeof(IAssemblySafeguard)));
            containingClass.Fields.Add(field);

            // Create static constructor
            var methodAttributes =
                MethodAttributes.Private |
                MethodAttributes.HideBySig |
                MethodAttributes.Static |
                MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName;
            var staticConstructor = new MethodDefinition(".cctor", methodAttributes, module.Import(typeof(void)));
            var il = staticConstructor.Body.GetILProcessor();
            var safeguardConstructor = module.Import(typeof(DefaultAssemblySafeguard).GetConstructor(new[] { typeof(int) }));
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Newobj, safeguardConstructor);
            il.Emit(OpCodes.Stsfld, field);
            il.Emit(OpCodes.Ret);
            containingClass.Methods.Add(staticConstructor);
            module.Types.Add(containingClass);

            return field;
        }

        private void InjectSafeguardIntoMethod(FieldDefinition safeguard, MethodDefinition method, ModuleDefinition module)
        {
            if (method.DeclaringType == safeguard.DeclaringType)
            {
                return;
            }

            var il = method.Body.GetILProcessor();
            var onInstruction = module.Import(typeof(DefaultAssemblySafeguard).GetMethod(nameof(DefaultAssemblySafeguard.OnInstruction)));
            var instructions = method.Body.Instructions.ToArray();
            for (int i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];
                if (instruction.OpCode == OpCodes.Nop)
                {
                    continue;
                }
                var ldsfld = il.Create(OpCodes.Ldsfld, safeguard);
                il.InsertBefore(instruction, ldsfld);
                il.InsertBefore(instruction, il.Create(OpCodes.Callvirt, onInstruction));
                ReplaceOperand(il, instruction, ldsfld);
            }
        }

        private void ReplaceOperand(ILProcessor il, object target, object operand)
        {
            var instructions = il.Body.Instructions;
            for (int i = 0; i < instructions.Count; i++)
            {
                var instruction = instructions[i];
                if (instruction.Operand == target)
                {
                    instruction.Operand = operand;
                }
            }
        }
    }
}