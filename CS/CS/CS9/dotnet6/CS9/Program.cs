// CS 9
/*
1. Records
2. Init only setters
3. Top-level statements
4. Pattern matching enhancements
5. Native sized integers
6. Function pointers
7. Suppress emitting localsinit flag
8. Target-typed new expressions
9. static anonymous functions
10. Target-typed conditional expressions
11. Covariant return types
12. Extension GetEnumerator support for foreach loops
13. Lambda discard parameters
14. Attributes on local functions
15. Module initializers
16. New features for partial methods
*/


using System;
using System.Collections.Generic;
using System.Text;
// using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

// 3. Top-level statements
// Top-level statements must precede namespace and type declarations.
// Warning CS7022: The entry point of the program is global code; ignoring 'Program.Main()' entry point. // Errors will not be ignored though.
// Console.WriteLine("3. Top-level statements");
// Console.WriteLine(Environment.Version);
// dotnet run World
// Console.WriteLine($"Hello {args[0]}!");
// return;
// only int
// return 1;
// The top-level statements may contain async expressions. In that case, the synthesized entry point returns a Task, or Task<int>.
// Task taskAsync = Task.Run(() => Thread.Sleep(1000)); // Task taskAsync = Task.Run(() => Task.Delay(1000));
// Console.WriteLine(taskAsync.GetType());
// await taskAsync;
// Task<int> asyncTask = Task.Run( () => Enumerable.Range(1, new Random().Next(100)).Count( x => x % 2 == 0));
// Console.WriteLine(asyncTask.GetType());
// return await asyncTask;

// 1. Records
/*
public record Vehicle
{
    public string RealName { get; }
    public string Nickname  { get; }

    public Vehicle(string real, string nick) => (RealName, Nickname) = (real, nick);
}

// Inherit record
// Records may only inherit from object or another record
public record Car : Vehicle
{
    public string Brand { get; }

    public Car(string real, string nick, string tagline) : base(real, nick) => Brand = tagline;
}

// Can be sealed
public sealed record Aircraft : Vehicle
{
    public int AircraftClassificationNumber { get; }

    public Aircraft(string real, string nick, int acn) : base(real, nick) => AircraftClassificationNumber = acn;
}
*/
// Positional records
public record Vehicle(string RealName, string Nickname);

public record Car(string RealName, string Nickname, string Brand) : Vehicle(RealName, Nickname);

public sealed record Aircraft(string RealName, string Nickname, int AircraftClassificationNumber) : Vehicle(RealName, Nickname);

// Anamoly?
record WithoutCurlyBraces;

// Can have destructor
public record RecordType
{
    ~RecordType()
    {
        Console.Write("record destructor");
    }
}

interface IRecordInterface { }

// Default internal
// Can be generic
// Can be abstract
// Can only inherit from object or another record
// Can implement interface
// Can have static constructor
// Cannot be static 
abstract record AssertRecord<T> : object, IRecordInterface
{ 
    static AssertRecord() { }
    public abstract void AbstractMethod(); 
}

// Add a body and include any additional methods as well to record
public record Games(string Name)
{
    public void Play() => Console.WriteLine("Good sport!");
}

public record Olympics(string Name) : Games(Name)
{
    public void WinMedals() => Console.WriteLine("Bring laurels!");

    public override string ToString()
    {
        StringBuilder s = new();
        // CS 9 record base
        base.PrintMembers(s); 
        return $"Olympics {{ {s.ToString()} }}";
    }
}

class C
{
    public int x;
}

struct S
{
    public int x;
}

record R
{
    public int x;
}

class Records
{
    private void PrintC()
    {
	RecordType typeRecord = new RecordType();
        C a = new();
        C b = new();
        a.x = 1;
        b.x = 2;
        Console.WriteLine("Class Before a = b: a.x = {0}, b.x = {1}\n", a.x, b.x); // 1, 2
        a = b;
        a.x = 3;
        Console.WriteLine("Class After a = b and a.x = 3: a.x = {0}, b.x = {1}\n", a.x, b.x); // 3, 3
    }

