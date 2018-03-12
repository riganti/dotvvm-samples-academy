# MetadataName Samples

The 'Flags' and 'Properties' notes specify the MetadataNameKind flags and MetadataName properties respectively
that are set for every instance of that MetadataName kind, although other may be set as well.

---

## DefaultConvention

### Global Types

> GlobalType

Flags: Type
Properties: Name

### Top Level Types

> Some.Namespace.SomeType

Flags: Type
Properties: Namespace, Name

### Nested Types

> Some.Namespace.SomeType/NestedType

Flags: Type, NestedType
Properties: Owner, Name

### Generic Types

__Arity__ is the number after _\'_. It represents the number of the type's own generic parameters.

> Some.Namespace.SomeType\`1

> Some.Namespace.SomeType\`1/NestedType\`2

Flags: Type
Properties: Name, Arity

### Array Types

> Some.Namespace.SomeType[]

> Some.Namespace.SomeType[][]

> Some.Namespace.SomeType[,]

> Some.Namespace.SomeType[,,]

Flags: Type, ArrayType
Properties: Owner, Dimension

### Pointer Types

> Some.Namespace.SomeStruct*

> Some.Namespace.SomeStruct**

> Some.Namespace.SomeStruct*[]

Flags: Type, PointerType
Properties: Owner, IndirectionLevel

### Nullable Types

> System.Nullable<Some.Namespace.SomeStruct>

Flags: Type, ConstructedType
Properties: Owner, TypeArguments

### Simple Types

In this order: `sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, bool`

> System.SByte

> System.Byte

> System.Int16

> System.UInt16

> System.Int32

> System.UInt32

> System.Int64

> System.UInt64

> System.Char

> System.Single

> System.Double

> System.Decimal

> System.Boolean

Flags: Type
Properties: Namespace, Name

### The Void Type

> System.Void

Flags: Type
Properties: Namespace, Name

### Class Types

> System.Object

> System.String

> System.ValueType

> System.Enum

> System.Array

> System.Delegate

> System.Exception

Flags: Type
Properties: Namespace, Name

### The Dynamic Type

WIP

### Constructed Types

> Some.Namespace.SomeType<Some.Namespace.SomeStruct>

> Some.Namespace.SomeType<Some.Namespace.SomeStruct, System.String>

> System.Collections.Generic.IEnumerable<System.Object>

> System.Nullable<System.UInt64>

Type: Type, ConstructedType
Properties: Owner, TypeArguments

### Generic Type Parameters

WIP

> Some.Namespace.SomeType%TParam
> System.Void Some.Namespace.SomeType::Method(System.String)%TParam

Flags: Type, TypeParameter
Properties: Owner, Name

### Fields and events

> GlobalType::Field

> Some.Namespace.SomeType::Field

> Some.Namespace.SomeType/NestedType::Field

Flags: Member
Properties: Owner, Name

### Methods

> System.Void GlobalType::Method()

> System.String GlobalType::Method(System.String)

> GlobalType Some.Namespace.SomeType`1::Method(System.UInt32,System.Collections.Generic.IEnumerable<System.String>)

Flags: Member, Method
Properties: Owner, Name, Parameters, ReturnType

### Properties

> System.UInt32 GlobalType::Property()

> System.String Some.Namespace.SomeType`1::Property()

Flags: Member, Method
Properties: Owner, Name, ReturnType

### Indexers

> Some.Namespace.SomeType GlobalType::Indexer(System.String)

Flags: Member, Method
Properties: Owner, Name, Parameters, ReturnType


### Generic Methods

> System.Void GlobalType::Method\`1()

> GlobalType Some.Namespace.SomeType\`1::Method\`2(System.String)

Flags: Member, Method
Properties: Owner, Name, Parameters, ReturnType, Arity

### Constructed Methods

> System.Void GlobalType::Method<System.String>()

> System.Void GlobalType::Method<Some.Namespace.SomeType%TParam>(System.String)

Flags: Member, Method, ConstructedMethod
Properties: Owner, TypeArguments

---

## HumanReadableConvention

Samples are the same as in DefaultConvention if not specified otherwise.

### Generic Types

Without the arity suffix.

> Some.Namespace.SomeType

### Nullable Types

> Some.Namespace.SomeStruct?

### Simple Types

> sbyte

> byte

> short

> ushort

> int

> uint

> long

> ulong

> char

> float

> double

> decimal

> bool

### The Void Type

> void

### Class Types

> object

> string

> System.ValueType

> System.Enum

> System.Array

> System.Delegate

> System.Exception

### The Dynamic Type

> dynamic