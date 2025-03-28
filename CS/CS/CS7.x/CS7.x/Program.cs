// CS 7.1
/*
1. async Main method
// The entry point for an application can have the async modifier.
2. default literal expressions
// You can use default literal expressions in default value expressions when the target type can be inferred.
3. Inferred tuple element names
// The names of tuple elements can be inferred from tuple initialization in many cases.
// Finally, the compiler has two options /refout and /refonly that control reference assembly generation.
4. Reference assembly generation
// There are two new compiler options that generate reference-only assemblies: /refout and /refonly. 
// /refout a
// The -refout option specifies a file path where the reference assembly should be output.
// This translates to metadataPeStream in the Emit API.
// /refonly
// The -refonly option indicates that a reference assembly should be output instead of an implementation assembly, as the primary output.
// The -refonly parameter silently disables outputting PDBs, as reference assemblies cannot be executed.
*/

// CS 7.2
/*
5. Techniques for writing safe efficient code
// A combination of syntax improvements that enable working with value types using reference semantics.
6. Non-trailing named arguments
// Named arguments can be followed by positional arguments.
7. Leading underscores in numeric literals
// Numeric literals can now have leading underscores before any printed digits.
8. private protected access modifier
// The private protected access modifier enables access for derived classes in the same assembly. // Not in different assembly.
*/

// CS 7.3
/*
// New features
9. Access fixed fields without pinning.
10. Reassign ref local variables.
11. Initializers on stackalloc arrays.
12. Fixed statements with any type that supports a pattern.
13. Additional generic constraints. // Unmanaged constraint // Delegate constraints // Enum constraints
// Enhancements
14. Test == and != with tuple types.
15. Expression variables in more locations.
16. Attach attributes to the backing field of auto-implemented properties.
17. Method resolution when arguments differ by in has been improved.
18. Overload resolution now has fewer ambiguous cases.
// New compiler options
19. -publicsign and -pathmap
// -publicsign to enable Open Source Software (OSS) signing of assemblies.
// The -publicsign compiler option instructs the compiler to sign the assembly using a public key. 
// The assembly is marked as signed, but the signature is taken from the public key. 
// This option enables you to build signed assemblies from open-source projects using a public key.
// -pathmap to provide a mapping for source directories.
// The -pathmap compiler option instructs the compiler to replace source paths from the build environment with mapped source paths. 
// The -pathmap option controls the source path written by the compiler to PDB files or for the CallerFilePathAttribute.
*/

using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

class DefaultLiteralExpressions
{
    // 2. default literal expressions
    // Default literal expressions are an enhancement to default value expressions
    // These expressions initialize a variable to the default value
    // Where you previously would write:
    // Func<string, bool> whereClause = default(Func<string, bool>);
    // You can now omit the type on the right-hand side of the initialization
    Func<string, bool> whereClause = default;

    public void Print()
    {
        string[] array = { "Alpha", "Beta", "Gamma", "Delta", "Epsilon" };
        whereClause = x => x.Length > 5;
        IEnumerable<string> query = array.Where(whereClause);
        WriteLine("2. default literal expressions");
        foreach (string name in query)
        {
            WriteLine(name);
        }
        WriteLine();
    }
}

class InferredTupleElementNames
{
    int count = 5;
    string label = "Colors used in the map";
    public void Print()
    {
        // This feature is a small enhancement to the tuples feature introduced in C# 7.0
        // Many times when you initialize a tuple, the variables used for the right side of the assignment are the same as the names you'd like for the tuple elements
        // var pair = (count: count, label: label);
        // The names of tuple elements can be inferred from the variables used to initialize the tuple in C# 7.1
        WriteLine("3. Inferred tuple element names");
        var pair = (count, label); // element names are "count" and "label"
        WriteLine("Count = {0}, Label = {1}", pair.count, pair.label);
        WriteLine();
    }
}

// 5. Techniques for writing safe efficient code
class WriteSafeAndEfficientCSCode
{
    // Example 3D-point structure:
    /*
    public struct Point3D
    {
        public double X;
        public double Y;
        public double Z;
    }
    */
    // 5.1 Declare a readonly struct to express that a type is immutable and enables the compiler to save copies when using in parameters.
    // Declare readonly structs for immutable value types
    // Declaring a struct using the readonly modifier informs the compiler that your intent is to create an immutable type.
    // The compiler enforces that design decision with the following rules:
    // All field members must be readonly
    // All properties must be read-only, including auto-implemented properties.
    // These two rules are sufficient to ensure that no member of a readonly struct modifies the state of that struct. 
    // The struct is immutable.
    // The example Point3D structure could be defined as an immutable struct as shown in the following example:
    readonly struct ReadonlyPoint3D
    {
        public ReadonlyPoint3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
    }

