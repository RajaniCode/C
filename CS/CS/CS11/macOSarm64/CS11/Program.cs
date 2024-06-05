// CS 11
/*
1. Raw string literals
2. Generic math support
3. Generic attributes
4. UTF-8 string literals
5. Newlines in string interpolation expressions
6. List patterns
7. File-local types
8. Required members
9. Auto-default structs
10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string
11. Extended nameof scope
12. Numeric IntPtr and UIntPtr: nint and nuint types now alias System.IntPtr and System.UIntPtr, respectively
13. ref fields and scoped ref
14. Improved method group conversion to delegate
15. Warning wave 7
*/


// NB
// default
// <ImplicitUsings>enable</ImplicitUsings>
// <Nullable>enable</Nullable>
// Implicit using directives
// /Users/rajaniapple/Desktop/Working/CS/CS11/obj/Debug/net7.0/CS11.GlobalUsings.g.cs
/*
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
*/


using System.Runtime.InteropServices;

// 7. File-local types
// Error CS9055: File-local type 'GlobalUsingStaticFileLocal' cannot be used in a 'global using static' directive
// global using static GlobalUsingStaticFileLocal;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using FileLocalCClientNamespace;
using FileLocalNamespace;
using static System.Formats.Asn1.AsnWriter;


// Version
/******************************************************************************/
// using System;
Console.WriteLine($"Environment.OSVersion: {Environment.OSVersion}");
Console.WriteLine($"Environment.OSVersion.Platform: {Environment.OSVersion.Platform}");
Console.WriteLine($"Environment.OSVersion.Version: {Environment.OSVersion.Version}");
Console.WriteLine($"Environment.OSVersion.VersionString: {Environment.OSVersion.VersionString}");
Console.WriteLine($"Environment.OSVersion.Version.Major: {Environment.OSVersion.Version.Major}");
Console.WriteLine($"Environment.OSVersion.Version.Minor: {Environment.OSVersion.Version.Minor}");
// Empty
// Console.WriteLine($"Environment.OSVersion.ServicePack: {Environment.OSVersion.ServicePack}");

// Environment.Version property returns the .NET runtime version for .NET 5+ and .NET Core 3.x
// Not recommend for .NET Framework 4.5+
Console.WriteLine($"Environment.Version: {Environment.Version}");
//  <-- Keep this information secure! -->
// Console.WriteLine($"Environment.UserName: {Environment.UserName}");

//  <-- Keep this information secure! -->
// Console.WriteLine($"Environment.MachineName: {Environment.MachineName}");

//  <-- Keep this information secure! -->
// Console.WriteLine($"Environment.UserDomainName: {Environment.UserDomainName}");

Console.WriteLine($"Environment.Is64BitOperatingSystem: {Environment.Is64BitOperatingSystem}");
Console.WriteLine($"Environment.Is64BitProcess: {Environment.Is64BitProcess}");

//  <-- Keep this information secure! -->
// Console.WriteLine("CurrentDirectory: {0}", Environment.CurrentDirectory);
//  <-- Keep this information secure! -->
// Console.WriteLine("SystemDirectory: {0}", Environment.SystemDirectory);

// RuntimeInformation.FrameworkDescription property gets the name of the .NET installation on which an app is running
// .NET 5+ and .NET Core 3.x // .NET Framework 4.7.1+ // Mono 5.10.1+
// using System.Runtime.InteropServices;
Console.WriteLine($"RuntimeInformation.FrameworkDescription: {RuntimeInformation.FrameworkDescription}");

Console.WriteLine($"RuntimeInformation.ProcessArchitecture: {RuntimeInformation.ProcessArchitecture}");
Console.WriteLine($"RuntimeInformation.OSArchitecture: {RuntimeInformation.OSArchitecture}");
Console.WriteLine($"RuntimeInformation.OSDescription): {RuntimeInformation.OSDescription}");
// .NET Mono 6.12.0 does not contain a definition for `RuntimeIdentifier'
Console.WriteLine($"RuntimeInformation.RuntimeIdentifier: {RuntimeInformation.RuntimeIdentifier}");

// <-- Keep this information secure! -->
#if comments
Console.WriteLine("Environment Variables:");
foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables())
{
    Console.WriteLine("{0} = {1}", de.Key, de.Value);
}
#endif
Console.WriteLine();
/******************************************************************************/


// 1. Raw string literals
/******************************************************************************/
Console.WriteLine("1. Raw string literals");
// At least three double-quotes
Console.WriteLine(""""
    Starts with at least three double-quotes
    "without requiring escape sequences"
    Ends with the same number of double-quote
    """");

Console.WriteLine("1. Raw string literals: Interpolated raw string literals");
int xaxis = 1;
int yaxis = 2;
Console.WriteLine($"""Pair of numbers: "{xaxis}, {yaxis}" """);
// use the same number of braces (excluding braces to be resulted) as the number of $ characters
Console.WriteLine($$"""Pair of numbers: {{{xaxis}}, {{yaxis}}} """);
Console.WriteLine();
/******************************************************************************/


// 2. Generic math support
/******************************************************************************/
Console.WriteLine("2. Generic math support: static abstract in interface");
OperatorImplementation.StaticAbstract();

Console.WriteLine("2. Generic math support: static virtual in interface");
// static virtual or abstract interface member can be accessed only on a type parameter
// IStaticGeneric<RecordImplementation>.StaticAbstract();
// IStaticGeneric<RecordImplementation>.StaticVirtual();
OperatorImplementation.StaticAbstract();
OperatorImplementation.StaticVirtual();

Console.WriteLine("2. Generic math support: static abstract operator in interface");
// 'target-typed object creation' // CS 9+
OperatorImplementation implementationOperator = new();
Console.WriteLine($"nameof: {nameof(implementationOperator)}");
Console.WriteLine($"GetType: {implementationOperator.GetType()}");
Console.WriteLine($"typeof: {typeof(OperatorImplementation)}");
Console.WriteLine($"typeof GetType: {typeof(OperatorImplementation).GetType()}");
Console.WriteLine($"IsValueType: {typeof(OperatorImplementation).IsValueType}");
Console.WriteLine("overloading unary operator ++");
for (int i = 0; i < 26; i++)
{
    Console.WriteLine(++implementationOperator);
}
Console.WriteLine("overloading unary operator --");
for (int i = 0; i < 26; i++)
{
    Console.WriteLine(--implementationOperator);
}
Console.WriteLine("2. Generic math support: static extern in interface");
Console.WriteLine(IStatic.c_code(123456789));

Console.WriteLine("2. Generic math support: arithmetic shift and logical shift");
/*
>>
arithmetic shift
The value of the most significant bit is propagated to the high-order empty bit positions)
arithmetic shift for
11111111111111111111111111111000
(Prefix 1's) for negative shift-expression
11111111111111111111111111111110 = Prefix 2 1's for >> 2
11111111111111111111111111111111 = Prefix 3 1's for >> 3
11111111111111111111111111111111 = Prefix 4 1's for >> 4
>>>
logical shift
The high-order empty bit positions are always set to zero
logical shift for
11111111111111111111111111111000
(Prefix 0's) for negative shift-expression
00111111111111111111111111111110 = Prefix 2 0's for >>> 2
00011111111111111111111111111111 = Prefix 3 0's for >>> 3
00001111111111111111111111111111 = Prefix 4 0's for >>> 4
*/
int x = -8;
Console.WriteLine($"x = {x}, hex: {x,8:x}, binary: {Convert.ToString(x, toBase: 2),32}");
Console.WriteLine("arithmetic shift: >> 2");

int y = x >> 2;
Console.WriteLine($"y = x >> 2 = {y}, hex: {y,8:x}, binary: {Convert.ToString(y, toBase: 2),32}");
Console.WriteLine("logical shift: >>> 2");

int z = x >>> 2;
Console.WriteLine($"z = x >>> 2 = {z}, hex: {z,8:x}, binary: {Convert.ToString(z, toBase: 2).PadLeft(32, '0'),32}");

/*
Right Shifts
The right-shift operator causes the bit pattern in shift-expression to be shifted to the right by the number of positions specified by additive-expression.
For unsigned numbers, the bit positions that have been vacated by the shift operation are zero-filled.
For signed numbers, the sign bit is used to fill the vacated bit positions.
In other words, if the number is positive, 0 is used, and if the number is negative, 1 is used.
*/
int negative = -8;
int rightShiftNegative = negative >> 2;
Console.WriteLine($"rightShiftNegative = negative >> 2 = {rightShiftNegative}, hex: {rightShiftNegative,8:x}, binary: {Convert.ToString(rightShiftNegative, toBase: 2),32}");

int positive = 8;
int rightShiftPositive = positive >> 2;
Console.WriteLine($"rightShiftPositive = positive >> 2 = {rightShiftPositive}, hex: {rightShiftPositive,8:x}, binary: {Convert.ToString(rightShiftPositive, toBase: 2),32}");

/*
Left Shifts
The left-shift operator causes the bits in shift-expression to be shifted to the left by the number of positions specified by additive-expression.
The bit positions that have been vacated by the shift operation are zero-filled.
A left shift is a logical shift (the bits that are shifted off the end are discarded, including the sign bit).
*/
int leftShiftNegative = negative << 2;
Console.WriteLine($"leftShiftNegative = negative << 2 = {leftShiftNegative}, hex: {leftShiftNegative,8:x}, binary: {Convert.ToString(leftShiftNegative, toBase: 2),32}");

int leftShiftPositive = positive << 2;
Console.WriteLine($"leftShiftPositive = positive << 2 = {leftShiftPositive}, hex: {leftShiftPositive,8:x}, binary: {Convert.ToString(leftShiftPositive, toBase: 2),32}");

/*
NB
The result of a shift operation is undefined if additive-expression is negative or if additive-expression is greater than or equal to the number of bits in the (promoted) shift-expression.
No shift operation takes place if additive-expression is 0.
*/

Console.WriteLine("2. Generic math support: relaxed shift operator");
// Second operand allowed to be types that implement generic math interfaces
int a = x >> GenericMath.Add<int>(2, 4);
Console.WriteLine($"a = x >> GenericMath.Add<int>(2, 4) = {a}, hex: {a,8:x}, binary: {Convert.ToString(a, toBase: 2).PadLeft(32, '0'),32}");

Console.WriteLine("2. Generic math support: checked and unchecked user defined operators");
RecordOperatorOverloading operatorOverload1 = new(int.MaxValue, int.MaxValue);
RecordOperatorOverloading operatorOverload2 = new (10, 20);
RecordOperatorOverloading operatorOverload3 = new();
Console.WriteLine("Operator Overload 1:");
operatorOverload1.Print();
Console.WriteLine("Operator Overload 2:");
operatorOverload2.Print();
Console.WriteLine("Operator Overload 3:");
operatorOverload3.Print();

try
{
    // operator without the checked modifier returns an instance representing a truncated result.
    unchecked
    {
        Console.WriteLine("Adding objects operatorOverload3 = operatorOverload1 + operatorOverload2:");
        operatorOverload3 = operatorOverload1 + operatorOverload2;
        operatorOverload3.Print();
    }
    // checked operator throws an OverflowException
    checked
    {
        Console.WriteLine("Adding objects operatorOverload3 = operatorOverload1 + operatorOverload2:");
        operatorOverload3 = operatorOverload1 + operatorOverload2;
        operatorOverload3.Print();
    }
}
catch (OverflowException e)
{
    Console.WriteLine(e.Message);
}
Console.WriteLine();
/******************************************************************************/


// 3. Generic attributes
/******************************************************************************/
GenericAttribute<string> attributeGeneric = new();
Console.WriteLine(attributeGeneric.Method());
Console.WriteLine();
/******************************************************************************/


// 4. UTF-8 string literals
/******************************************************************************/
// UTF-8 string literals aren't compile time constants; they're runtime constants.
// Therefore, they can't be used as the default value for an optional parameter.
// UTF-8 string literals can't be combined with string interpolation.
// You can't use the $ token and the u8 suffix on the same string expression.
Console.WriteLine("4. UTF-8 string literals");
ReadOnlySpan<byte> readOnlySpanStringLiteral = "POST "u8;
Console.WriteLine(string.Join(", ", readOnlySpanStringLiteral.ToArray()));

byte[] stringLiteral = "POST "u8.ToArray();
Console.WriteLine(string.Join(", ", stringLiteral));

// System.ReadOnlySpan<T> equivalent // ASCII // P, O, S, T, Space // 80, 79, 83, 84, 32
ReadOnlySpan<byte> readOnlySpanEquivalent = new byte[] { 0x50, 0x4F, 0x53, 0x54, 0x20 };
Console.WriteLine(string.Join(", ", readOnlySpanEquivalent.ToArray()));
Console.WriteLine();
/******************************************************************************/


// 5. Newlines in string interpolation expressions
/******************************************************************************/
Console.WriteLine("5. Newlines in string interpolation expressions");
int score = new Random().Next(0, 100);
string grade = $"Academic grading for {score} is {
    score switch
    {
        > 90 => "Grade: A, GPA: 4.0.",
        > 80 => "Grade: B, GPA: 3.0.",
        > 70 => "Grade: C, GPA: 2.0.",
        > 60 => "Grade: D, GPA: 1.0.",
        _ => "Grade: F, GPA: 0.0.",
    }
}";
Console.WriteLine(grade);
Console.WriteLine();
/******************************************************************************/


