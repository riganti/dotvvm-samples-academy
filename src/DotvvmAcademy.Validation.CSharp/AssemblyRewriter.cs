using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.IO;

namespace DotvvmAcademy.Validation.CSharp
{
    public class AssemblyRewriter
    {
        public const string ContainerClassName = "SafeguardContainer";
        public const string SafeguardFieldName = "Safeguard";

        public void Rewrite(Stream source, Stream target)
        {
            var assembly = AssemblyDefinition.ReadAssembly(source);
            foreach (var module in assembly.Modules)
            {
                var checkMethod = InjectSafeguard(module);
                foreach (var type in module.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        InjectSafeguardCalls(method, checkMethod);
                    }
                }
            }
            assembly.Write(target);
        }

        private MethodDefinition InjectSafeguard(ModuleDefinition module)
        {
            var safeguardReference = module.ImportReference(typeof(AssemblySafeguard));

            // the safeguard container shouldn't be called the same every time
            var id = Guid.NewGuid();

            // create the SafeguardContainer class
            var containerNamespace = $"{nameof(AssemblyRewriter)}.{id:N}";
            var containerClass = new TypeDefinition(
                @namespace: containerNamespace,
                name: ContainerClassName,
                attributes: TypeAttributes.Class | TypeAttributes.Abstract | TypeAttributes.Sealed,
                baseType: module.ImportReference(typeof(object)));

            // create the safeguard field
            var field = new FieldDefinition(
                name: SafeguardFieldName,
                attributes: FieldAttributes.Static | FieldAttributes.Assembly,
                fieldType: safeguardReference);
            containerClass.Fields.Add(field);

            // create .cctor and assign the field
            var cctor = new MethodDefinition(
                name: ".cctor", // static constructor
                attributes: MethodAttributes.Private
                            | MethodAttributes.HideBySig
                            | MethodAttributes.Static
                            | MethodAttributes.SpecialName
                            | MethodAttributes.RTSpecialName,
                returnType: module.ImportReference(typeof(void)));
            {
                var il = cctor.Body.GetILProcessor();
                var safeguardCtor = typeof(AssemblySafeguard).GetConstructor(Type.EmptyTypes);
                il.Emit(OpCodes.Newobj, module.ImportReference(safeguardCtor));
                il.Emit(OpCodes.Stsfld, field);
                il.Emit(OpCodes.Ret);
            }
            containerClass.Methods.Add(cctor);

            // create the static Check method
            var checkMethod = new MethodDefinition(
                name: "Check",
                attributes: MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Static,
                returnType: module.ImportReference(typeof(void)));
            {
                var il = checkMethod.Body.GetILProcessor();
                var safeguardCheck = typeof(AssemblySafeguard).GetMethod(nameof(AssemblySafeguard.Check));
                il.Emit(OpCodes.Ldsfld, field);
                il.Emit(OpCodes.Callvirt, module.ImportReference(safeguardCheck));
                il.Emit(OpCodes.Ret);
            }
            containerClass.Methods.Add(checkMethod);

            module.Types.Add(containerClass);
            return checkMethod;
        }

        private void InjectSafeguardCalls(MethodDefinition method, MethodDefinition checkMethod)
        {
            if (method.DeclaringType == checkMethod.DeclaringType)
            {
                return;
            }

            var il = method.Body.GetILProcessor();
            var instructions = method.Body.Instructions.ToArray();
            for (int i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];
                switch (instruction.OpCode.Code)
                {
                    case Code.Nop:
                        continue;
                    case Code.Br_S:
                        instruction.OpCode = OpCodes.Br;
                        break;
                    case Code.Brfalse_S:
                        instruction.OpCode = OpCodes.Brfalse;
                        break;
                    case Code.Brtrue_S:
                        instruction.OpCode = OpCodes.Brtrue;
                        break;
                    case Code.Beq_S:
                        instruction.OpCode = OpCodes.Beq;
                        break;
                    case Code.Bge_S:
                        instruction.OpCode = OpCodes.Bge_S;
                        break;
                    case Code.Bgt_S:
                        instruction.OpCode = OpCodes.Bgt;
                        break;
                    case Code.Ble_S:
                        instruction.OpCode = OpCodes.Ble;
                        break;
                    case Code.Blt_S:
                        instruction.OpCode = OpCodes.Blt;
                        break;
                    case Code.Bne_Un_S:
                        instruction.OpCode = OpCodes.Bne_Un;
                        break;
                    case Code.Bge_Un_S:
                        instruction.OpCode = OpCodes.Bge_Un;
                        break;
                    case Code.Bgt_Un_S:
                        instruction.OpCode = OpCodes.Bgt_Un;
                        break;
                    case Code.Ble_Un_S:
                        instruction.OpCode = OpCodes.Ble_Un;
                        break;
                    case Code.Blt_Un_S:
                        instruction.OpCode = OpCodes.Blt_Un;
                        break;
                }
                var checkCall = il.Create(OpCodes.Call, checkMethod);
                il.InsertBefore(instruction, checkCall);
                if (instruction.OpCode == OpCodes.Brfalse_S)
                {
                    instruction.Operand = instructions[^1];
                }
                for (int j = 0; j < instructions.Length; j++)
                {
                    var referent = instructions[j];
                    if (referent.Operand == instruction)
                    {
                        referent.Operand = checkCall;
                    }
                }
            }
        }
    }
}