    // 5.2 Use a ref readonly return when the return value is a struct larger than IntPtr.Size and the storage lifetime is greater than the method returning the value.
    // Use ref readonly return statements for large structures when possible
    // You can return values by reference when the value being returned isn't local to the returning method. 
    // Returning by reference means that only the reference is copied, not the structure. 
    // In the following example, the Origin property can't use a ref return because the value being returned is a local variable:
    // public Point3D Origin { get; } => new Point3D(0, 0, 0);
    // However, the following property definition can be returned by reference because the returned value is a static member:
    /*
    struct Point3D
    {
        private static Point3D origin = new Point3D(0, 0, 0);

        // Dangerous! returning a mutable reference to internal storage
        // public ref Point3D Origin { get; } => ref origin;
        // public ref Point3D Origin => ref origin; 
    }
    */    
    // struct Point3D
    // 5.4 Never pass a struct as an in parameter unless it's declared with the readonly modifier because it may negatively affect performance and could lead to an obscure behavior.
    // Never use mutable structs as in in argument
    readonly struct Point3D
    {
        public Point3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        private static Point3D origin = new Point3D(0, 0, 0);

        // You don't want callers modifying the origin, so you should return the value by readonly ref:
        // readonly ref struct type
        // Declaring a struct as readonly ref combines the benefits and restrictions of ref struct and readonly struct declarations. 
        // The memory used by the readonly span is restricted to a single stack frame, and the memory used by the readonly span can't be modified.
        // public ref readonly Point3D Origin { get; } => ref origin;
        public ref readonly Point3D Origin => ref origin;

        // 5.3 When the size of a readonly struct is bigger than IntPtr.Size, you should pass it as an in parameter for performance reasons.
        // Apply the in modifier to readonly struct parameters larger than System.IntPtr.Size
        // The in keyword complements the existing ref and out keywords to pass arguments by reference. 
        // The in keyword specifies passing the argument by reference, but the called method doesn't modify the value.
        // This addition provides a full vocabulary to express your design intent. 
        // Value types are copied when passed to a called method when you don't specify any of the following modifiers in the method signature. 
        // Each of these modifiers specifies that a variable is passed by reference, avoiding the copy. 
        // Each modifier expresses a different intent:
        // out: This method sets the value of the argument used as this parameter.
        // ref: This method may set the value of the argument used as this parameter.
        // in: This method doesn't modify the value of the argument used as this parameter.
        // Add the in modifier to pass an argument by reference and declare your design intent to pass arguments by reference to avoid unnecessary copying. 
        // You don't intend to modify the object used as that argument. The following code shows an example of a method that calculates the distance between two points in 3D space.
        /*
        public double CalculateDistance(in Point3D point1, in Point3D point2)
        {
            double xDifference = point1.X - point2.X;
            double yDifference = point1.Y - point2.Y;
            double zDifference = point1.Z - point2.Z;

            return Math.Sqrt(xDifference * xDifference + yDifference * yDifference + zDifference * zDifference);
        }
        // Another feature of in parameters is that you may use literal values or constants for the argument to an in parameter. 
        // Also, unlike a ref or out parameter, you don't need to apply the in modifier at the call site. 
        // The following code shows you two examples of calling the CalculateDistance method. 
        // The first uses two local variables passed by reference. The second includes a temporary variable created as part of the method call.
        // Point3D pt1 = new Point3D();
        // Point3D pt2 = new Point3D();
        // var distance = CalculateDistance(pt1, pt2);
        // var fromOrigin = CalculateDistance(pt1, new Point3D());
        */

        // The arguments are two structures that each contain three doubles. 
        // A double is 8 bytes, so each argument is 24 bytes. By specifying the in modifier, you pass a 4 byte or 8-byte reference to those arguments, depending on the architecture of the machine. 
        // The difference in size is small, but it adds up when your application calls this method in a tight loop using many different values.
        // The in modifier complements out and ref in other ways as well. You can't create overloads of a method that differ only in the presence of in, out, or ref. 
        // These new rules extend the same behavior that had always been defined for out and ref parameters. 
        // Like the out and ref modifiers, value types aren't boxed because the in modifier is applied.
        // The in modifier may be applied to any member that takes parameters: methods, delegates, lambdas, local functions, indexers, operators.

        // There are several ways in which the compiler enforces the read-only nature of an in argument. 
        // First of all, the called method can't directly assign to an in parameter. 
        // It can't directly assign to any field of an in parameter when that value is a struct type. 
        // In addition, you can't pass an in parameter to any method using the ref or out modifier. 
        // These rules apply to any field of an in parameter, provided the field is a struct type and the parameter is also a struct type. 
        // In fact, these rules apply for multiple layers of member access provided the types at all levels of member access are structs. 
        // The compiler enforces that struct types passed as in arguments and their struct members are read-only variables when used as arguments to other methods.

        // The use of in parameters can avoid the potential performance costs of making copies. 
        // It doesn't change the semantics of any method call. Therefore, you don't need to specify the in modifier at the call site. Omitting the in modifier at the call site informs the compiler that it's allowed to make a copy of the argument for the following reasons:
        // There exists an implicit conversion but not an identity conversion from the argument type to the parameter type.
        // The argument is an expression but doesn't have a known storage variable.
        // An overload exists that differs by the presence or absence of in. In that case, the by value overload is a better match.

        // These rules are useful as you update existing code to use read-only reference arguments. 
        // Inside the called method, you can call any instance method that uses by value parameters. 
        // In those instances, a copy of the in parameter is created. 
        // Because the compiler may create a temporary variable for any in parameter, you can also specify default values for any in parameter. 
        // The following code specifies the origin (point 0,0) as the default value for the second point:
        /*
        public double CalculateDistance(in Point3D point1, in Point3D point2 = default)
        {
            double xDifference = point1.X - point2.X;
            double yDifference = point1.Y - point2.Y;
            double zDifference = point1.Z - point2.Z;

            return Math.Sqrt(xDifference * xDifference + yDifference * yDifference + zDifference * zDifference);
        }
        */
        // 5.4 Never pass a struct as an in parameter unless it's declared with the readonly modifier because it may negatively affect performance and could lead to an obscure behavior.
        // Never use mutable structs as in in argument  
        // The techniques described above explain how to avoid copies by returning references and passing values by reference. 
        // These techniques work best when the argument types are declared as readonly struct types. 
        // Otherwise, the compiler must create defensive copies in many situations to enforce the readonly-ness of any arguments.

        // The Point3D structure is not a readonly struct. // struct Point3D { }
        // There are six different property access calls in the body of this method. 
        // On first examination, you may have thought these accesses were safe. 
        // After all, a get accessor shouldn't modify the state of the object. 
        // But there's no language rule that enforces that. It's only a common convention. 
        // Any type could implement a get accessor that modified the internal state. 
        // Without some language guarantee, the compiler must create a temporary copy of the argument before calling any member. 
        // The temporary storage is created on the stack, the values of the argument are copied to the temporary storage, and the value is copied to the stack for each member access as the this argument. 
        // In many situations, these copies harm performance enough that pass-by-value is faster than pass-by-readonly-reference when the argument type isn't a readonly struct.

        // Make Point3D struct readonly // readonly struct Point3D { }
        // Instead, if the distance calculation uses the immutable struct, readonly Point3D, temporary objects are not needed:
        public double CalculateDistance(in Point3D point1, in Point3D point2 = default)
        {
            double xDifference = point1.X - point2.X;
            double yDifference = point1.Y - point2.Y;
            double zDifference = point1.Z - point2.Z;

            return Math.Sqrt(xDifference * xDifference + yDifference * yDifference + zDifference * zDifference);
        }
        // The compiler generates more efficient code when you call members of a readonly struct: 
        // The this reference, instead of a copy of the receiver, is always an in parameter passed by reference to the member method. 
        // This optimization saves copying when you use a readonly struct as an in argument.

        // 5.5 Use a ref struct, or a readonly ref struct such as Span<T> or ReadOnlySpan<T> to work with memory as a sequence of bytes.
        // Use ref struct types to work with blocks or memory on a single stack frame
        public Span<char> Trim(Span<char> source)
        {
            if (source.IsEmpty)
            {
                return source;
            }

            int start = 0, end = source.Length - 1;
            char startChar = source[start], endChar = source[end];

            while ((start < end) && (startChar == ' ' || endChar == ' '))
            {
                if (startChar == ' ')
                {
                    start++;
                }

                if (endChar == ' ')
                {
                    end--;
                }

                startChar = source[start];
                endChar = source[end];
            }

            return source.Slice(start, end - start + 1);
        }
    }