// 6. List patterns
/******************************************************************************/
Console.WriteLine("6. List patterns");
// int[] sequence = { 2, 4, 6 };
// Or
List<int> sequence = new() { 2, 4, 6 };
Console.WriteLine(sequence is [2, 4, 6]); // True
Console.WriteLine(sequence is [1, 3, 5]); // False
Console.WriteLine(sequence is [6, 4, 2]); // False
Console.WriteLine(sequence is [2, 4, 6, 6]); // False
Console.WriteLine(sequence is [0 or 2, <= 4, >=6]); // True
// Cannot implicitly convert type 'double' to 'int'
// Console.WriteLine(sequence is [ 2.0, 4.0, 6.0 ]);

Console.WriteLine("6. List patterns: discard pattern (_), range pattern (..)");
Console.WriteLine(1);
Console.WriteLine(sequence is [_]); // False 
Console.WriteLine(sequence is [..]); // True
Console.WriteLine(sequence is [> 0]); // False

Console.WriteLine(2);
int[] single = { 0 };
Console.WriteLine(single is [_]); // True
Console.WriteLine(single is [..]); // True
Console.WriteLine(single is [>= 0]); // True
Console.WriteLine(single is [0, 1, 2]); // False

Console.WriteLine(3);
int[] none = { };
Console.WriteLine(none is [_]); // False
Console.WriteLine(none is [..]); // True 
Console.WriteLine(none is [ ]); // True

Console.WriteLine(4);
Console.WriteLine(new[] { 2, 4, 6 } is [.., > 0, 2, 4, 6]); // False
Console.WriteLine(new[] { 2, 4, 6 } is [.., > 0, 4, 6]); // True
Console.WriteLine(new[] { 2, 4, 6 } is [.., 2, 4, 6]); // True
Console.WriteLine(new[] { 2, 4, 6 } is [.., 4, 6]); // True

Console.WriteLine(5);
// Slice patterns may only be used once and directly inside a list pattern
Console.WriteLine(new[] { 2, 4, 6 } is [> 0, > 0, ..]); // True
Console.WriteLine(new[] { 2, 4, 6 } is [_, _, ..]); // True
Console.WriteLine(new[] { 2, 4, 6 } is [_, _, _, ..]); // True
Console.WriteLine(new[] { 2, 4, 6 } is [ 2, > 4, ..]); // False

Console.WriteLine(6);
Console.WriteLine(new[] { 2, 4, 6, 8 } is [_, _, ..]); // True
Console.WriteLine(new[] { 2, 4, 6, 8 } is [_, .., _]); // True
Console.WriteLine(new[] { 2, 4, 6, 8 } is [.., _, _]); // True

Console.WriteLine(7);
Console.WriteLine(new[] { 2, 4, 6, 8 } is [> 0, .. _]); // True
Console.WriteLine(new[] { 2, 4, 6, 8 } is [.., > 0, _]); // True
Console.WriteLine(new[] { 2, 4, 6, 8 } is [.., _, > 0]); // True
Console.WriteLine(new[] { 2, 4, 6, 8 } is [_, .., > 0]); // True

Console.WriteLine(8);
Console.WriteLine(new[] { 2, 4, 6, 8 } is [> 0, _, _]); // False
Console.WriteLine(new[] { 2, 4, 6, 8 } is [> 0, > 0, ..]); // True
Console.WriteLine(new[] { 2, 4, 6, 8 } is [.., > 0, > 0]); // True

Console.WriteLine(9);
Console.WriteLine(new[] { 2, 4, 6, 8 } is [> 2, > 4, ..]); // False
Console.WriteLine(new[] { 2, 4, 6, 8 } is [>= 2, > 4, ..]); // False
Console.WriteLine(new[] { 2, 4, 6, 8 } is [>= 2, 4, ..]); // True

Console.WriteLine(10);
Console.WriteLine(new[] { 2, 4, 6, 8 } is [>= 2, .., 2 or 8]); // True
Console.WriteLine(new[] { 2, 0, 0, 2 } is [2, 0, .., 0, 2]); // True
Console.WriteLine(new[] { 2, 0, 2 } is [2, 0, .., 0, 2]); // False
Console.WriteLine(new[] { 2, 4, 6 } is [var alpha, _, _]); // True

Console.WriteLine("6. List patterns: nest a subpattern within a slice pattern");
void Match(string pattern)
{
    var output = pattern is ['a' or 'A', .. var v, 'a' or 'A']
        ? $"Pattern {pattern} matches; middle chunk is {v}."
        : $"Pattern {pattern} doesn't match.";
    Console.WriteLine(output);
}
Match("Alpha");
Match("Alphabet");

void IsValid(List<int> numbers)
{
    var output = numbers.ToArray<int>() is [< 0, .. { Length : 2 or 4 }, > 0] ? "Yep" : "Nope";
    Console.WriteLine(output);
}
IsValid(new() { -2, 0, 2 });
IsValid(new() { -2, 0, 0, 2 });
Console.WriteLine();
/******************************************************************************/


// 7. File-local types
/******************************************************************************/
Console.WriteLine("7. File-local types");
Console.WriteLine("CS 11, File-local keyword 'file' is type modifier");
Console.WriteLine("CS 11, File-local cannot use access modifier");
Console.WriteLine("CS 11, File-local default access modifier is internal");

// Error CS0246: The type or namespace name 'FileLocal' could not be found 
// FileLocal localFile = new();
FileLocalClient clientFileLocal = new();
clientFileLocal.Print();

FileLocalCClient clientFileLocalC = new();
clientFileLocalC.Print();

FileLocalAttributeClient clientFileLocalAttribute = new();
clientFileLocalAttribute.Print();

AttributeClient clientAttribute = new();
clientAttribute.Print();
Console.WriteLine();
/******************************************************************************/


// 8. Required members
/******************************************************************************/
Console.WriteLine("8. Required members");
RquiredMembers membersRquired;
// Parameterless constructor not attributed with [System.Diagnostics.CodeAnalysis.SetsRequiredMembers] 
// Required members (Alpha, Beta) must be set in the object initializer or attribute constructor
// membersRquired = new();
membersRquired = new("foo", "bar");
membersRquired.NullableNumber = 555;
Console.WriteLine($"{membersRquired.Alpha}, {membersRquired.Beta}, {membersRquired.NullableNumber}");

ExtendedRquiredMembers rquiredMembersExtended;
// Parameterless constructor not attributed with [System.Diagnostics.CodeAnalysis.SetsRequiredMembers] 
// Required members (Alpha, Beta) must be set in the object initializer or attribute constructor
// rquiredMembersExtended = new();
rquiredMembersExtended = new("baz", "qux");
rquiredMembersExtended.NonNullableNumber = 777D;
Console.WriteLine($"{rquiredMembersExtended.Alpha}, {rquiredMembersExtended.Beta}, {rquiredMembersExtended.NonNullableNumber}");