    private void PrintS()
    {
        S a = new();
        S b = new();
        a.x = 1;
        b.x = 2;
        Console.WriteLine("Structure Before a = b: a.x = {0}, b.x = {1}\n", a.x, b.x); // 1, 2
        a = b;
        a.x = 3;
        Console.WriteLine("Structure After a = b and a.x = 3: a.x = {0}, b.x = {1}\n", a.x, b.x); // 3, 2
    }

    private void PrintR()
    {
        R a = new();
        R b = new();
        a.x = 1;
        b.x = 2;
        Console.WriteLine("Record Before a = b: a.x = {0}, b.x = {1}\n", a.x, b.x); // 1, 2
        a = b;
        a.x = 3;
        Console.WriteLine("Record After a = b and a.x = 3: a.x = {0}, b.x = {1}\n", a.x, b.x); // 3, 3
    }

    public void Print()
    {
        Console.WriteLine("1. Records");
        Console.WriteLine($"record IsValueType: {typeof(R).IsValueType}");
        Console.WriteLine($"record new() IsValueType: {new R().GetType().IsValueType}");
        Console.WriteLine($"record IsClass: {typeof(R).IsClass}\n");
        PrintC(); 
        PrintS();
        PrintR();  
        var veh = new Vehicle("Boeing", "Dreamliner");
        var air = new Aircraft("Boeing", "Dreamliner", 787);
                
        // Equality is value-based
        Console.WriteLine("Equality is value-based");
        Console.WriteLine(veh == air); // false
        Console.WriteLine(veh is Aircraft a); // false
        Console.WriteLine(air is Vehicle c); // true
        // Print record object ToString() 
        Console.WriteLine(air.ToString()); 

        // Deconstruct method for positional records
        Console.WriteLine("Deconstruct method for positional records");
        var (real, nick) = veh;
        Console.WriteLine(real);
        Console.WriteLine(nick);
        var (_, _, acn) = air;
        Console.WriteLine(acn);

        // Record with-expressions
        Console.WriteLine("Record with-expressions");
        Vehicle airbusDreamliner = veh with { RealName = "Airbus" };
        Console.WriteLine(airbusDreamliner.ToString());
        Vehicle boeingSuperjumbo = veh with { Nickname = "Superjumbo" };
        Console.WriteLine(boeingSuperjumbo.ToString());
        Vehicle airbusSuperjumbo = veh with { RealName = "Airbus", Nickname = "Superjumbo" };
        Console.WriteLine(airbusSuperjumbo.ToString());
        Vehicle airbusIndustrie = air with { RealName = "Airbus", Nickname = "Superjumbo", AircraftClassificationNumber = 380 };
        Console.WriteLine(airbusIndustrie.ToString());

	Console.WriteLine(new Games("Tokyo 2020").ToString());
        Console.WriteLine(new Olympics("Tokyo 2020").ToString());
        Console.WriteLine();
    }
}

// record Analytics
struct Analytics
{
    // 2. Init only setters
    // init accessors instead of set accessors for properties and indexers
    // public string Browser { get; set; }
    public string Browser { get; init; }
    public int Sessions { get; init; }
    public float PagesPerSession { get; init; }

    public override string ToString() => $"Browser = {Browser}, Sessions = {Sessions}, with {PagesPerSession} pages/session.";
}

class InitOnlySetters
{
    public void Print()
    {
        // Property initializer syntax to set the values, while still preserving the immutability
        var metrics = new Analytics 
        { 
            Browser = "Chromium", 
            Sessions = 3000,
            PagesPerSession = 3.5F,
        };
        // metrics = metrics with { Browser = "Chrome" }; // record type
        // Changing a property after initialization is an error by assigning to an init-only property outside of initialization
        // The construction phase effectively ends after all initialization, including property initializers and with-expressions (record type) have completed.
        // Error CS8852: Init-only property or indexer 'Analytics.Browser' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
        // metrics.Browser = "Edge";
        Console.WriteLine("2. Init only setters");
        Console.WriteLine(metrics.ToString());
        Console.WriteLine();
    }
}

static class Extensions
{
    // 4. Pattern matching enhancements
    public static bool IsNumeric(this char c) => c is >= '0' and <= '9';
    public static bool IsAlphaNumeric(this char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or (>= '0' and <= '9');

    // 12. Extension GetEnumerator support for foreach loops
    // CS 9 foreach loop will recognize and use an extension method GetEnumerator
    public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;
    public static IAsyncEnumerator<T> GetAsyncEnumerator<T>(this IAsyncEnumerator<T> enumerator) => enumerator;
    public static IEnumerator<object> GetEnumerator<T1, T2, T3>(this ValueTuple<T1, T2, T3> tuple)
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
    }
    public static IEnumerator<object> GetEnumerator<T1, T2, T3>(this Tuple<T1, T2, T3> tuple)
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
    }
}