    // 5.5 Use a ref struct, or a readonly ref struct such as Span<T> or ReadOnlySpan<T> to work with memory as a sequence of bytes.
    // Use ref struct types to work with blocks or memory on a single stack frame
    class SpanReadOnlySpan
    {
        public void Print()
        {
            // System.Span<T> is a new value type at the heart of .NET. 
            // It enables the representation of contiguous regions of arbitrary memory, regardless of whether that memory is associated with a managed object, is provided by native code via interop, or is on the stack. 
            // And it does so while still providing safe access with performance characteristics like that of arrays.
            // For example, you can create a Span<T> from an array:
            var arr = new byte[10];
            Span<byte> bytes = arr; // Implicit cast from T[] to Span<T>

            // From there, you can easily and efficiently create a span to represent/point to just a subset of this array, utilizing an overload of the span’s Slice method. 
            // From there you can index into the resulting span to write and read data in the relevant portion of the original array:
            Span<byte> slicedBytes = bytes.Slice(start: 5, length: 2);
            slicedBytes[0] = 42;
            slicedBytes[1] = 43;

            WriteLine($"Assert 42 == slicedBytes[0]: {42 == slicedBytes[0]}");
            WriteLine($"Assert 43 == slicedBytes[1]: {43 == slicedBytes[1]}");
            WriteLine($"Assert arr[5] == slicedBytes[0]: {arr[5] == slicedBytes[0]}");
            WriteLine($"Assert arr[6] == slicedBytes[1]: {arr[6] == slicedBytes[1]}");

            // slicedBytes[2] = 44; // Throws IndexOutOfRangeException
            bytes[2] = 45; // OK
            WriteLine($"Assert arr[2] == bytes[2]: {arr[2] == bytes[2]}");
            WriteLine($"Assert 45 == arr[2]: {45 == arr[2]}");

            // As mentioned, spans are more than just a way to access and subset arrays. 
            // They can also be used to refer to data on the stack. For example:
            Span<byte> stackallocBytes = stackalloc byte[2]; // Using C# 7.2 stackalloc support for spans
            stackallocBytes[0] = 42;
            stackallocBytes[1] = 43;

            // Assert.AreEqual(42, stackallocBytes[0]);
            WriteLine($"Assert 42 == stackallocBytes[0]: {42 == stackallocBytes[0]}");

            // Assert.AreEqual(43, stackallocBytes[1]);
            WriteLine($"Assert 43 == stackallocBytes[1]: {43 == stackallocBytes[1]}");
            // stackallocBytes[2] = 44; // throws IndexOutOfRangeException

            // A second variant of Span<T>, called System.ReadOnlySpan<T>, enables read-only access. 
            // This type is just like Span<T>, except its indexer takes advantage of a new C# 7.2 feature to return a "ref readonly T" instead of a “ref T,” enabling it to work with immutable data types like System.String. 
            // ReadOnlySpan<T> makes it very efficient to slice strings without allocating or copying, as shown here:
            string text = "Hello World!";
            string substring = text.Substring(startIndex: 6, length: 5); // Allocates
            ReadOnlySpan<char> spanReadOnly = text.AsSpan().Slice(start: 6, length: 5); // No allocation

            WriteLine($"Assert 'W' == spanReadOnly[0]: {'W' == spanReadOnly[0]}");
            WriteLine();

            // spanReadOnly[0] = 'a'; // Error CS0200: indexer cannot be assigned to
            // Spans provide a multitude of benefits beyond those already mentioned. 
            // For example, spans support the notion of reinterpret casts, meaning you can cast a Span<byte> to be a Span<int> (where the 0th index into the Span<int> maps to the first four bytes of the Span<byte>).
            // That way if you read a buffer of bytes, you can pass it off to methods that operate on grouped bytes as ints safely and efficiently.
        }
    }

