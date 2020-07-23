﻿using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotvvmAcademy.Validation.CSharp
{
    public class OldRewriter
    {
        public const string RewriterNamespace = "DotvvmAcademy.Validation.CSharp";
        public const string ContainingClassPrefix = "SafeguardContainer";
        public const string SafeguardFieldName = "Safeguard";

        public void Rewrite(Stream source, Stream target)
        {
            var rewriteId = Guid.NewGuid();
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
        }

        private FieldDefinition InjectSafeguardField(Guid rewriteId, ModuleDefinition module)
        {
            // Create SafeContainer{Guid} class
            var containingClassName = $"{ContainingClassPrefix}{rewriteId:N}";
            var typeAttributes = TypeAttributes.Class | TypeAttributes.Abstract | TypeAttributes.Sealed;
            var containingClass = new TypeDefinition(RewriterNamespace, containingClassName,
                typeAttributes, module.ImportReference(typeof(object)));

            // Create Safeguard field
            var fieldAttributes = FieldAttributes.Static | FieldAttributes.Assembly;
            var field = new FieldDefinition(SafeguardFieldName, fieldAttributes,
                module.ImportReference(typeof(OldSafeguard)));
            containingClass.Fields.Add(field);

            // Create static constructor
            var methodAttributes =
                MethodAttributes.Private |
                MethodAttributes.HideBySig |
                MethodAttributes.Static |
                MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName;
            var staticConstructor = new MethodDefinition(".cctor", methodAttributes,
                module.ImportReference(typeof(void)));
            var il = staticConstructor.Body.GetILProcessor();
            var safeguardConstructor = module.ImportReference(typeof(OldSafeguard)
                .GetConstructor(new[] { typeof(int) }));
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Newobj, safeguardConstructor);
            il.Emit(OpCodes.Stsfld, field);
            il.Emit(OpCodes.Ret);
            containingClass.Methods.Add(staticConstructor);
            module.Types.Add(containingClass);

            return field;
        }

        private void InjectSafeguardIntoMethod(FieldDefinition safeguard, MethodDefinition method,
            ModuleDefinition module)
        {
            if (method.DeclaringType == safeguard.DeclaringType)
            {
                return;
            }

            var il = method.Body.GetILProcessor();
            var onInstruction = module.ImportReference(typeof(OldSafeguard)
                .GetMethod(nameof(OldSafeguard.OnInstruction)));
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