class PatternMatchingEnhancements
{      
    public void Print()
    {
        Console.WriteLine("4. Pattern matching enhancements");
        Console.WriteLine('1'.IsNumeric()); // true
        Console.WriteLine('a'.IsNumeric()); // false
        // Alternatively, with optional parentheses to make it clear that and has higher precedence than or
        Console.WriteLine('a'.IsAlphaNumeric()); // true
        Console.WriteLine('1'.IsAlphaNumeric()); // true
        Console.WriteLine('~'.IsAlphaNumeric()); // false

        var isNot = string.Empty;

        if (isNot is not null)
	{
    	    Console.WriteLine("is not null");
	}
        Console.WriteLine();
    }
}

class NativeSizedIntegers
{
    // 5. Native sized integers
    nint native = nint.MinValue;
    nuint unsignedNative = nuint.MaxValue; 

    public void Print()
    {
        Console.WriteLine("5. Native sized integers");
        Console.WriteLine($"nint.MinValue = {native}");
        Console.WriteLine($"nuint.MaxValue = {unsignedNative}");
        Console.WriteLine();
    }
}

// <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
unsafe delegate char* UnsafeDelegate(char *c);

// Only for reference/analogy
/*
delegate int FunctionPointer(string s);
// delegate FunctionPointer FunPointer(FunctionPointer f);
// unsafe delegate delegate*<string, int> FunPointer(delegate* managed<string, int> f);
unsafe delegate delegate*<char*, int> FunPointer(delegate* managed<char*, int> f);
// unsafe delegate delegate* unmanaged<string, void> UnmanagedDelegate();
unsafe delegate delegate* unmanaged<char*, void> UnmanagedDelegate();
*/

class FunctionPointers
{
    // Unsafe delegate need not refer static method
    // public static char* UnsafeDelegateMethod(char *c) => c;
    unsafe public char* UnsafeDelegateMethod(char *c) => c;

    // Only static methods for function pointers delegate*
    public static void StaticMethod() => Console.WriteLine("Function pointer to static method");
    public static void StaticMethod(string s) => Console.WriteLine($"Function pointer to static method: {s}");
    public static void StaticMethod(int i) => Console.WriteLine($"Function pointer to static method: {i}");

    // Only static methods for managed and unmanaged function pointers delegate*
    public static void ManagedStaticMethod(string s) => Console.WriteLine($"Function pointer: {s}");
    // unsafe public static void UnManagedStaticMethod(int i) => Console.WriteLine($"Function pointer: {i}");
    unsafe public static void UnManagedStaticMethod(char *c) => Console.WriteLine($"Function pointer: {Marshal.PtrToStringAnsi((IntPtr)c)}");    
    
    // Only static methods for function pointer equivalents delegate*
    public static int Function(string s) { Console.WriteLine($"Function pointer: {s}"); return default(int); }
    unsafe public static delegate*<string, int> Fun(delegate* managed<string, int> f) => default(delegate*<string, int>);