RequiredRecord recordRequired;
// Parameterless constructor not attributed with [System.Diagnostics.CodeAnalysis.SetsRequiredMembers] 
// Required members (Alpha, Beta) must be set in the object initializer or attribute constructor
// recordRequired = new();
recordRequired = new("quux", "corge");
Console.WriteLine(recordRequired);

DerivedRecordRequired recordRequiredDerived;
// Parameterless constructor not attributed with [System.Diagnostics.CodeAnalysis.SetsRequiredMembers] 
// Required members (Alpha, Beta) must be set in the object initializer or attribute constructor
// recordRequiredDerived = new();
recordRequired = new("grault", "garply");
recordRequiredDerived = new(recordRequired);
Console.WriteLine(recordRequired);

RequiredStructure structureRequired;
// Required members are enforced for struct instances created with new StructType(), even when StructType has no parameterless constructor and the default struct constructor is used.
// Required members (Alpha, Beta) must be set in the object initializer or attribute constructor
// structureRequired = new();
// Required members are not enforced on instances of struct types created with default or default(StructType).
structureRequired = default(RequiredStructure);
Console.WriteLine(structureRequired);
structureRequired.Alpha = "waldo";
structureRequired.Beta = "fred";
Console.WriteLine(structureRequired);
Console.WriteLine();
/******************************************************************************/


// 9. Auto-default structs
/******************************************************************************/
Console.WriteLine("9. Auto-default structs");
AutoDefaultStructure structureAutoDefault = new(1);
Console.WriteLine(structureAutoDefault);

structureAutoDefault = new();
Console.WriteLine(structureAutoDefault);

structureAutoDefault = default(AutoDefaultStructure);
Console.WriteLine(structureAutoDefault);
Console.WriteLine();
/******************************************************************************/


// 10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string
/******************************************************************************/
PatternMatchSpan spanPatternMatch = new();
Console.WriteLine("10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string");
spanPatternMatch.Print();
Console.WriteLine();
/******************************************************************************/


// 11. Extended nameof scope
/******************************************************************************/
Console.WriteLine("11. Extended nameof scope");
NameOfExpression expressionNameOf = new();
expressionNameOf.Method("https://dotnet.microsoft.com/en-us/");
Console.WriteLine();
/******************************************************************************/


// 12. Numeric IntPtr and UIntPtr: nint and nuint types now alias System.IntPtr and System.UIntPtr, respectively
/******************************************************************************/
Console.WriteLine("12. Numeric IntPtr and UIntPtr: nint and nuint types now alias System.IntPtr and System.UIntPtr, respectively");
Console.WriteLine($"System.IntPtr.Size: {System.IntPtr.Size}, System.IntPtr.Size: {System.UIntPtr.Size}");
Console.WriteLine($"nint.Size: {nint.Size}, nuint.Size: {nuint.Size}");
Console.WriteLine();
/******************************************************************************/


// 13. ref fields and scoped ref
/******************************************************************************/
RefStruct structRef = new();
Console.WriteLine("13. ref fields and scoped ref");
structRef.Print();
Console.WriteLine();
/******************************************************************************/


// 14. Improved method group conversion to delegate
/******************************************************************************/
ImprovedMethodGroupConversionToDelegate methodGroupConversionToDelegateImproved = new();
methodGroupConversionToDelegateImproved.Print();
Console.WriteLine();
/******************************************************************************/


// 15. Warning wave 7
/******************************************************************************/
Console.WriteLine("15. Warning wave 7");
Console.WriteLine("Any new keywords added for C# will be all lower-case ASCII characters. This warning ensures that none of your types conflict with future keywords.");
Console.WriteLine("Address this warning by renaming the type to include at least one non-lower case ASCII character, such as an upper case character, a digit, or an underscore");
/******************************************************************************/


// 2. Generic math support
/******************************************************************************/
class GenericMath
{
    // CS 11
    // System.Numerics.INumber<T>
    public static T Add<T>(T first, T second) where T : System.Numerics.INumber<T>
    {
        T result;
        result = first + second;
        return result;
    }
}

record RecordOperatorOverloading
{
    int x;
    int y;

    public RecordOperatorOverloading()
    {
        x = 0;
        y = 0;
    }

    public RecordOperatorOverloading(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // unchecked context
    public static RecordOperatorOverloading operator +(RecordOperatorOverloading operator1, RecordOperatorOverloading operator2) // adding objects
    {
        RecordOperatorOverloading operatorOverload = new();
        operatorOverload.x = operator1.x + operator2.x;
        operatorOverload.y = operator1.y + operator2.y;
        return operatorOverload;
    }

    // checked context
    // Requires a matching non-checked version of the operator to also be defined
    // operator without the checked modifier is called in both a checked and unchecked context.
    public static RecordOperatorOverloading operator checked +(RecordOperatorOverloading operator1, RecordOperatorOverloading operator2) // adding objects
    {
        checked
        {
            RecordOperatorOverloading operatorOverload = new();
            operatorOverload.x = operator1.x + operator2.x;
            operatorOverload.y = operator1.y + operator2.y;
            return operatorOverload;
        }
    }

    public void Print()
    {
        Console.WriteLine($"x = {x}, y = {y}");
    }
}

partial interface IStatic
{
    // DllImport attribute cannot be applied to a method that is generic or contained in a generic method or type
    [System.Runtime.InteropServices.DllImport(@"/Users/rajaniapple/Desktop/GitHub/CS/CS/CS/CS11/macOSarm64/CS11/GCC/C/msys64-mingw64-gcc.so")]
    static extern int c_code(int number);

    // partial method must be declared within a partial type
    static partial void StaticPartial();
}

partial interface IStatic
{
    static partial void StaticPartial() { }
}

// CS 11
// The static virtual and static abstract methods declared in interfaces don't have a runtime dispatch mechanism analogous to virtual or abstract methods declared in classes.
// Instead, the compiler uses type information available at compile time.
// Therefore, static virtual methods are almost exclusively declared in generic interfaces.
// Furthermore, most interfaces that declare static virtual or static abstract methods declare that one of the type parameters must implement the declared interface. 
interface IStaticGeneric<T> where T : IStaticGeneric<T>
{
    // CS 11
    // static abstract only in interface
    // interface static abstract method must be implemented (implicitly or explicitly) in the implementing type without the override keyword
    static abstract void StaticAbstract();

    // CS 11
    // static virtual only in interface
    // optional to implement interface static virtual method (implicitly or explicitly) in the implementing type, however without the override keyword if implemented 
    static virtual void StaticVirtual()
    {
        Console.WriteLine("static virtual body in interface");
    }

    // NB
    // The parameter of a unary operator must be the containing type, or its type parameter constrained to it.
    // One of the parameters of a binary operator must be the containing type.
    // overloadable unary operators:
    // +  -  !  ~  ++  --  true  false
    // overloadable binary operators:
    // +  -  *  /  %  &  |  ^  <<  >>  ==  !=  >  <  <=  >=

    // CS 11
    // static abstract only in interface
    // interface static abstract operator must be implemented (implicitly) in the implementing type without the override keyword
    // Type arguments implement declared interface
    // The parameter type for ++ or -- operator must be the containing type, or its type parameter constrained to it
    static abstract T operator ++(T t);

    // CS 11
    // static virtual only in interface
    // optional to implement interface static virtual operator (implicitly) in the implementing type, however without the override keyword if implemented 
    static virtual T operator --(T t) => t;
}

// CS 11
// static abstract operator receiver type should be a valid record/struct type
// ref structs cannot implement interfaces and may not be used as a type argument
// struct OperatorImplementation : IStaticGeneric<OperatorImplementation>
record OperatorImplementation : IStaticGeneric<OperatorImplementation>
{
    int number = 0;
    int sixtyFive = 65;
    int hundredAndTwentyTwo = 122;
    
    // For struct // A 'struct' with field initializers must include an explicitly declared constructor.
    // public OperatorImplementation() { }

    // CS 11
    // mandatory implicit implementation
    public static OperatorImplementation operator ++(OperatorImplementation implementationOperator)
        => implementationOperator with
        {
            sixtyFive = implementationOperator.sixtyFive + 1,
            number = implementationOperator.sixtyFive
        };

    // CS 11
    // optional implicit implementation
    public static OperatorImplementation operator --(OperatorImplementation implementationOperator)
        => implementationOperator with
        {
            hundredAndTwentyTwo = implementationOperator.hundredAndTwentyTwo - 1,
            number = implementationOperator.hundredAndTwentyTwo
        };

    public override string ToString() => ((char)number).ToString();

    // CS 11
    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public static void StaticAbstract()
    {
        Console.WriteLine("static abstract method implicitly implemented in the implementing type");
    }
    // Or
    // explicit implementation
    static void IStaticGeneric<OperatorImplementation>.StaticAbstract()
    {
        Console.WriteLine("static abstract method explicitly implemented in the implementing type");
    }

    // CS 11
    // optional (implicit or explicit) implementation
    // implicit implementation
    public static void StaticVirtual()
    {
        Console.WriteLine("static virtual method implicitly implemented in the implementing type");
    }
    // Or
    // explicit implementation
    static void IStaticGeneric<OperatorImplementation>.StaticVirtual()
    {
        Console.WriteLine("static virtual method explicitly implemented in the implementing type");
    }
}
/******************************************************************************/


// 3. Generic attributes
/******************************************************************************/
class GenericAttribute<T> : Attribute
{
    // Type arguments must supply all type parameters when you apply the attribute.
    // In other words, the generic type must be fully constructed.
    // A type that includes at least one type argument is called a constructed type.
    // [GenericAttribute<T>()] // Not allowed, generic attributes must be fully constructed types
    // Type arguments must satisfy the same restrictions as the typeof operator.
    // Types that require metadata annotations aren't allowed:
    // dynamic
    // string? (or any nullable reference type)
    // (int X, int Y) (or any other tuple types using CS tuple syntax)
    // These types aren't directly represented in metadata, hence use the underlying type instead:
    // object for dynamic
    // string instead of string?
    // ValueTuple<int, int> instead of (int X, int Y)
    [GenericAttribute<string>()] // [GenericAttribute<string>(1, 2, 3, null, 5)] // requires  argument(s) or explicit default constructor if the constructor is overloaded
    public string Method() => "3. Generic attributes: [GenericAttribute<string>()]";