    public class SpanReadOnlySpanClient
    { 
        public void CallSite()
        {
            // 5. Techniques for writing safe efficient code
            WriteLine("5. Techniques for writing safe efficient code");
            // 5.2 Use a ref readonly return when the return value is a struct larger than IntPtr.Size and the storage lifetime is greater than the method returning the value.
            // Use ref readonly return statements for large structures when possible
            Point3D point = new Point3D(1, 2, 3);
            var originValue = point.Origin;
            ref readonly var originReference = ref point.Origin;
            WriteLine("Origin Value = {0}, Origin Reference = {1}", originValue, originReference);

            // 5.3 When the size of a readonly struct is bigger than IntPtr.Size, you should pass it as an in parameter for performance reasons.
            // Apply the in modifier to readonly struct parameters larger than System.IntPtr.Size
            // Never use mutable structs as in in argument
            // To force the compiler to pass read-only arguments by reference, specify the in modifier on the arguments at the call site, as shown in the following code:
            Point3D pt1 = new Point3D(4, 5, 6);
            Point3D pt2 = new Point3D(7, 8, 9);
            var distance = point.CalculateDistance(in pt1, in pt2);
            WriteLine("Distance = {0}", distance);
            distance = point.CalculateDistance(in pt1, new Point3D());
            WriteLine("Distance = {0}", distance);
            distance = point.CalculateDistance(pt1, in point.Origin);
            WriteLine("Distance = {0}", distance);
            // This behavior makes it easier to adopt in parameters over time in large codebases where performance gains are possible. 
            // You add the in modifier to method signatures first. 
            // Then, you can add the in modifier at call sites and create readonly struct types to enable the compiler to avoid creating defensive copies of in parameters in more locations.
            // The in parameter designation can also be used with reference types or numeric values. However, the benefits in both cases are minimal, if any.

            // 5. Techniques for writing safe efficient code
            string message = "  readonly ref struct such as Span<T> ";
            WriteLine(point.Trim(message.ToCharArray()).ToArray());
            
            SpanReadOnlySpan spanReadOnly = new SpanReadOnlySpan();
            spanReadOnly.Print();
        }
    }    
}

class NamedArguments
{
    private void PrintOrderDetails(string sellerName, int orderNum, string productName)
    {
        if (string.IsNullOrWhiteSpace(sellerName))
        {
            throw new ArgumentException(message: "Seller name cannot be null or empty.", paramName: nameof(sellerName));
        }
        WriteLine($"Seller: {sellerName}, Order #: {orderNum}, Product: {productName}");
    }

    public void Print()
    {
        WriteLine("6. Non-trailing named arguments");
        // Named arguments mixed with positional arguments are valid
        // as long as they are used in their correct position.
        PrintOrderDetails("Gift Shop", 31, productName: "Red Mug");
        // 6.Non - trailing named arguments
        // Named arguments can be followed by positional arguments.            
        PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");    // C# 7.2 onwards
        PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");                   // C# 7.2 onwards

        // The method can be called in the normal way, by using positional arguments.
        PrintOrderDetails("Gift Shop", 31, "Red Mug");
        // Named arguments can be supplied for the parameters in any order.
        PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
        PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);

        // However, mixed arguments are invalid if used out-of-order.
        // The following statements will cause a compiler error.
        // PrintOrderDetails(productName: "Red Mug", 31, "Gift Shop");
        // PrintOrderDetails(31, sellerName: "Gift Shop", "Red Mug");
        // PrintOrderDetails(31, "Red Mug", sellerName: "Gift Shop");
        WriteLine();
    }
}

class BaseClass
{
    private protected void BaseClassPrivateProtectedMethod()
    {
        WriteLine("8. private protected access modifier");
        WriteLine("Base Class Private Protected Method accessible only in the same assembly and not in different assembly.");
    }
}