    unsafe public void Print() 
    {
	UnsafeDelegate delUnsafe = UnsafeDelegateMethod;                 

        // 6. Function pointers
        // Method groups will now be allowed as arguments to an address-of expression
        delegate*<void> delStaticMethod = &StaticMethod; // StaticMethod()
        // last parameterized type is return type
        delegate*<string, void> delStaticMethods = &StaticMethod; // StaticMethod(string s)
        delegate*<int, void> delStaticMethodi = &StaticMethod; // StaticMethod(int i)

        // Error: ambiguous conversion from method group StaticMethod to "void*"
        // void* v = &StaticMethod;

        delegate* managed<string, void> delManaged = &ManagedStaticMethod;
        // delegate* unmanaged<int, void> delUnmanaged = (delegate* unmanaged<int, void>)(delegate* <int, void>)&UnManagedStaticMethod;
        delegate* unmanaged<char*, void> delUnmanaged = (delegate* unmanaged<char*, void>)(delegate* <char*, void>)&UnManagedStaticMethod;

        // Function pointer equivalent without calling convention
        delegate*<string, int> delEquivalent = &Function;
        delegate*<delegate* managed<string, int>, delegate*<string, int>> delEquivalentManaged = &Fun;

        // Function pointer equivalent with calling convention
        delegate* managed<string, int> delManagedEqui = &Function;
        delegate*<delegate* managed<string, int>, delegate*<string, int>> delEquiManaged = &Fun;

        Console.WriteLine("6. Function pointers");

        string marshal = "Marshal: unsafe delegate can refer non-static method";
        IntPtr iptrmarshal = Marshal.StringToHGlobalAnsi(marshal);  
        char *cptrmarshal = (char*)iptrmarshal.ToPointer();
        char *cpmarshal = delUnsafe(cptrmarshal);
        Console.WriteLine(Marshal.PtrToStringAnsi((IntPtr)cpmarshal));

        delStaticMethod();
        delStaticMethods("Hello, World!");
        delStaticMethodi(1);

        delManaged("delegate managed");
        // delUnmanaged(1);
        string marsh = "Marshal: delegate unmanaged";
        IntPtr iptrmarsh = Marshal.StringToHGlobalAnsi(marsh);  
        char *cptrmarsh = (char*)iptrmarsh.ToPointer();
        delUnmanaged(cptrmarsh);

        Console.WriteLine($"delegate equivalent return: {delEquivalent("delegate equivalent")}");
        /*
        delegate*<string, int> delEquivalentManagedRoundtrip = delEquivalentManaged(delEquivalent);
        delEquivalentManagedRoundtrip = &Function;
        Console.WriteLine($"delegate equivalent managed round trip return: {delEquivalentManagedRoundtrip("delegate equivalent managed round trip")}");
        */
        delEquivalent = delEquivalentManaged(delEquivalent);
        delEquivalent = &Function;
        Console.WriteLine($"delegate equivalent managed round trip return: {delEquivalent("delegate equivalent managed round trip")}");
        
        Console.WriteLine($"delegate managed equi return: {delManagedEqui("delegate managed equi")}");
        /*
        delegate*<string, int> delEquiManagedRoundtrip = delEquiManaged(delManagedEqui);
        delEquiManagedRoundtrip = &Function;
        Console.WriteLine($"delegate equi managed round trip return: {delEquiManagedRoundtrip("delegate equi managed round trip")}");
        */
        delManagedEqui = delEquiManaged(delManagedEqui);
        delManagedEqui = &Function;
        Console.WriteLine($"delegate equi managed round trip return: {delManagedEqui("delegate equi managed round trip")}");
        Console.WriteLine();
    }
}

class Suppresslocalsinit
{ 
    // 7. Suppress emitting localsinit flag
    // CS 9 Add the SkipLocalsInitAttribute to instruct the compiler not to emit the localsinit flag 
    // As this flag instructs the CLR to zero-initialize all local variables
    // <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    [SkipLocalsInitAttribute]
    public void StackAllocatedMemoryBlock()
    {
        Console.WriteLine("7. Suppress emitting localsinit flag");
        Console.Write("SkipLocalsInitAttribute for stackalloc: ");
        int length = 3;
        Span<int> numbers = stackalloc int[length];
        for (var i = 0; i < length; i++)
        {
            numbers[i] = i;
            Console.Write($"{numbers[i]} ");
        }
        Console.WriteLine("\n");
    }

    public void Print()
    {
        StackAllocatedMemoryBlock();
    }
}

class AnalyticsOptions { }

class Dimension
{
    // Init only setters or properties 
    public string Location { get; init; }
}

delegate void Del();

class A 
{ 
    public virtual A VirtualMethodA() => new();
}

class B : A 
{ 
    // 11. Covariant return types
    // CS 9 Overridden virtual function can return a type derived from the return type declared in the base class method
    public override B VirtualMethodA() => new();
    // public override Covariant VirtualMethodA() => new();
}

// class Covariant : A { }
class Covariant : B { }

partial class PartialMethods
{
    // Defining declaration for implementing declaration of partial method
    public partial PartialMethods PartialMethod(out int o);
}