    // public GenericAttribute() { }
    // public GenericAttribute(int i, int j = 2, int k = 3, int?[] integerArray = null, params int[] parameterIntegerArray) { }
}
/******************************************************************************/


// 7. File-local types
/******************************************************************************/
file class FileLocalClass {}

file interface IFileLocalInterface {}

file delegate void FileLocalDelegate();

file record FileLocalRecord {}

file record struct FileLocalRecordStruct {}

file struct FileLocalStruct {}

file enum FileLocalEnum {}

// File-local classes can be attribute types
// Can be used as attributes within both file-local types and non-file-local types
file class FileLocalAttribute : Attribute { }

[FileLocalAttribute]
file class FileLocalAttributeClient
{
    public void Print()
    {
        Console.WriteLine("CS 11, File-local classes can be attribute types");
        Console.WriteLine("CS 11, File-local can be used as attributes within file-local types");
        var attribute = typeof(FileLocalAttributeClient).CustomAttributes.Where(x => x.AttributeType == typeof(FileLocalAttribute)).Last();
        Console.WriteLine(attribute);
    }
}

[FileLocalAttribute]
class AttributeClient
{
    public void Print()
    {
        Console.WriteLine("CS 11, File-local can be used as attributes within non-file-local types");
        var attribute = typeof(AttributeClient).CustomAttributes.Where(x => x.AttributeType == typeof(FileLocalAttribute)).Last();
        Console.WriteLine(attribute);
    }
}

file class FileLocalBase { }

file class FileLocalDerived : FileLocalBase { }

// Error CS9053: File-local type 'FileLocalBase' cannot be used as a base type of non-file-local type 'DerivedType'.
// class DerivedType : FileLocalBase { }

file interface IFileLocal
{
    void Method(IFileLocal localFile);
}

// file class FileLocalExplicitImplementation : IFileLocal // Fine // file-local type declarations can implement interfaces, override virtual methods, etc.
// class FileLocalExplicitImplementation : IFileLocal
// {
//     Error CS9051: File-local type 'IFileLocal' cannot be used in a member signature in non-file-local type 'FileLocalExplicitImplementation'
//     void IFileLocal.Method(IFileLocal localFile) { }
// }

// file class FileLocalImplementation : IFileLocal // Fine // file-local type declarations can implement interfaces, override virtual methods, etc.
// class FileLocalImplementation : IFileLocal
// {
//     Error CS9051: File-local type 'IFileLocal' cannot be used in a member signature in non-file-local type 'FileLocalImplementation' 
//     public void Method(IFileLocal localFile) { }
// }

// Error CS9055: File-local type 'GlobalUsingStaticFileLocal' cannot be used in a 'global using static' directive
// file class GlobalUsingStaticFileLocal { }
/******************************************************************************/


// 8. Required members
/******************************************************************************/

// required can be volatile
// volatile cannot be readonly or const or local
// required cannot be readonly or const or local
// volatile can be static
// required cannot be static
// required cannot be less visible than the containing type

// volatile keyword
// Reference types including record
// Pointer types (in an unsafe context)
// Simple types (sbyte, byte, short, ushort, int, uint, char, float, bool)
// enum with base types (sbyte, byte, short, ushort, int, uint) although enum can be long and ulong 
// Generic parameters known to be reference types
// IntPtr, UIntPtr

// volatile keyword can be applied to instance/static field (in class, struct [excluding readonly], ref struct [excluding readonly], record [including positional record but not positional parameter] , record struct [excluding readonly])
// volatile keyword can be applied to static fields (in interface)
// NB
// struct with instance field initializers (with or without volatile keyword) must include an explicitly declared constructor

// required modifier (without intending hiding) can be applied to instance field/property (in class, struct [excluding readonly], ref struct [excluding readonly], record [including positional record but not positional parameter] , record struct [excluding readonly])
// required modifier is not valid in interface
// NB
// struct with instance field/property initializers (with or without required modifier) must include an explicitly declared constructor

class RquiredMembers
{
    // modifier 'required'
    // Valid in class, struct, record, record struct
    // Not valid in interface
    // required is not allowed to be applied to indexers
    // required cannot be combined with:
    // static
    // const
    // readonly // Required member must be settable // If the member is a field, it cannot be readonly // If the member is a property, it must have a setter or initer at least as accessible as the member's containing type
    // ref readonly
    // ref
    // fixed
    // required members must be at least as visible as their containing type

    public RquiredMembers() { }

    // SetsRequiredMembersAttribute
    // All constructors in a type with required members, or whose base type specifies required members, must have those members set by a consumer when that constructor is called.
    // In order to exempt constructors from this requirement, a constructor can be attributed with SetsRequiredMembersAttribute, which removes these requirements.
    // The constructor body is not validated to ensure that it definitely sets the required members of the type.

    // SetsRequiredMembersAttribute removes all requirements from a constructor, and those requirements are not checked for validity in any way.
    // NB: this is the escape hatch if inheriting from a type with an invalid required members list is necessary: mark the constructor of that type with SetsRequiredMembersAttribute, and no errors will be reported.

    // Warning
    // The SetsRequiredMembers disables the compiler's checks that all required members are initialized when an object is created.
    // Use it with caution.
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public RquiredMembers(string alpha, string beta) => (Alpha, Beta) = (alpha, beta);

    public required string? Alpha { get; init; }
    public required string? Beta { get; init; }

    public required int? NullableNumber { get; set; }
}

class ExtendedRquiredMembers : RquiredMembers
{
    public ExtendedRquiredMembers() : base() { }

    // A constructor that chains to another constructor annotated with the SetsRequiredMembers attribute, either this(), or base(), must also include the SetsRequiredMembers attribute.
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public ExtendedRquiredMembers(string alpha, string beta) : base(alpha, beta) { }

    public required double NonNullableNumber { get; set; }
}

abstract class AbstractRequiredMembers
{
    public required string? Field;

    // Init-only property or indexer can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor
    // required properties must have setters (set or init accessors) that are at least as visible as their containing types
    // NB
    // Cannot specify accessibility modifiers for both accessors of the property or indexer
    // The accessibility modifier of the (get/init/set) accessor must be more restrictive than the property or indexer
    public required string? Property { get; init; }

    public required abstract string? AbstractProperty { get; init; }
    public virtual required string? VirtualProperty { get; init; }

    // System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute can be added to constructors to inform the compiler that a constructor initializes all required members
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public AbstractRequiredMembers()
    {
        // Don't confuse required with non-nullable.
        // It's valid to set a required members to null or default.
        Field = default(string);
        Property = default(string);
        AbstractProperty = default(string);
        VirtualProperty = default(string);
    }
}

// A type with any required members may not be used as a type argument when the type parameter includes the new() constraint.
// The compiler can't enforce that all required members are initialized in the generic code.
class DerivedRequiredMembers : AbstractRequiredMembers
{
    // Derived classes can't hide a required member declared in the base class
    // public required string? Field;
    // public required string? Property { get; init; }

    // This constructor must add 'SetsRequiredMembers' because it chains to a constructor that has that attribute
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public DerivedRequiredMembers() { }

    // Derived types that override a required property must include the required modifier
    // Can be sealed override
    public override required string? AbstractProperty { get; init; }
    // Can be sealed override
    // Types are allowed to override required virtual properties.
    // NB: This is a general CS anti-pattern
    public override required string? VirtualProperty { get; init; }
}

interface IExplicit
{
    // modifier 'required' not valid in interface
    string? InstanceProperty { get; init; }
}

class ExplicitImplementation : IExplicit
{
    // Explicit interface implementations can't be marked as required
    // required string IExplicit.InstanceProperty { get; init; }
    string? IExplicit.InstanceProperty { get; init; }
    // interface implementations other than explicit can be marked as required
    // public required string? InstanceProperty { get; init; }
}

// For records, constructors are generated and for positional records, public properties are for the primary constructor parameters:

// For record types, SetsRequiredMembersAttribute will be emitted on the synthesized copy constructor of a record if the record type or any of its base types have required members.

// The required modifier isn't allowed on the declaration for positional parameters on a record. 
// For positional records, use a primary constructor to initialize positional properties.
// If any of those properties include the required modifier, the primary constructor adds the SetsRequiredMembers attribute.
// This indicates that the primary constructor initializes all required members.

// When you declare a primary constructor on a record, the compiler generates public properties for the primary constructor parameters.
// The primary constructor parameters to a record are referred to as positional parameters.
// The compiler creates positional properties that mirror the primary constructor or positional parameters.
// The compiler doesn't synthesize properties for primary constructor parameters on types that don't have the record modifier.
record RecordPositional(string Alpha, string Beta);

record RequiredRecord
{
    // You can add an explicit declaration for a positional property that does include the required modifier.
    public required string? Alpha { get; init; }
    public required string? Beta { get; init; }

    public RequiredRecord() { }

    // You can write your own constructor with the System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute attribute.
    // However, the compiler doesn't verify that these constructors do initialize all required members.
    // Rather, the attribute asserts to the compiler that the constructor does initialize all required members.

    // Copy constructors generated for record types have the SetsRequiredMembers attribute applied if any of the members are required.
    // Copy constructor
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public RequiredRecord(RequiredRecord recordRequired) => (Alpha, Beta) = (recordRequired.Alpha, recordRequired.Beta);

    // Instance constructor
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public RequiredRecord(string? alpha, string? beta) => (Alpha, Beta) = (alpha, beta);