class DerivedClass : BaseClass
{
    public void DerivedClassPublicMethod()
    {
        base.BaseClassPrivateProtectedMethod();
        WriteLine("Derived Class Public Instance Method");
        WriteLine();
    }

    public static void DerivedClassPublicStaticMethod()
    {
        DerivedClass derived = new DerivedClass();
        derived.BaseClassPrivateProtectedMethod();
        WriteLine("Derived Class Public Static Method");
        WriteLine();
    }
}

/*
// Assembly1.cs  
// Compile with: /target:library  
public class BaseClass
{
    private protected int number = 0;
}

public class DerivedClass1 : BaseClass
{
    void Access()
    {
        BaseClass baseObject = new BaseClass();

        // Error CS1540, because number can only be accessed by
        // classes derived from BaseClass.
        // baseObject.number = 5;  

        // OK, accessed through the current derived class instance
        number = 5;
    }
}

// Assembly2.cs  
// Compile with: /reference:Assembly1.dll  
class DerivedClass2 : BaseClass
{
    void Access()
    {
        // Error CS0122, because number can only be
        // accessed by types in Assembly1
        // number = 10;
    }
}
*/

class Features
{
    // 9. Access fixed fields without pinning.
    // Indexing fixed fields does not require pinning    
    unsafe struct UnsafeStruct
    {
        public fixed int fixedField[10];
    }
    // In earlier versions of C#, you needed to pin a variable to access one of the integers that are part of fixedField
    // In C# 7.3, the following code compiles in a safe context:
    // The variable p accesses one element in myFixedField. 
    // You don't need to declare a separate int* variable. 
    // Note that you still need an unsafe context.
    class UnsafeInClass
    {
        private static UnsafeStruct structUnsafe = new UnsafeStruct();

        unsafe public void UnsafeMethod()
        {
            structUnsafe.fixedField[5] = int.MinValue;
            int p = structUnsafe.fixedField[5];
            WriteLine("p = {0}", p);
            structUnsafe.fixedField[5] = int.MaxValue;
            p = structUnsafe.fixedField[5];
            WriteLine("p = {0}", p);
        }
    }
    // In earlier versions of C#, you need to declare a second fixed pointer:
    /*
    class UnsafeInClass
    {
        private static UnsafeStruct structUnsafe = new UnsafeStruct();

        unsafe public void UnsafeMethod()
        {
            fixed (int* ptr = structUnsafe.fixedField)
            {
                structUnsafe.fixedField[5] = int.MinValue;
                int p = structUnsafe.fixedField[5];
                WriteLine("p = {0}", p);
                structUnsafe.fixedField[5] = int.MaxValue;
                p = structUnsafe.fixedField[5];
                WriteLine("p = {0}", p);
            }            
        }
    }
    */
    struct NumberStructure
    {
        public int number;
    }

    private NumberStructure xStruct;
    private NumberStructure yStruct;

    // 12. Fixed statements with any type that supports a pattern.
    sealed class FixableType
    {
        private readonly int _value;
        public FixableType(int value) => _value = value;
        public ref readonly int GetPinnableReference() => ref _value;
    }

    unsafe public void Print()
    {
        // 9. Access fixed fields without pinning.
        WriteLine("9. Access fixed fields without pinning");
        UnsafeInClass unsafeClass = new UnsafeInClass();
        unsafeClass.UnsafeMethod();
        WriteLine();

        // 10. Reassign ref local variables.
        WriteLine("10. Reassign ref local variable");
        // ref local variables may be reassigned
        // Now, ref locals may be reassigned to refer to different instances after being initialized
        // The following code now compiles:        
        WriteLine("xStruct.number = 1:");
        xStruct.number = 1;
        WriteLine("xStruct.number={0}", xStruct.number);
        WriteLine("ref NumberStructure refLocalStruct = ref xStruct:");
        ref NumberStructure refLocalStruct = ref xStruct; // initialization        
        WriteLine("refLocalStruct.number={0}", refLocalStruct.number);
        WriteLine("yStruct.number = 2");
        yStruct.number = 2;
        WriteLine("yStruct.number={0}", yStruct.number);
        WriteLine("refLocalStruct = ref yStruct:");
        refLocalStruct = ref yStruct; // reassigned, refLocalStruct refers to different storage.
        WriteLine("refLocalStruct.number={0}", refLocalStruct.number);

        WriteLine("refLocalStruct = ref xStruct:");
        refLocalStruct = ref xStruct;
        WriteLine("xStruct.number = 3:");
        xStruct.number = 3;
        WriteLine("refLocalStruct.number={0}", refLocalStruct.number);

        NumberStructure localStruct;
        // localStruct = ref xStruct; // Error	CS8373	The left-hand side of a ref assignment must be a ref local or parameter
        WriteLine("localStruct.number = 4:");
        localStruct.number = 4;
        WriteLine("localStruct.number={0}", localStruct.number);
        WriteLine("localStruct = xStruct:");
        localStruct = xStruct;
        WriteLine("xStruct.number = 5:");
        xStruct.number = 5;
        WriteLine("localStruct.number={0}", localStruct.number);
        
        WriteLine();

        // 11. Initializers on stackalloc arrays.
        WriteLine("11. Initializers on stackalloc arrays");
        // stackalloc arrays support initializers
        int* pArr1 = stackalloc int[3] { 1, 2, 3 };
        for (int i = 0; i < 3; i++)
        {
            WriteLine(pArr1[i]);
        }
        int* pArr2 = stackalloc int[] { 1, 2, 3 };
        for (int i = 0; i < 3; i++)
        {
            WriteLine(pArr2[i]);
        }
        Span<int> spn = stackalloc[] { 1, 2, 3 };
        Array.ForEach(spn.ToArray(), element => WriteLine(element));
        WriteLine();

        // 12. Fixed statements with any type that supports a pattern.
        // The fixed statement supported a limited set of types. 
        // Starting with C# 7.3, any type that contains a GetPinnableReference() method that returns a ref T or ref readonly T may be fixed. 
        // Adding this feature means that fixed can be used with System.Span<T> and related types.
        WriteLine("12. Fixed statements with any type that supports a pattern");
        var fixableObject = new FixableType(1);
        fixed (int* ptr = fixableObject)
        {
            WriteLine(*ptr); // prints '1'
        }
        // In .NET Core 2.1 Span<T> and ReadOnlySpan<T> implement the 'fixed' pattern
        var span = ".NET Core 2.1".AsSpan();
        fixed (char* ptr = span)
        {
            WriteLine(ptr[0]); // prints '.'
        }
        WriteLine();
    }
}