partial class PartialMethods
{
    // Pre-CS9 partial methods are private but can't specify an access modifier, have a void return, and can't have out parameters
    // CS 9 removes these restrictions, but requires that partial method declarations have an implementation
    // Partial method without an access modifier follow the old rules
    // If the partial method includes the private access modifier, the new rules govern that partial method
    public partial PartialMethods PartialMethod(out int o)
    { 
        o = 1;
        Console.WriteLine("16. New features for partial methods");
        return new();
    }
}

class CS9Features
{
    // 8. Target-typed new expressions
    // CS 9 Omit the type in a new expression when the created object's type is already known
    private List<Analytics> metrics = new();

    // Return an instance created by the default constructor using a return new(); statement
    public Analytics Instance() 
    {
        return new();
    }

    // 8. Target-typed new expressions
    // Target-typed new can also be used when you need to create a new object to pass as an argument to a method
    // public Analytics MetricsFor(string browser, AnalyticsOptions options) => new();
    public Analytics MetricsFor(string browser, AnalyticsOptions options) => Instance();
    
    // 9. static anonymous functions
    // CS 9 static modifier for lambda expressions or anonymous methods
    Del anonymous = static () => Console.WriteLine("9. static anonymous functions"); // Del anonymous = static delegate { Console.WriteLine("9. static anonymous functions"); };
    Func<int, int> fun = static x => x * x;
    Action<int> act = static (int i) => Console.WriteLine($"static action: {i}"); // Action<int> act = static delegate (int i) { Console.WriteLine($"static action: {i}"); };
    // 13. Lambda discard parameters
    // CS 9 discards for specifying two or more input parameters of a lambda expression that aren't used in the expression
    Func<string, string, string> constant = static (_, _) => "13. Lambda discard parameters (9. static lambda)";

    async IAsyncEnumerator<int> GetAsyncEnumerator()
    {
        yield return 0;
        await Task.Delay(1);
        yield return 1;
    }

    public void LocalFunctions()
    {
        Console.WriteLine("14. Attributes on local functions");
        Console.WriteLine("AllowNull");
        Console.WriteLine(AllowNullLocalFunction(null));
        Console.WriteLine("DisallowNull: warning CS8625, warning CS8603");
#nullable enable // Or //#nullable enable warnings // warning CS8625: Cannot convert null literal to non-nullable reference type.
        Console.WriteLine(DisallowNullLocalFunction(null));
#nullable disable // Or //#nullable disable warnings

        ///*
        Console.WriteLine("static extern in local function");
        c_code();
        Console.WriteLine();
        //*/

	/*
	You can use the following modifiers with a local function:
	async
	unsafe
	static (in C# 8.0 and later). A static local function can't capture local variables or instance state.
	extern (in C# 9.0 and later). An external local function must be static.
	*/

        // 14. Attributes on local functions
        // CS 9 Nullable attribute annotations to local functions
        [return: MaybeNull] string AllowNullLocalFunction([AllowNull] string allowNull) => null;
#nullable enable // Or //#nullable enable warnings // warning CS8603: Possible null reference return // [return: NotNull] 
        string DisallowNullLocalFunction([DisallowNull] string disallowNull) => null;
#nullable disable // Or //#nullable disable warnings

	///*
        // CS 9 extern modifier for local functions
        // [DllImport(@"D:\RajaniS Master Folder\.NET\CS\CS.NET-Core\CS9\CS9\msys64-mingw64-gcc.dll", CharSet=CharSet.Unicode)]

        [DllImport(@"/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/msys64-mingw64-gcc.so")]

        static extern int c_code();
	//*/
    }
     
    // 15. Module initializers 
    /*
    Must have ModuleInitializerAttribute attribute 
    Must be static
    Must be parameterless
    Must return void
    Must not be a generic method
    Must not be contained in a generic class
    Must be accessible from the containing module // Will be called by the runtime when the assembly loads
    */
    [ModuleInitializerAttribute]
    public static void ModuleInitializer()
    {
	Console.WriteLine("15. Module initializers");
	Console.WriteLine("ModuleInitializerAttribute, static, parameterless, and void");
        Console.WriteLine();   
    }