    public override string ToString() => $"({Alpha}), ({Beta})";
}

record DerivedRecordRequired : RequiredRecord
{
    public DerivedRecordRequired() { }

    // Alternate copy constructor in a record calls the instance constructor of base record
    // Unlike alternate copy constructor in a class that calls its instance constructor, alternate copy constructor:in a record must call a copy constructor of the base, or a parameterless object constructor if the record inherits from object
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public DerivedRecordRequired(RequiredRecord recordRequired) : base(recordRequired.Alpha, recordRequired.Beta) { }
}

// A type with any required members may not be used as a type argument when the type parameter includes the new() constraint.
// The compiler can't enforce that all required members are initialized in the generic code.
// A type with a parameterless constructor that advertises a contract is not allowed to be substituted for a type parameter constrained to new(), as there is no way for the generic instantiation to ensure that the requirements are satisfied.
// NB
// new() constraint
// The type argument must have a public parameterless constructor.
// When used together with other constraints, the new() constraint must be specified last.
// The new() constraint can't be combined with the struct and unmanaged constraints.
class GenericConstarint<T> where T : new()
{
    T t = new();

    private void ShowType()
    {
        Console.WriteLine($"Type: {typeof(T)}");
    }

    private T GetObject()
    {
        return t;
    }

    public void Print()
    {
        ShowType();
        Console.WriteLine($"Object: {GetObject()}");

        // 'RquiredMembers' cannot satisfy the 'new()' constraint on parameter 'T' in the generic type or or method 'GenericConstarint<T>' because 'RquiredMembers' has required members.
        // GenericConstarint<RquiredMembers> genericConstarint = new GenericConstarint<RquiredMembers> ();
    }
}

struct RequiredStructure
{
    // set // init
    public required string? Alpha { get; set; }
    public required string? Beta { get; set; }

    public override string ToString() => $"({Alpha}), ({Beta})";
}

// It is an error to hide a required member, as that member can no longer be set by a consumer.
class RequiredCannotHide { public string? Alpha { get; set; } }

class RequiredUnhide : RequiredCannotHide { public required new string? Alpha { get; set; } }

// The compiler will issue a warning when Obsolete is applied to a required member of a type and:
// The type is not marked Obsolete, or
// Any constructor not attributed with SetsRequiredMembersAttribute is not marked Obsolete.
// [Obsolete]
class Deprecated
{
    [Obsolete]
    public required string? Alpha { get; init; }

    [Obsolete]
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Deprecated(string alpha) => Alpha = alpha;
}
/******************************************************************************/


// 9. Auto-default structs
/******************************************************************************/
readonly struct AutoDefaultStructure
{

    public AutoDefaultStructure(int autoImplementedProperty)
        => AutoImplementedProperty = autoImplementedProperty;

    public AutoDefaultStructure(int autoImplementedProperty, string autoImplementedPropertyFullyAssigned)
        => (AutoImplementedProperty, AutoImplementedPropertyFullyAssigned) = (autoImplementedProperty, autoImplementedPropertyFullyAssigned);

    public AutoDefaultStructure(string autoImplementedPropertyFullyAssigned)
        => AutoImplementedPropertyFullyAssigned = autoImplementedPropertyFullyAssigned;

    // Prior to CS 11, a constructor of a structure type must initialize all instance fields of the type.
    // Prior to CS 10, you can't declare a parameterless constructor.
    // Prior to CS 10, you can't initialize an instance field or property at its declaration.
    // Auto-default struct
    // All fields of a struct type are initialized to their default value as part of executing a constructor.
    // This change means any field or auto property not initialized by a constructor is automatically initialized by the compiler.
    // Structs where the constructor doesn't definitely assign all fields now compile, and any fields not explicitly initialized are set to their default value.
    // Pre-CS 11 // Error CS0843: Auto-implemented property 'AutoDefaultStructure.AutoImplementedProperty' must be fully assigned before control is returned to the caller
    public int AutoImplementedProperty { get; init; }
    public string AutoImplementedPropertyFullyAssigned { get; init; } = "Fully Assigned";

    public override string ToString() => $"({AutoImplementedProperty}), ({AutoImplementedPropertyFullyAssigned})";
}

struct StructurePreCS11
{
    int field;
    public int Number
    {
        get => field;
        set => field = field < value ? value : field;
    }