static class GenericConstraintsExtensionMethod
{
    // 13. Additional generic constraints.
    // Unmanaged constraint
    unsafe public static byte[] ToByteArray<T>(this T argument) where T : unmanaged
    {
        var size = sizeof(T);
        var result = new Byte[size];
        Byte* p = (byte*)&argument;
        for (var i = 0; i < size; i++)
        {
            result[i] = *p++;
        }
        return result;
    }

    // 13. Additional generic constraints.
    // Delegate constraints
    public static TDelegate TypeSafeCombine<TDelegate>(this TDelegate source, TDelegate target) where TDelegate : System.Delegate => Delegate.Combine(source, target) as TDelegate;
}

class GenericConstraintsEnum
{
    // 13. Additional generic constraints.
    // Enum constraints
    public Dictionary<int, string> EnumNamedValues<T>() where T : System.Enum
    {
        var result = new Dictionary<int, string>();
        var values = Enum.GetValues(typeof(T));

        foreach (int item in values)
        {
            result.Add(item, Enum.GetName(typeof(T), item));
        }
        return result;
    }
}

enum Greek
{
    Alpha,
    beta,
    Gamma,
    Delta,
    Epsilon,
    Zeta,
    Eta
}

class GenericConstraintsClient
{ 
    public void Print()
    {
        // 13. Additional generic constraints.
        WriteLine("13. Additional generic constraints");
        // Unmanaged constraint
        WriteLine("Unmanaged constraint");
        Guid gid = Guid.NewGuid();
        WriteLine("Guid: {0}", gid);
        byte[] byteArray = gid.ToByteArray<Guid>();
        Array.ForEach(byteArray, element => WriteLine(element));

        // 13. Additional generic constraints.
        // Delegate constraints
        WriteLine("Delegate constraints");
        Action first = () => WriteLine("Foo");
        Action second = () => WriteLine("Bar");
        var combined = first.TypeSafeCombine(second);
        combined();

        Func<bool> test = () => true;
        // Combine signature ensures combined delegates must
        // have the same type.
        //var badCombined = first.TypeSafeCombine(test);

        // 13. Additional generic constraints.
        // Enum constraints
        GenericConstraintsEnum genericConstraintEnum = new GenericConstraintsEnum();
        var map = genericConstraintEnum.EnumNamedValues<Greek>();

        WriteLine("Enum constraints");
        foreach (var pair in map)
        {
            WriteLine($"{pair.Key}:{pair.Value}");
        }
        WriteLine();
    }
}

public class ParentClass
{
    public ParentClass(int i, out int j)
    {
        j = i;
    }
}

public class ChildClass : ParentClass
{
    // 15. Expression variables in more locations.
    // Extend expression variables in initializers
    // The syntax added in C# 7.0 to allow out variable declarations has been extended to include field initializers, property initializers, constructor initializers, and query clauses.
    public ChildClass(int i) : base(i, out var j)
    {
        WriteLine($"The value of 'j' is {j}");
    }
}

// 16. Attach attributes to the backing field of auto-implemented properties.
class SomeThingAboutFieldAttribute : Attribute { }

// 17. Method resolution when arguments differ by in has been improved.
readonly struct ReadonlyStruct { }

delegate int Delegate1(Alpha alph);
delegate void Delegate2(Beta bet); // delegate void return

class Alpha { }
class Beta { }

class MethodGroupConversion
{
    private void DelegateMethod(Delegate1 d) { WriteLine("delegate int return"); }
    private void DelegateMethod(Delegate2 d) { WriteLine("delegate void return"); } // delegate void return

    private void MethodOverload(Alpha alph) { }
    private void MethodOverload(Beta bet) { }

    public void Print()
    {
        // 18. Overload resolution now has fewer ambiguous cases.
        // 3. For a method group conversion, candidate methods whose return type doesn't match up with the delegate's return type are removed from the set.
        // Pre-C# 7.3 // Error CS0121  The call is ambiguous between the following methods or properties: 'MethodGroupConversion.M(D1)' and 'MethodGroupConversion.M(D2)'  
        WriteLine("3. For a method group conversion, candidate methods whose return type doesn't match up with the delegate's return type are removed from the set");
        DelegateMethod(MethodOverload); // delegate void return // Prints "delegate void return" in C# 7.3
    }
}