    public async Task Print()
    {
        Console.WriteLine("8. Target-typed new expressions");        
        var metric = MetricsFor("Chrome", new());
        // Combine target-typed new with init only properties to initialize a new object
        Dimension city = new() { Location = "Scientia" };
        Console.WriteLine(city.Location);
        Console.WriteLine();

        // 9. static anonymous functions
        anonymous();
        act(fun(5)); 
        Console.WriteLine();

        var rand = new Random();
        var condition = rand.Next(0, 99) > 50;
        // 10. Target-typed conditional expressions
        // Conditional expressions that previously required casts or wouldn’t compile now just work
        // int? nullable = condition ? 1 : (int?)null;
        Console.WriteLine("10. Target-typed conditional expressions");
        int? nullable = condition ? 1 : null;
        Console.WriteLine($"{condition} {nullable}");
        // Type of conditional expression cannot be determined because there is no implicit conversion between 'int' and '<null>'
        // var implicitNullable = condition ? 1 : null;
        Console.WriteLine();

        Console.WriteLine("11. Covariant return types");
        B b = new B();
        b.VirtualMethodA();
        Console.WriteLine();

        IEnumerator<int> enumerator = Enumerable.Range(0, 10).GetEnumerator();        
        // Pre-CS 9 
        /*
        while(enumerator.MoveNext()) 
        {
            Console.Write(enumerator.Current + " ");            
        }   
        */
        // Pre-CS 9 foreach statement cannot operate on variables of type 'IEnumerator<int>' because 'IEnumerator<int>' does not contain a public instance 
        // CS 9 foreach loop recognizes extension method GetEnumerator 
        Console.WriteLine("12. Extension GetEnumerator support for foreach loops");
        foreach (var item in enumerator)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
  
        // CS 9 foreach recognizes async extension method
        IAsyncEnumerator<int> enumeratorAsync = GetAsyncEnumerator();
        await foreach (var item in enumeratorAsync)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // CS 9 Iterate on ValueTuple using a foreach
        foreach (var item in (2, 3, 4))
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        // CS 9 Iterate on Tuple using a foreach
        foreach (var item in Tuple.Create(4, 5, 6))
        {
            Console.Write(item + " ");
        }
        Console.WriteLine("\n");

        // 13. Lambda discard parameters
        Console.WriteLine(constant(null, null));
        Console.WriteLine();

        LocalFunctions();
      
        // CS 9 The two features added for code generators are extensions to partial method syntax, and module initializers
        // ModuleInitializer called without invoking
        int outResult;
        PartialMethods partialMethod = new();
        partialMethod.PartialMethod(out outResult);
        Console.WriteLine($"partial method out parameter:{outResult}");
        Console.WriteLine();
    }
}

class Program
{
    static async Task Main()
    {
        Records rec = new();
        rec.Print();

        InitOnlySetters ios = new();
        ios.Print();

        PatternMatchingEnhancements pem = new();
        pem.Print();

        NativeSizedIntegers native = new();
        native.Print();

        FunctionPointers funPointers = new();
        funPointers.Print();

        Suppresslocalsinit suppress = new();
        suppress.Print();
     
        CS9Features cs9 = new();
        await cs9.Print();       
    }
}


/*
// msys64-mingw64-gcc.c
#include <stdio.h>

void
c_code(void) {
  printf("C!\n");
}
*/

/*
int main() {
  c_code();
}
*/

/*
// D:\RajaniS Master Folder\.NET\CS\CS.NET-Core\CS9\CS9\msys64-mingw64-gcc.dll

// $ gcc -c msys64-mingw64-gcc.c

// $ gcc -shared -o msys64-mingw64-gcc.dll msys64-mingw64-gcc.o


// /Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/msys64-mingw64-gcc.so

% gcc -c -fPIC msys64-mingw64-gcc.c

% gcc -shared -o msys64-mingw64-gcc.so msys64-mingw64-gcc.o
*/