    // Pre-CS 11 // Error CS0188: The 'this' object cannot be used before all of its fields have been assigned
    public StructurePreCS11() // Error: backing field of 'StructurePriorToCS11.Number' is implicitly initialized to 'default'
    {
        Number = 1;
    }
}
/******************************************************************************/


// 10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string
/******************************************************************************/
class PatternMatchSpan
{
    public void Print()
    {
        string literal = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 1234567890~!@#$%^&*()_+|}{\":?><,./;'[]\\=-`12345123456789012345678901234567";

        Console.WriteLine("10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string: Span<char>");
        Span<char> spanRef = literal.ToCharArray();
        Span<char> sliceSpanRef = spanRef.Slice(63, 32);
        Console.WriteLine($"Literal: {literal}");
        Console.WriteLine($"PAttern: {sliceSpanRef.ToString()}");
        Console.WriteLine($"Pattern match Span<char>: {sliceSpanRef is "~!@#$%^&*()_+|}{\":?><,./;'[]\\=-`"}");

        Console.WriteLine("10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string: ReadOnlySpan<char>");
        ReadOnlySpan<char> spanRefReadOnly = literal.ToCharArray();
        ReadOnlySpan<char> sliceSpanRefReadOnly = spanRefReadOnly.Slice(63, 32);

        Console.WriteLine($"Pattern match ReadOnlySpan<char>: {sliceSpanRefReadOnly is "~!@#$%^&*()_+|}{\":?><,./;'[]\\=-`"}");
    }
}
/******************************************************************************/


// 11. Extended nameof scope
/******************************************************************************/
// CS 11
// nameof expression with a method parameter inside an attribute on a method or its parameter
class NameOfExpression
{
    // Works in CS 8 with .NET 3.1
    [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(url))]
    string? GetTopLevelDomainFromFullUrl(string? url)
    {
        string? host = string.Empty;
        if (!string.IsNullOrWhiteSpace(url))
        {
            Uri uniformResourceIdentifier = new Uri(url);

            host = uniformResourceIdentifier.Host;
        }
        return host;
    }

    // Works in CS 8 with .NET 3.1
    [ParameterString(nameof(msg))]
    public void Method(string? msg)
    {
        // 'local function attributes'  // CS 9+
        LocalFunction<string>(msg ??= string.Empty);

        [ParameterString(nameof(T))]
        void LocalFunction<T>(T param)
        {
            Console.WriteLine(GetTopLevelDomainFromFullUrl(msg));
        }

        // 'lambda attributes' // CS 10+
        // 'inferred delegate type' // CS 10+
        var lambdaExpression = ([ParameterString(nameof(aNumber))] int aNumber) => aNumber.ToString();
    }
}

// Custom Attribute
class ParameterStringAttribute : Attribute
{
    public ParameterStringAttribute(string msg) { }
}
/******************************************************************************/


// 13. ref fields and scoped ref
/******************************************************************************/

// For readonly ref struct
// NB
// Unless declared readonly, instance fields are not valid in readonly ref struct
// Unless declared readonly, ref fields are not valid in readonly ref struct 
// Unless declared readonly, ref readonly fields are not valid in readonly ref struct

ref struct RefStruct
{
    // NB
    // Unless declared readonly, instance fields are not valid in readonly ref struct
    string? text = "ref field init/set";
    int? number = 555;

    string? literal = "readonly ref field init/set";
    int? numeral = 777;

    /*
    ref string? refAssignerInConstructor;
    ref int? refAssignerInConstructorNumber;
    ref string? refAssignerInMethod;
    ref int? refAssignerInMethodNumber;
    */

    /*
    // NB
    // static constructor // static readonly
    // A static readonly field can be used as a ref or out value (only in a static constructor)
    // An object reference is required for the non-static field to be assigned (in a static constructor)
    static string? staticAssignerInConstructor = "static (= ref) assignment in constructor";
    static int? staticAssignerInConstructorNumber = int.MaxValue;
    */
    
    static string? staticAssignerAtDeclaration = "static (= ref) assignment at declaration";
    static int? staticAssignerAtDeclarationNumber = int.MaxValue;

    static string? staticAssignerInMethod = "static (= ref) assignment in method";
    static int? staticAssignerNumberInMethod = 111;

    // CS 11
    //  A ref field can only be declared in a ref struct.
    // The compiler ensures that a reference stored in a ref field doesn't outlive its referent.
    // The ref fields feature enables a safe implementation of types like System.Span<T>:
    // The modifier 'static' is not valid for ref field

    // Can be (= ref) assigned at declaration (only with static member)
    // Can be (= ref) assigned in constructor (with static/ref member)
    // Can be (=) assigned in init/set accessor
    // Can be (= ref) assigned in method (with static/ref member) 
    // Can be (=) assigned in method
    // NB
    // Can be passed as argument and also as in, ref, out, params
    // Can be passed to optional parameter and also as named argument
    // Unless declared readonly, ref fields are not valid in readonly ref struct
    ref string? refField;
    ref int? refFieldNumber;

    // Can be (= ref) assigned at declaration (only with static member)
    // Can be (= ref) assigned in constructor (with static/ref member)
    // Can be (=) assigned in init/set accessor
    // CANNOT be (= ref) assigned in method
    // Can be (=) assigned in method
    // NB
    // Can be passed as argument and also as in, ref, out, params
    // Can be passed to optional parameter and also as named argument
    readonly ref string? readonlyRefField = ref staticAssignerAtDeclaration;
    readonly ref int? readonlyRefFieldNumber = ref staticAssignerAtDeclarationNumber;

    // Can be (= ref) assigned at declaration (only with static member)
    // Can be (= ref) assigned in constructor (with static/ref member)
    // CANNOT be (=) assigned in accessor
    // Can be (= ref) assigned in method (with static/ref member) 
    // CANNOT be (=) assigned in method
    // NB
    // Can be passed as argument and also as in, params (except ref and out)
    // Can be passed to optional parameter and also as named argument
    // Unless declared readonly, ref readonly fields are not valid in readonly ref struct
    ref readonly string? refReadonlyField = ref staticAssignerAtDeclaration;
    ref readonly int? refReadonlyFieldNumber = ref staticAssignerAtDeclarationNumber;

    // Can be (= ref) assigned at declaration (only with static member)
    // Can be (= ref) assigned in constructor (with static/ref member)
    // CANNOT be (=) assigned in accessor
    // CANNOT be (= ref) assigned in method
    // CANNOT be (=) assigned in method
    // NB
    // Can be passed as argument and also as in, params (except ref and out)
    // Can be passed to optional parameter and also as named argument
    readonly ref readonly string? readonlyRefReadonlyField = ref staticAssignerAtDeclaration;
    readonly ref readonly int? readonlyRefReadonlyFieldNumber = ref staticAssignerAtDeclarationNumber;

    // static  RefStruct() { }
    public RefStruct()
    {
        // Valid ref assignments with ref field
        /*
        refField = ref refAssignerInConstructor;
        refFieldNumber = ref refAssignerInConstructorNumber;

        readonlyRefField = ref refAssignerInConstructor;
        readonlyRefFieldNumber = ref refAssignerInConstructorNumber;

        refReadonlyField = ref refAssignerInConstructor;
        refReadonlyFieldNumber = ref refAssignerInConstructorNumber;

        readonlyRefReadonlyField = ref refAssignerInConstructor;
        readonlyRefReadonlyFieldNumber = ref refAssignerInConstructorNumber;
        */

        // Valid ref assignments with static field
        /*
        refField = ref staticAssignerInConstructor;
        refFieldNumber = ref staticAssignerInConstructorNumber;

        readonlyRefField = ref staticAssignerInConstructor;
        readonlyRefFieldNumber = ref staticAssignerInConstructorNumber;

        refReadonlyField = ref staticAssignerInConstructor;
        refReadonlyFieldNumber = ref staticAssignerInConstructorNumber;

        readonlyRefReadonlyField = ref staticAssignerInConstructor;
        readonlyRefReadonlyFieldNumber = ref staticAssignerInConstructorNumber;
        */
    }

    // init // set
    public string? InitRef
    {
        get => text;
        init => refField = text;
        // set => reFField = text;
    }
    public int? InitRefNumber
    {
        get => number;
        init => refFieldNumber = number;
         // set => refFieldNumber= number;
    }

    // NB
    // Properties which return by reference cannot have init/set accessors
    /*
    public ref string? GetRef
    {
        get => ref refField;
    }
    public ref int? GetRefNumber
    {
        get => ref refFieldNumber;
    }
    */

    public string? InitReadonlyRef
    {
        get => literal;
        init =>  readonlyRefField = literal;
        // set => readonlyRefField = literal;
    }
    public int? InitReadonlyRefNumber
    {
        get => numeral;
        init =>  readonlyRefFieldNumber = numeral;
        // set => readonlyRefFieldNumber = numeral;
    }
    
    private void Scope(in string? inText, ref string? refText, out string? outText, string? text, params string?[] textArray)
    {
        outText = string.Empty;
    }
   
    private void Scope(in int? inNumber, ref int? refNumber, out int? outNumber, int? number, params int?[] numberArray)
    {
        outNumber = 0;
    }

    public void Print()
    {
        // A ref field may have the null value.
        // Use the Unsafe.IsNullRef<T>(T) method to determine if a ref field is null.
        // Determines if a given managed pointer to a value of type T is a null reference.
        var isNull = System.Runtime.CompilerServices.Unsafe.IsNullRef<string?>(ref refField);
        Console.WriteLine($"System.Runtime.CompilerServices.Unsafe.IsNullRef<string?>(ref refField): {isNull}");
        isNull = System.Runtime.CompilerServices.Unsafe.IsNullRef<int?>(ref refFieldNumber);
        Console.WriteLine($"System.Runtime.CompilerServices.Unsafe.IsNullRef<int?>(ref refFieldNumber): {isNull}");

        // System.NullReferenceException: "Object reference not set to an instance of an object."
        // isNull = string.IsNullOrEmpty(reFField);
        // System.NullReferenceException: "Object reference not set to an instance of an object."
        // isNull = (reFField is null);
        // System.NullReferenceException: "Object reference not set to an instance of an object."
        // isNull = (reFFieldNumber is null);

        Console.WriteLine(InitRef);
        Console.WriteLine(InitRefNumber);
        Console.WriteLine(InitReadonlyRef);
        Console.WriteLine(InitReadonlyRefNumber);

        // System.NullReferenceException: "Object reference not set to an instance of an object."
        // Console.WriteLine(GetRef);
        // Console.WriteLine(GetRefNumber);

        // Valid/Invalid ref assignments with ref field
        /*
        // Can be (= ref) assigned in method (with static/ref member) 
        refField = ref refAssignerInMethod;
        refFieldNumber = ref refAssignerInMethodNumber;

        // CANNOT be (= ref) assigned in method
        // readonlyRefField = ref refAssignerInMethod;
        // readonlyRefFieldNumber = ref refAssignerInMethodNumber;

        // Can be (= ref) assigned in method (with static/ref member) 
        refReadonlyField = ref refAssignerInMethod;
        refReadonlyFieldNumber = ref refAssignerInMethodNumber;

        // CANNOT be (= ref) assigned in method
        // readonlyRefReadonlyField = ref refAssignerInMethod;
        // readonlyRefReadonlyFieldNumber = ref refAssignerInMethodNumber;
        */

        // Valid/Invalid assignments with static field
        /**********************************************************************/
        // Can be (= ref) assigned in method (with static/ref member) 
        refField =  ref staticAssignerInMethod;
        refFieldNumber = ref staticAssignerNumberInMethod;
        Console.WriteLine(refField); // ref field local (=) assingment
        Console.WriteLine(refFieldNumber); // 222
        // Can be (=) assigned in method
        refField = "ref field local (=) assingment"; //
        refFieldNumber = 222; //

        // CANNOT be (= ref) assigned in method
        // readonlyRefField = ref staticAssignerInMethod;
        // readonlyRefFieldNumber = ref staticAssignerNumberInMethod;
        Console.WriteLine(readonlyRefField); // readonly ref field local (=) assingment
        Console.WriteLine(readonlyRefFieldNumber); // 333
        // Can be (=) assigned in method
        readonlyRefField = "readonly ref field local (=) assingment"; //
        readonlyRefFieldNumber = 333; //

        // Can be (= ref) assigned in method (with static/ref member) 
        refReadonlyField = ref staticAssignerInMethod;
        refReadonlyFieldNumber = ref staticAssignerNumberInMethod;
        Console.WriteLine(refReadonlyField);
        Console.WriteLine(refReadonlyFieldNumber);
        // CANNOT be (=) assigned in method
        // refReadonlyField = string.Empty;
        // refReadonlyFieldNumber = 0;

        // CANNOT be (= ref) assigned in method
        // readonlyRefReadonlyField = ref staticAssignerInMethod;
        // readonlyRefReadonlyFieldNumber = ref staticAssignerNumberInMethod;
        Console.WriteLine(readonlyRefReadonlyField);
        Console.WriteLine(readonlyRefReadonlyFieldNumber);
        // CANNOT be (=) assigned in method
        // readonlyRefReadonlyField = string.Empty;
        // readonlyRefReadonlyFieldNumber = 0;

        Scope(in refField, ref refField, out refField, refField, refField);
        Scope(in readonlyRefField, ref readonlyRefField, out readonlyRefField, readonlyRefField, readonlyRefField);
        // ref readonly and readonly ref readonly cannot be used as a ref or out value
        // Scope(in refReadonlyField, ref refReadonlyField, out refReadonlyField, refReadonlyField, refReadonlyField);
        // Scope(in readonlyRefReadonlyField, ref readonlyRefReadonlyField, out readonlyRefReadonlyField, readonlyRefReadonlyField, readonlyRefReadonlyField);

        Scope(in refFieldNumber, ref refFieldNumber, out refFieldNumber, refFieldNumber, refFieldNumber);
        Scope(in readonlyRefFieldNumber, ref readonlyRefFieldNumber, out readonlyRefFieldNumber, readonlyRefFieldNumber, readonlyRefFieldNumber);
        // ref readonly and readonly ref readonly cannot be used as a ref or out value
        // Scope(in refReadonlyFieldNumber, ref refReadonlyFieldNumber, out refReadonlyFieldNumber, refReadonlyFieldNumber, refReadonlyFieldNumber);
        // Scope(in readonlyRefReadonlyFieldNumber, ref readonlyRefReadonlyFieldNumber, out readonlyRefReadonlyFieldNumber, readonlyRefReadonlyFieldNumber, readonlyRefReadonlyFieldNumber);
        /**********************************************************************/
    }
}

// readonly ref struct StructRef
ref struct StructRef
{
    // CAN return by reference even if refField is readonly ref, however not if refField is ref readonly or readonly ref readonly
    // public readonly ref string? refField;
    public ref string? refField;

    // CAN return by reference if it is a static member, however not if it is a static readonly member
    // public static string? staticField;

    public string? instanceField;

    public StructRef() { }
}

/*
// interface ReferenceType // only static field
// static class ReferenceType // only static member
// record ReferenceType
// record ReferenceType() // positional record
// abstract class ReferenceType // to be derived for instance member
class ReferenceType
{
    public static string? staticReferenceType;
    public string? instanceReferenceType;
}

// readonly struct ValueType
ref struct ValueType
// readonly ref struct ValueType
// record struct ValueType
// struct ValueType
{
    public static string? staticValueType;
    // readonly struct // readonly ref struct
    // public readonly string? instanceValueType;
    // public string? instanceValueType;

    // public ref string? refValueType;
    // public readonly ref string? refValueType;
    // public ref readonly string? refValueType;
    // public readonly ref readonly string? refValueType;
}
*/

class ScopedRef
{
    // The contextual keyword scoped restricts the lifetime of a value. 
    // The scoped modifier restricts the ref-safe-to-escape or safe-to-escape lifetime, respectively, to the current method.
    // Effectively, adding the scoped modifier asserts that your code won't extend the lifetime of the variable.