class Foo { }
class Bar { }

static class FooExtensions
{
    public static TFoo Frob<TFoo>(this TFoo foo) where TFoo : Foo { WriteLine("FooExtensions Frob"); return null; }
    public static TFoo Brob<TFoo>(this TFoo foo) where TFoo : Foo { WriteLine("FooExtensions Brob"); return null; }
}

static class BarExtensions
{
    public static TBar Frob<TBar>(this TBar bar) where TBar : Bar { WriteLine("BarExtensions Frob"); return null; }
}

class Animal { public Animal() { WriteLine("Animal"); } }
class Mammal : Animal { public Mammal() { WriteLine("Mammal"); } }
class Giraffe : Mammal { public Giraffe() { WriteLine("Giraffe"); } }
class Reptile : Animal { public Reptile() { WriteLine("Reptile"); } }

class Alphabet { }

class GenericConstraints
{
    private void OverloadAnimal<T>(T t) where T : Reptile
    {
        WriteLine("Overload Animal Generic Constraint Reptile");
    }

    private void OverloadAnimal(Animal animal)
    {
        WriteLine("Overload Animal Non-generic");
    }

    private void OverloadGeneric<T>(T t) where T : class
    {
        WriteLine("Overload Generic constraint class");
    }

    private void OverloadGeneric<T>(Alphabet alphab) where T : struct
    {
        WriteLine("Overload Generic constraint struct");
    }

    public void Print()
    {
        // Pre-C# 7.3 // Error	CS0311	The type 'Giraffe' cannot be used as type parameter 'T' in the generic type or method 'GenericConstraints.OverloadAnimal<T>(T)'. 
        // There is no implicit reference conversion from 'Giraffe' to 'Reptile'.
        OverloadAnimal(new Giraffe());

        Alphabet alphab = new Alphabet();
        // Pre-C# 7.3 // Error	CS0453	The type 'Alphabet' must be a non-nullable value type in order to use it as parameter 'T' in the generic type or method 'GenericConstraints.OverloadGeneric<T>(Alphabet)'	
        OverloadGeneric<Alphabet>(alphab);
    }
}

class Query
{
    public static object Select(Func<Alpha, Alpha> y)
    {
        Console.WriteLine("Static");
        return null;
    }

    public object Select(Func<Beta, Beta> y)
    {
        Console.WriteLine("Instance");
        return null;
    }
}

class Enhancements
{
    // 16. Attach attributes to the backing field of auto-implemented properties.
    // Attach attributes to the backing fields for auto-implemented properties
    // The attribute SomeThingAboutFieldAttribute is applied to the compiler generated backing field for SomeProperty
    [field: SomeThingAboutFieldAttribute]
    public int SomeProperty { get; set; }

    // 17. Method resolution when arguments differ by in has been improved.
    // This was implemented as a bug fix. This no longer is ambiguous even with the language version set to "7.2".
    private void OverloadMethod(ReadonlyStruct arg) { WriteLine("non-in-argument"); }
    private void OverloadMethod(in ReadonlyStruct arg) { WriteLine("in-argument"); }