// Output // arch // gcc // dotnet
/*
Last login: Fri Aug 19 11:12:02 on ttys000
rajaniapple@Rajanis-MacBook-Pro ~ % arch                                                               
arm64
rajaniapple@Rajanis-MacBook-Pro ~ % gcc --version                                                      
Apple clang version 13.1.6 (clang-1316.0.21.2.5)
Target: arm64-apple-darwin21.6.0
Thread model: posix
InstalledDir: /Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin
rajaniapple@Rajanis-MacBook-Pro ~ % dotnet --version                                                   
6.0.400
rajaniapple@Rajanis-MacBook-Pro ~ % cd /Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9
rajaniapple@Rajanis-MacBook-Pro CS9 % gcc -c -fPIC msys64-mingw64-gcc.c                                
rajaniapple@Rajanis-MacBook-Pro CS9 % gcc -shared -o msys64-mingw64-gcc.so msys64-mingw64-gcc.o          
rajaniapple@Rajanis-MacBook-Pro CS9 % dotnet run
/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/Program.cs(455,19): warning CS8618: Non-nullable property 'Location' must contain a non-null value when exiting constructor. Consider declaring the property as nullable. [/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/CS9.csproj]
/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/Program.cs(275,22): warning CS8603: Possible null reference return. [/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/CS9.csproj]
/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/Program.cs(276,22): warning CS8603: Possible null reference return. [/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/CS9.csproj]
/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/Program.cs(280,22): warning CS8603: Possible null reference return. [/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/CS9.csproj]
/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/Program.cs(281,22): warning CS8603: Possible null reference return. [/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/CS9.csproj]
/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/Program.cs(536,53): warning CS8625: Cannot convert null literal to non-nullable reference type. [/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/CS9.csproj]
/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/Program.cs(557,81): warning CS8603: Possible null reference return. [/Users/rajaniapple/Desktop/GitHub/CSUpdate/CS/CS/CS9/dotnet6/CS9/CS9.csproj]
15. Module initializers
ModuleInitializerAttribute, static, parameterless, and void

1. Records
record IsValueType: False
record new() IsValueType: False
record IsClass: True

Class Before a = b: a.x = 1, b.x = 2

Class After a = b and a.x = 3: a.x = 3, b.x = 3

Structure Before a = b: a.x = 1, b.x = 2

Structure After a = b and a.x = 3: a.x = 3, b.x = 2

Record Before a = b: a.x = 1, b.x = 2

Record After a = b and a.x = 3: a.x = 3, b.x = 3

Equality is value-based
False
False
True
Aircraft { RealName = Boeing, Nickname = Dreamliner, AircraftClassificationNumber = 787 }
Deconstruct method for positional records
Boeing
Dreamliner
787
Record with-expressions
Vehicle { RealName = Airbus, Nickname = Dreamliner }
Vehicle { RealName = Boeing, Nickname = Superjumbo }
Vehicle { RealName = Airbus, Nickname = Superjumbo }
Aircraft { RealName = Airbus, Nickname = Superjumbo, AircraftClassificationNumber = 380 }
Games { Name = Tokyo 2020 }
Olympics { Name = Tokyo 2020 }

2. Init only setters
Browser = Chromium, Sessions = 3000, with 3.5 pages/session.

4. Pattern matching enhancements
True
False
True
True
False
is not null

5. Native sized integers
nint.MinValue = -9223372036854775808
nuint.MaxValue = 18446744073709551615

6. Function pointers
Marshal: unsafe delegate can refer non-static method
Function pointer to static method
Function pointer to static method: Hello, World!
Function pointer to static method: 1
Function pointer: delegate managed
Function pointer: Marshal: delegate unmanaged
Function pointer: delegate equivalent
delegate equivalent return: 0
Function pointer: delegate equivalent managed round trip
delegate equivalent managed round trip return: 0
Function pointer: delegate managed equi
delegate managed equi return: 0
Function pointer: delegate equi managed round trip
delegate equi managed round trip return: 0

7. Suppress emitting localsinit flag
SkipLocalsInitAttribute for stackalloc: 0 1 2 

8. Target-typed new expressions
Scientia

9. static anonymous functions
static action: 25

10. Target-typed conditional expressions
True 1

11. Covariant return types

12. Extension GetEnumerator support for foreach loops
0 1 2 3 4 5 6 7 8 9 
0 1 
2 3 
4 5 

13. Lambda discard parameters (9. static lambda)

14. Attributes on local functions
AllowNull

DisallowNull: warning CS8625, warning CS8603

static extern in local function
C!

16. New features for partial methods
partial method out parameter:1

rajaniapple@Rajanis-MacBook-Pro CS9 % 
*/


// Credits:
/*
https://dotnet.microsoft.com/
https://gcc.gnu.org/
*/