    // The 'scoped' modifier can be used for refs and ref struct values only.

    // You can apply scoped to a parameter or local variable.
    // The scoped modifier may be applied to parameters and locals when the type is a ref struct.
    // Otherwise, the scoped modifier may be applied only to local reference variables[*] (refs and ref struct values only).
    // *[When you declare a local variable and add the ref keyword before the variable's type, you declare a reference variable, or a ref local.]
    // That includes local variables declared with the ref modifier and parameters declared with the in, ref or out modifiers.
    public ref string? Method(string? text, scoped in string? scopedInText, scoped ref string? scopedRefText, scoped out string? scopedOutText)
    {
        // Cannot return a parameter by reference 'text' because it is not a ref parameter
        text = string.Empty;

        // Cannot assign to variable 'scopedInText' or use it as the right hand side of a ref assignment because it is a readonly variable
        // scopedInText = string.Empty;

        scopedRefText = string.Empty;

        // The out parameter 'scopedOutText' must be assigned to before control leaves the current method
        scopedOutText = string.Empty;

        StructRef refStruct = new();

        // The left-hand side of a ref assignment must be a ref variable.
        // text = ref refStruct.refField;

        // Cannot return variable 'scopedInText' by writable reference because it is a readonly variable 
        scopedInText = ref refStruct.refField;

        // Cannot return a parameter by reference 'scopedRefText' because it is scoped to the current method
        scopedRefText = ref refStruct.refField;

        // Cannot return a parameter by reference 'scopedOutText' because it is scoped to the current method
        scopedOutText = ref refStruct.refField;

        // Cannot return 'scopedRefLocal' by reference because it was initialized to a value that cannot be returned by reference
        scoped ref string? scopedRefLocal = ref refStruct.refField;

        // Cannot return // An expression cannot be used in this context because it may not be passed or returned by reference
        ref readonly string? refReadonlyLocal = ref refStruct.refField;


        // The scoped modifier may be applied to parameters and locals when the type is a ref struct. 
        // Cannot return local 'scopedRefStruct' by reference because it is not a ref local
        scoped StructRef scopedStruct = refStruct;

        // Cannot return 'scopedRefStruct' by reference because it was initialized to a value that cannot be returned by reference (for return type ref StructRef)
        scoped ref StructRef scopedRefStruct = ref refStruct;

        // CAN return by reference even if refStruct.refField is readonly ref, however not if refStruct.refField is ref readonly or readonly ref readonly (for return type ref StructRef)
        ref string? refLocal = ref refStruct.refField;
        // CAN return by reference if it is a static member, however not if it is a static readonly member
        // ref string? refLocal = ref StructRef.staticField;
        // CAN return by reference static member of any type, however not if it is a static readonly member
        // ref string? refLocal = ref ReferenceType.staticReferenceType;
        // ref string? refLocal = ref ValueType.staticValueType;
        // CAN return by reference instance member of reference type
        // ref string? refLocal = ref new ReferenceType().instanceReferenceType;

        // CAN return by reference ref member of value type (ref struct/readonly ref struct) except ref readonly member and readonly ref readonly member
        // ref string? refLocal = new ValueType().refValueType;

        // NB
        // instance member of value type can be (=) assigned only after being (= ref) assigned, before return by reference
        // refLocal = new StructRef().instanceField;
        // refLocal = new ValueType().instanceValueType;
        // ref readonly and readonly ref readonly member of value type (ref struct/readonly ref struct) can be (=) assigned only after being (= ref) assigned, before return by reference
        // refLocal = new ValueType().refValueType;

        // By-reference returns may only be used in methods that return by reference
        // By-value returns may only be used in methods that return by value
        return ref refLocal; // return ref refStruct.refField; // return ref StructRef.staticField; // return ref ReferenceType.staticReferenceType; // return ref new ReferenceType().instanceReferenceType; // return ref ValueType.staticValueType;
    }
}

class ClassField
{
    public int field = 0;
}

struct NonRefStrucField
{
    public int field = 0;
    public NonRefStrucField() { }
}

ref struct RefStrucField
{
    public ref int field;
}

class Ref_safe_to_escapeScope
{
    // The ref_safe_to_escape scope defines the scope where a reference to any expression can be safely accessed or modified.
    // The ref_safe_to_escape scope defines when a variable can be ref assigned or ref reassigned. 
    // NB
    // The safe_to_escape scope defines the scope where any expression can be safely accessed.
    // The safe_to_escape scope defines when a variable can be assigned or reassigned.

    // The following lists the ref safe to escape scopes for variable types[*].
    // *[ref fields can't be declared in a class or a non-ref struct, so those aren't in the list (Hence, refer to ref struct Safe_to_escapeScopeAndRefStructs)]
    /*
    Declaration-----------------ref safe to escape scope
    non-ref local---------------block where local is declared
    non-ref parameter-----------current method
    ref, in parameter-----------calling method
    out parameter---------------current method
    class field-----------------calling method
    non-ref struct field--------current method
    ref field of ref struct-----calling method 
    */

    /*
    private int privateField;
    */

    public ref int EscapeOrNot(int nonRefParameter, ref int refParameter, in int inParameter, out int outParameter)
    {
        // The out parameter 'outParameter' must be assigned to before control leaves the current method 
        outParameter = 0;

        // non-ref local // block where local is declared // Can't escape
        /*
        int nonRefLocal = 1;
        // Cannot return local 'nonRefLocal' by reference because it is not a ref local 
        return ref nonRefLocal; // Error: nonRefLocal's ref safe to escape scope is the body of EscapeOrNot
        */

        // non-ref parameter // current method // Can't escape
        /*
        // Cannot return a parameter by reference 'nonRefParameter' because it is not a ref parameter 
        return ref nonRefParameter;
        */

        // ref parameter // calling method
        /*
        // CAN ref return a ref parameter // Can escape
        return ref refParameter;
        */

        // in parameter // calling method // Can't escape
        /*
        // Cannot return variable 'inParameter' by writable reference because it is a readonly variable 
        return ref inParameter;
        */

        // out parameter // current method // Can't escape
        /*
        // Cannot return a parameter by reference 'outParameter' because it is scoped to the current method
        return ref outParameter;
        */

        // class field // calling method // Can escape
        /*
        // CAN ref return a class field
        return ref new ClassField().field;
        */
        /*
        // CAN ref return a class field // Can escape
        return ref this.privateField;
        */

        // non-ref struct field // current method // Can't escape
        /*
        // An expression cannot be used in this context because it may not be passed or returned by reference
        return ref new NonRefStrucField().field;
        */

        // ref field of ref struct // calling method
        // CAN ref return a ref field of ref struct // Can escape
        return ref new RefStrucField().field; 
    }
}

// readonly ref struct Safe_to_escapeScopeAndRefStructs
ref struct Safe_to_escapeScopeAndRefStructs
{
    // ref struct types require more rules to ensure they can be used safely.
    // A ref struct type may include ref fields.
    // That requires the introduction of a safe to escape scope.
    // For most types, the safe to escape scope is the calling method.
    // In other words, a value that's not a ref struct can always be returned from a method.

    // private readonly string text = "Safe to escape scope and ref structs";
    private string text = "Safe to escape scope and ref structs";

    public Safe_to_escapeScopeAndRefStructs() { }

    public ReadOnlySpan<char> Safe()
    {
        var span = text.AsSpan();
        return span;
    }

    public Span<int> EscapeOrNot()
    {
        int length = 10;
        // Cant escape because of stackalloc (stack allocated array of integers)
        // Span<int> numbers = stackalloc int[length]; // Can't escape
        Span<int> numbers = new int[length]; // Can escape
        for (var i = 0; i < length; i++)
        {
            numbers[i] = i;
        }
        return numbers; // Error! numbers can't escape if stack allocated
    }
}

// NB
// CS 7 ref locals and returns
// ref return
// CS 7.2
// ref readonly
// returned by reference because the returned value is a static member
struct Point3D
{
    public Point3D(int i, int j, int k) { }
    private static Point3D origin = new Point3D(0, 0, 0);
    // Dangerous! returning a mutable reference to internal storage
    // By-value returns may only be used in methods that return by value.
    // public ref Point3D Origin => ref origin;
    // You don't want callers modifying the origin, so you should return the value by readonly ref:
    public ref readonly Point3D Origin => ref origin; // CS 7 property ref return of static member
}
// CS 11 // ref fields
// ref field in ref struct
// readonly ref struct RefStructure
ref struct RefStructure
{
    // ref field 
    static string staticString = "ref field assignment at declaration with static member only in CS 11";
    // readonly ref struct
    // readonly ref string refString = ref staticString; // Not to be confused with CS 7 property ref return of static member
    ref string refString = ref staticString;
    // A 'struct' with field initializers must include an explicitly declared constructor. 
    public RefStructure() { }
}
/******************************************************************************/


// 14. Improved method group conversion to delegate
/******************************************************************************/
delegate void MethodGroup();

class ImprovedMethodGroupConversionToDelegate
{
    private void Method() { }

    public void Print()
    {
        Console.WriteLine("14. Improved method group conversion to delegate");
        Console.WriteLine("CS 11: The compiler may cache the delegate object created from a method group.");
        Console.WriteLine("Pre-CS 11: Use a lambda expression to reuse a single delegate object.");

        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i =0; i < 1000000; i++ )
        {
            // CS 11 .NET 7 // Compare this with CS 10 .NET 6
            // The compiler may cache the delegate object created from a method group.
            MethodGroup groupMethod = Method;
        }
        stopwatch.Stop();
        Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed}");

        System.Diagnostics.Stopwatch stopwatchLambda = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000000; i++)
        {
            // Pre-CS 11
            // Use a lambda expression to reuse a single delegate object.
            MethodGroup groupMethodLambda = () => Method();
        }
        stopwatchLambda.Stop();
        Console.WriteLine($"Elapsed Time Lambda: {stopwatchLambda.Elapsed}");
    }
}
/******************************************************************************/