    public void Print()
    {
        // 14. Test == and != with tuple types.
        // Tuples support == and !=
        WriteLine("14. Test == and != with tuple types");
        var left = (a: 5, b: 10);
        var right = (a: 5, b: 10);
        WriteLine(left == right); // displays 'true'
        WriteLine(left != right); // displays 'false'
        (int a, int b)? nullableTuple = right;
        WriteLine(left == nullableTuple); // Also true

        // lifted conversions
        (int? a, int? b) nullableMembers = (5, 10);
        WriteLine(left == nullableMembers); // Also true

        // converted type of left is (long, long)
        (long a, long b) longTuple = (5, 10);
        WriteLine(left == longTuple); // Also true

        // comparisons performed on (long, long) tuples
        (long a, int b) longFirst = (5, 10);
        (int a, long b) longSecond = (5, 10);
        WriteLine(longFirst == longSecond); // Also true

        (int a, string b) pair = (1, "Hello");
        (int z, string y) another = (1, "Hello");
        WriteLine(pair == another); // true. Member names don't participate.
        WriteLine(pair == (z: 1, y: "Hello")); // warning: literal contains different member names

        (int, (int, int)) nestedTuple = (1, (2, 3));
        WriteLine(nestedTuple == (1, (2, 3)));
        WriteLine();

        // 15. Expression variables in more locations.
        WriteLine("15. Expression variables in more locations");
        ChildClass child = new ChildClass(default);
        WriteLine();

        // 16. Attach attributes to the backing field of auto-implemented properties.
        WriteLine("16. Attach attributes to the backing field of auto-implemented properties");
        WriteLine(SomeProperty);
        WriteLine();

        // 17. Method resolution when arguments differ by in has been improved.
        ReadonlyStruct readonlyStructure = default;
        WriteLine("17. Method resolution when arguments differ by in has been improved");
        // This was implemented as a bug fix. This no longer is ambiguous even with the language version set to "7.2".
        OverloadMethod(readonlyStructure);
        OverloadMethod(in readonlyStructure);
        WriteLine();

        // 18. Overload resolution now has fewer ambiguous cases.
        WriteLine("18. Overload resolution now has fewer ambiguous cases");
        // Three new rules to help the compiler pick the obvious choice:
        // 1. When a method group contains both instance and static members, the compiler discards the instance members if the method was invoked without an instance receiver or context. 
        // The compiler discards the static members if the method was invoked with an instance receiver. When there is no receiver, the compiler includes only static members in a static context, otherwise both static and instance members. 
        // When the receiver is ambiguously an instance or type, the compiler includes both. 
        // A static context, where an implicit this instance receiver cannot be used, includes the body of members where no this is defined, such as static members, as well as places where this cannot be used, such as field initializers and constructor-initializers.
        // Pre-C# 7.3 // Error CS1940  Multiple implementations of the query pattern were found for source type 'Query'.
        // Ambiguous call to 'Select'.CoreCSC7.x
        WriteLine("1. When a method group contains both instance and static members, the compiler discards the instance members if the method was invoked without an instance receiver or context. The compiler discards the static members if the method was invoked with an instance receiver. When there is no receiver, the compiler includes only static members in a static context, otherwise both static and instance members. When the receiver is ambiguously an instance or type, the compiler includes both. A static context, where an implicit this instance receiver cannot be used, includes the body of members where no this is defined, such as static members, as well as places where this cannot be used, such as field initializers and constructor-initializers");
        var q = from x in new Query()
                select x;

        // 2. When a method group contains some generic methods whose type arguments do not satisfy their constraints, these members are removed from the candidate set.
        // Pre-C# 7.3 // Error CS0121  The call is ambiguous between the following methods or properties: 'FooExtensions.Frob<TFoo>(TFoo)' and 'BarExtensions.Frob<TBar>(TBar)'    
        WriteLine("2. When a method group contains some generic methods whose type arguments do not satisfy their constraints, these members are removed from the candidate set");
        new Foo().Frob();
        
        // 3. For a method group conversion, candidate methods whose return type doesn't match up with the delegate's return type are removed from the set.
        MethodGroupConversion groupConversion = new MethodGroupConversion();
        groupConversion.Print();
        WriteLine();

        WriteLine("C# 7.3 Note");
        WriteLine("Generic Constraints Overload");
        GenericConstraints genericConstraint = new GenericConstraints();
        genericConstraint.Print();
    }
}

class Program
{
    Task<int> DoAsyncWork()
    {
        // ReadKey();
        Read();
        return Task<int>.FromResult(0);
    }
    // If the program doesn't return an exit code, you can declare a Main method that returns a Task
    Task SomeAsyncMethod()
    {
        return Task.FromResult(0);
    }

    // 1. async Main method
    static async Task<int> Main()
    // static async Task Main() // If the program doesn't return an exit code, you can declare a Main method that returns a Task
    {
        // Environment.Version property returns the .NET runtime version for .NET 5+ and .NET Core 3.x
        // Not recommend for .NET Framework 4.5+
        Console.WriteLine($"Environment.Version: {Environment.Version}");
        // RuntimeInformation.FrameworkDescription property gets the name of the .NET installation on which an app is running
        // .NET 5+ and .NET Core 3.x // .NET Framework 4.7.1+ // Mono 5.10.1+
        Console.WriteLine($"RuntimeInformation.FrameworkDescription: {RuntimeInformation.FrameworkDescription}");
        Console.WriteLine();

        Program prgm = new Program();
        WriteLine("1. async Main method");
        WriteLine();

        DefaultLiteralExpressions defaultLiteralExpression = new DefaultLiteralExpressions();
        defaultLiteralExpression.Print();

        InferredTupleElementNames inferredTupleElementName = new InferredTupleElementNames();
        inferredTupleElementName.Print();

        WriteSafeAndEfficientCSCode.SpanReadOnlySpanClient point3D = new WriteSafeAndEfficientCSCode.SpanReadOnlySpanClient();
        point3D.CallSite();

        NamedArguments namedArgs = new NamedArguments();
        namedArgs.Print();

        // 7.Leading underscores in numeric literals
        // Numeric literals can now have leading underscores before any printed digits.
        // The implementation of support for digit separators in C# 7.0 didn't allow the _ to be the first character of the literal value.
        // Hex and binary numeric literals may now begin with an _.
        int binaryValue = 0b_0101_0101;
        WriteLine("7.Leading underscores in numeric literals");
        WriteLine("Binary numeric literal value = {0}", binaryValue);
        WriteLine();

        DerivedClass derived = new DerivedClass();
        derived.DerivedClassPublicMethod();
        DerivedClass.DerivedClassPublicStaticMethod();

        Features feature = new Features();
        feature.Print();

        GenericConstraintsClient genericConstraintClient = new GenericConstraintsClient();
        genericConstraintClient.Print();

        Enhancements enhancement = new Enhancements();
        enhancement.Print();

        // 1. async Main method
        // This could also be replaced with the body
        // DoAsyncWork, including its await expressions
        return await prgm.DoAsyncWork();
        // await prgm.SomeAsyncMethod();
    }
}

/*
Courtesy:
https://docs.microsoft.com
*/