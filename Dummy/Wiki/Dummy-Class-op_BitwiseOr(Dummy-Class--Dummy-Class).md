#### [Dummy](./Home.md 'Home')
### [Dummy](./Dummy.md 'Dummy').[Class](./Dummy-Class.md 'Dummy.Class')
## Class.operator |(Dummy.Class, Dummy.Class) Operator
Evaluates both [left](#Dummy-Class-op_BitwiseOr(Dummy-Class--Dummy-Class)-left 'Dummy.Class.op_BitwiseOr(Dummy.Class, Dummy.Class).left') and [right](#Dummy-Class-op_BitwiseOr(Dummy-Class--Dummy-Class)-right 'Dummy.Class.op_BitwiseOr(Dummy.Class, Dummy.Class).right') even if [left](#Dummy-Class-op_BitwiseOr(Dummy-Class--Dummy-Class)-left 'Dummy.Class.op_BitwiseOr(Dummy.Class, Dummy.Class).left') evaluates to `true`, so that the operation result is `true` regardless of the value of [right](#Dummy-Class-op_BitwiseOr(Dummy-Class--Dummy-Class)-right 'Dummy.Class.op_BitwiseOr(Dummy.Class, Dummy.Class).right').  
```csharp
public static bool operator |(Dummy.Class left, Dummy.Class right);
```
#### Parameters
<a name='Dummy-Class-op_BitwiseOr(Dummy-Class--Dummy-Class)-left'></a>
`left` [Class](./Dummy-Class.md 'Dummy.Class')  
Left operator parameter.  
  
<a name='Dummy-Class-op_BitwiseOr(Dummy-Class--Dummy-Class)-right'></a>
`right` [Class](./Dummy-Class.md 'Dummy.Class')  
Right operator parameter.  
  
#### Returns
[System.Boolean](https://docs.microsoft.com/dotnet/api/System.Boolean 'System.Boolean')  
`true` if [left](#Dummy-Class-op_BitwiseOr(Dummy-Class--Dummy-Class)-left 'Dummy.Class.op_BitwiseOr(Dummy.Class, Dummy.Class).left') is evaluated as `true` , otherwise, `false`.  