// 15. Warning wave 7
/******************************************************************************/
// Warning CS8981: The type name 'underscore' only contains lower-cased ascii characters. Such names may become reserved for the language. (CS8981)
// public class underscore { }
// Address this warning by renaming the type to include at least one non-lower case ASCII character, such as an upper case character, a digit, or an underscore.
public class Underscore { }
/******************************************************************************/


// msys64-mingw64-gcc.c
/*
// msys64-mingw64-gcc.c
// gcc -c -fPIC msys64-mingw64-gcc.c
// gcc -shared -o msys64-mingw64-gcc.so msys64-mingw64-gcc.o
#include <stdio.h>
int
c_code(int number) {
  return number * 10;
}
*/
// msys64-mingw64-gcc.c compilation
/*
Last login: Tue Sep 12 18:28:16 on ttys097
rajaniapple@Rajanis-MacBook-Pro ~ % arch
arm64
rajaniapple@Rajanis-MacBook-Pro ~ % gcc --version
Apple clang version 14.0.3 (clang-1403.0.22.14.1)
Target: arm64-apple-darwin22.6.0
Thread model: posix
InstalledDir: /Library/Developer/CommandLineTools/usr/bin
rajaniapple@Rajanis-MacBook-Pro ~ % cd /Users/rajaniapple/Desktop/Working/CS/CS11/CS11/GCC/C
rajaniapple@Rajanis-MacBook-Pro C % gcc -c -fPIC msys64-mingw64-gcc.c
rajaniapple@Rajanis-MacBook-Pro C % gcc -shared -o msys64-mingw64-gcc.so msys64-mingw64-gcc.o
rajaniapple@Rajanis-MacBook-Pro C % 
*/


// FileA.cs
/*
// 7.File - local types
namespace FileLocalNamespace;

file class FileLocal
{
    public string Method() => "Method in FileA";
}
*/

// FileB.cs
/*
// 7.File - local types
namespace FileLocalNamespace;

file class FileLocal
{
    public string Method() => "Method in FileB";
}


class FileLocalClient
{
    public void Print()
    {
        Console.WriteLine(new FileLocal().Method());
    }
}
*/

// FileC.cs
/*
// 7.File - local types
namespace FileLocalCNamespace
{
    file class FileLocalC
    {
        public string Method() => "Method in FileC";
    }
}

namespace FileLocalCClientNamespace
{
    class FileLocalCClient
    {
        public void Print()
        {
            // Console.WriteLine(new FileLocalC().Method()); // Error CS0246: The type or namespace name 'FileLocalC' could not be found
            Console.WriteLine(new FileLocalCNamespace.FileLocalC().Method()); // Fine
        }
    }
}
*/

// FileD.cs
/*
// 7.File - local types
namespace FileLocalCNamespace
{
    class FileLocalCClient
    {
        public void Print()
        {
            // Console.WriteLine(new FileLocalC().Method()); // Error CS0246: The type or namespace name 'FileLocalC' could not be found
            // Console.WriteLine(new FileLocalCNamespace.FileLocalC().Method()); // Error CS0234: The type or namespace name 'FileLocalC' does not exist in the namespace 'FileLocalCNamespace'
        }
    }
}
*/


// Output
/*
Environment.OSVersion: Unix 14.4.1
Environment.OSVersion.Platform: Unix
Environment.OSVersion.Version: 14.4.1
Environment.OSVersion.VersionString: Unix 14.4.1
Environment.OSVersion.Version.Major: 14
Environment.OSVersion.Version.Minor: 4
Environment.Version: 7.0.15
Environment.Is64BitOperatingSystem: True
Environment.Is64BitProcess: True
RuntimeInformation.FrameworkDescription: .NET 7.0.15
RuntimeInformation.ProcessArchitecture: Arm64
RuntimeInformation.OSArchitecture: Arm64
RuntimeInformation.OSDescription): Darwin 23.4.0 Darwin Kernel Version 23.4.0: Fri Mar 15 00:10:42 PDT 2024; root:xnu-10063.101.17~1/RELEASE_ARM64_T6000
RuntimeInformation.RuntimeIdentifier: osx.14-arm64

1. Raw string literals
Starts with at least three double-quotes
"without requiring escape sequences"
Ends with the same number of double-quote
1. Raw string literals: Interpolated raw string literals
Pair of numbers: "1, 2"
Pair of numbers: {1, 2}

2. Generic math support: static abstract in interface
static abstract method implicitly implemented in the implementing type
2. Generic math support: static virtual in interface
static abstract method implicitly implemented in the implementing type
static virtual method implicitly implemented in the implementing type
2. Generic math support: static abstract operator in interface
nameof: implementationOperator
GetType: OperatorImplementation
typeof: OperatorImplementation
typeof GetType: System.RuntimeType
IsValueType: False
overloading unary operator ++
A
B
C
D
E
F
G
H
I
J
K
L
M
N
O
P
Q
R
S
T
U
V
W
X
Y
Z
overloading unary operator --
z
y
x
w
v
u
t
s
r
q
p
o
n
m
l
k
j
i
h
g
f
e
d
c
b
a
2. Generic math support: static extern in interface
1234567890
2. Generic math support: arithmetic shift and logical shift
x = -8, hex: fffffff8, binary: 11111111111111111111111111111000
arithmetic shift: >> 2
y = x >> 2 = -2, hex: fffffffe, binary: 11111111111111111111111111111110
logical shift: >>> 2
z = x >>> 2 = 1073741822, hex: 3ffffffe, binary: 00111111111111111111111111111110
rightShiftNegative = negative >> 2 = -2, hex: fffffffe, binary: 11111111111111111111111111111110
rightShiftPositive = positive >> 2 = 2, hex:        2, binary:                               10
leftShiftNegative = negative << 2 = -32, hex: ffffffe0, binary: 11111111111111111111111111100000
leftShiftPositive = positive << 2 = 32, hex:       20, binary:                           100000
2. Generic math support: relaxed shift operator
a = x >> GenericMath.Add<int>(2, 4) = -1, hex: ffffffff, binary: 11111111111111111111111111111111
2. Generic math support: checked and unchecked user defined operators
Operator Overload 1:
x = 2147483647, y = 2147483647
Operator Overload 2:
x = 10, y = 20
Operator Overload 3:
x = 0, y = 0
Adding objects operatorOverload3 = operatorOverload1 + operatorOverload2:
x = -2147483639, y = -2147483629
Adding objects operatorOverload3 = operatorOverload1 + operatorOverload2:
Arithmetic operation resulted in an overflow.

3. Generic attributes: [GenericAttribute<string>()]

4. UTF-8 string literals
80, 79, 83, 84, 32
80, 79, 83, 84, 32
80, 79, 83, 84, 32

5. Newlines in string interpolation expressions
Academic grading for 47 is Grade: F, GPA: 0.0.

6. List patterns
True
False
False
False
True
6. List patterns: discard pattern (_), range pattern (..)
1
False
True
False
2
True
True
True
False
3
False
True
True
4
False
True
True
True
5
True
True
True
False
6
True
True
True
7
True
True
True
True
8
False
True
True
9
False
False
True
10
True
True
False
True
6. List patterns: nest a subpattern within a slice pattern
Pattern Alpha matches; middle chunk is lph.
Pattern Alphabet doesn't match.
Nope
Yep

7. File-local types
CS 11, File-local keyword 'file' is type modifier
CS 11, File-local cannot use access modifier
CS 11, File-local default access modifier is internal
Method in FileB
Method in FileC
CS 11, File-local classes can be attribute types
CS 11, File-local can be used as attributes within file-local types
[<Program>F28B8BBC01AF0711B9E45E6A429E3513BE3150ACD44286AFC6B471BF1E7277565__FileLocalAttribute()]
CS 11, File-local can be used as attributes within non-file-local types
[<Program>F28B8BBC01AF0711B9E45E6A429E3513BE3150ACD44286AFC6B471BF1E7277565__FileLocalAttribute()]

8. Required members
foo, bar, 555
baz, qux, 777
(quux), (corge)
(grault), (garply)
(), ()
(waldo), (fred)

9. Auto-default structs
(1), (Fully Assigned)
(0), ()
(0), ()

10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string
10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string: Span<char>
Literal: ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 1234567890~!@#$%^&*()_+|}{":?><,./;'[]\=-`12345123456789012345678901234567
PAttern: ~!@#$%^&*()_+|}{":?><,./;'[]\=-`
Pattern match Span<char>: True
10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string: ReadOnlySpan<char>
Pattern match ReadOnlySpan<char>: True

11. Extended nameof scope
dotnet.microsoft.com

12. Numeric IntPtr and UIntPtr: nint and nuint types now alias System.IntPtr and System.UIntPtr, respectively
System.IntPtr.Size: 8, System.IntPtr.Size: 8
nint.Size: 8, nuint.Size: 8

13. ref fields and scoped ref
System.Runtime.CompilerServices.Unsafe.IsNullRef<string?>(ref refField): True
System.Runtime.CompilerServices.Unsafe.IsNullRef<int?>(ref refFieldNumber): True
ref field init/set
555
readonly ref field init/set
777
static (= ref) assignment in method
111
static (= ref) assignment at declaration
2147483647
ref field local (=) assingment
222
readonly ref field local (=) assingment
333

14. Improved method group conversion to delegate
CS 11: The compiler may cache the delegate object created from a method group.
Pre-CS 11: Use a lambda expression to reuse a single delegate object.
Elapsed Time: 00:00:00.0325556
Elapsed Time Lambda: 00:00:00.0064729

15. Warning wave 7
Any new keywords added for C# will be all lower-case ASCII characters. This warning ensures that none of your types conflict with future keywords.
Address this warning by renaming the type to include at least one non-lower case ASCII character, such as an upper casecharacter, a digit, or an underscore
*/


// Credits:
/*
https://dotnet.microsoft.com/
https://gcc.gnu.org/
*/