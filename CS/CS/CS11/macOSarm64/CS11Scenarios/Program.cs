// indexer
/******************************************************************************/
using System;

FailSoftArrayConcrete arrayFailSoft;
Random randomNext = new Random();
int choice = randomNext.Next(1, 4);
Console.WriteLine($"Choice: {choice}");

if (choice == 1)
{
    arrayFailSoft = new(5);
}
else if (choice == 2)
{ 
    arrayFailSoft = new(5, 5);
}
else
{
     arrayFailSoft = new(5, 5, true);
}

int x;

// Show quiet failures
Console.WriteLine("Fail quietly.");

for (int i = 0; i < (arrayFailSoft.Length * 2); i++)
{
    arrayFailSoft[i] = i * 10;
}

for (int i = 0; i < (arrayFailSoft.Length * 2); i++)
{
    x = arrayFailSoft[i];
    if (x != -1)
    {
        Console.Write(x + " ");
    }
}
Console.WriteLine();

// Generate failures
Console.WriteLine("Fail with error reports");
for (int i = 0; i < (arrayFailSoft.Length * 2); i++)
{
    arrayFailSoft[i] = i * 10;
    if (arrayFailSoft.ErrorFlag)
    {
        Console.WriteLine("fs[" + i + "] out-of-bounds");
    }
}

for (int i = 0; i < (arrayFailSoft.Length * 2); i++)
{
    x = arrayFailSoft[i];
    if (!arrayFailSoft.ErrorFlag)
    {
        Console.Write(x + " ");
    }
    else
    {
        Console.WriteLine("fs[" + i + "] out-of-bounds");
    }
}

FailSoftArrayImplementation implementationFailSoftArray;
Console.WriteLine($"Choice: {choice}");

if (choice == 1)
{
    implementationFailSoftArray = new(5);
}
else if (choice == 2)
{
    implementationFailSoftArray = new(5, 5);
}
else
{
    implementationFailSoftArray = new(5, 5, true);
}

// Show quiet failures
Console.WriteLine("Fail quietly.");

for (int i = 0; i < (implementationFailSoftArray.Length * 2); i++)
{
    implementationFailSoftArray[i] = i * 10;
}

for (int i = 0; i < (implementationFailSoftArray.Length * 2); i++)
{
    x = implementationFailSoftArray[i];
    if (x != -1)
    {
        Console.Write(x + " ");
    }
}
Console.WriteLine();

// Generate failures
Console.WriteLine("Fail with error reports");
for (int i = 0; i < (implementationFailSoftArray.Length * 2); i++)
{
    implementationFailSoftArray[i] = i * 10;
    if (implementationFailSoftArray.ErrorFlag)
    {
        Console.WriteLine("fs[" + i + "] out-of-bounds");
    }
}

for (int i = 0; i < (implementationFailSoftArray.Length * 2); i++)
{
    x = implementationFailSoftArray[i];
    if (!implementationFailSoftArray.ErrorFlag)
    {
        Console.Write(x + " ");
    }
    else
    {
        Console.WriteLine("fs[" + i + "] out-of-bounds");
    }
}
Console.WriteLine();
/******************************************************************************/


// event
/******************************************************************************/
EventPublisher publisherEvent = new();
EventClient clientEvent = new();
publisherEvent.SampleEvent += clientEvent.ReceiveSampleEvent;
publisherEvent.SampleEventWithAccessors += clientEvent.ReceiveSampleEvent;
publisherEvent.RaiseSampleEvent();
publisherEvent.RaiseSampleEventWithAccessors();
Console.WriteLine();
/******************************************************************************/


// operator
/******************************************************************************/
OperatorImplementation implementationOperator = new();
Console.WriteLine($"nameof: {nameof(implementationOperator)}");
Console.WriteLine($"GetType: {implementationOperator.GetType()}");
Console.WriteLine($"typeof: {typeof(OperatorImplementation)}");
Console.WriteLine($"typeof GetType: {typeof(OperatorImplementation).GetType()}");
Console.WriteLine($"IsValueType: {typeof(OperatorImplementation).IsValueType}");
for (int i = 0; i < 10; i++)
{
   Console.WriteLine(implementationOperator++);
}
Console.WriteLine();
/******************************************************************************/


// Pattern match Span<char> or ReadOnlySpan<char> on a constant string
/******************************************************************************/
PatternMatchSpan spanPatternMatch = new();
Console.WriteLine("10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string");
spanPatternMatch.Print();
Console.WriteLine();
/******************************************************************************/


// indexer
/******************************************************************************/
interface IIndexer
{

    static int[] array = new int[1];

    int this[int i] { get; set; }

    abstract int this[int i, int j] { get;  set; }

    virtual int this[int i, int j, bool k]  
    {
        get { return array[i]; }
        set { array[i] = value; }
    }
}

class FailSoftArrayImplementation : IIndexer
{
    int[] underlyingArray;
    public int Length;
    public bool ErrorFlag;

    public FailSoftArrayImplementation(int size)
    {
        underlyingArray = new int[size];
        Length = size;
    }

    public FailSoftArrayImplementation(int size, int length)
    {
        underlyingArray = new int[size];
        Length = length;
    }

    public FailSoftArrayImplementation(int size, int length, bool flag)
    {
        underlyingArray = new int[size];
        Length = length;
        ErrorFlag = flag;
    }

    private bool IsWithinBounds(int index)
    {
        if (index >= 0 & index < Length)
        {
            return true;
        }
        return false;
    }

    public int this[int index]
    {
        get
        {
            if (IsWithinBounds(index))
            {
                ErrorFlag = false;
                return underlyingArray[index];
            }
            else
            {
                ErrorFlag = true;
                return 0;
            }
        }
        set
        {
            if (IsWithinBounds(index))
            {
                underlyingArray[index] = value;
                ErrorFlag = false;
            }
            else
            {
                ErrorFlag = true;
            }
        }
    }

    public int this[int index, int zero = 0]
    {
        get
        {
            if (IsWithinBounds(index))
            {
                ErrorFlag = false;
                return underlyingArray[index];
            }
            else
            {
                ErrorFlag = true;
                return zero;
            }
        }
        set
        {
            if (IsWithinBounds(index))
            {
                underlyingArray[index] = value;
                ErrorFlag = false;
            }
            else
            {
                ErrorFlag = true;
            }
        }
    }

    public int this[int index, int zero = 0, bool flag = false]
    {
        get
        {
            if (IsWithinBounds(index))
            {
                ErrorFlag = flag;
                return underlyingArray[index];
            }
            else
            {
                ErrorFlag = !flag;
                return zero;
            }
        }
        set
        {
            if (IsWithinBounds(index))
            {
                underlyingArray[index] = value;
                ErrorFlag = flag;
            }
            else
            {
                ErrorFlag = !flag;
            }
        }
    }
}

abstract class AbstractIndexer
{
    int[] array = new int[1];

    public int this[int i] => 0;

    public abstract int this[int i, int j] { get; set; }

    public virtual int this[int i, int j, bool k]
    {
        get { return array[i]; }
        set { array[i] = value; }
    }
}

class FailSoftArrayConcrete : AbstractIndexer
{
    int[] underlyingArray;
    public int Length;
    public bool ErrorFlag;

    public FailSoftArrayConcrete(int size)
    {
        underlyingArray = new int[size];
        Length = size;
    }

    public FailSoftArrayConcrete(int size, int length)
    {
        underlyingArray = new int[size];
        Length = length;
    }

    public FailSoftArrayConcrete(int size, int length, bool flag)
    {
        underlyingArray = new int[size];
        Length = length;
        ErrorFlag = flag;
    }

    private bool IsWithinBounds(int index)
    {
        if (index >= 0 & index < Length)
        {
            return true;
        }
        return false;
    }

    // new
    public new int this[int index]
    {
        get
        {
            if (IsWithinBounds(index))
            {
                ErrorFlag = false;
                return underlyingArray[index];
            }
            else
            {
                ErrorFlag = true;
                return 0;
            }
        }
        set
        {
            if (IsWithinBounds(index))
            {
                underlyingArray[index] = value;
                ErrorFlag = false;
            }
            else
            {
                ErrorFlag = true;
            }
        }
    }

    // override
    public override int this[int index, int zero = 0]
    {
        get
        {
            if (IsWithinBounds(index))
            {
                ErrorFlag = false;
                return underlyingArray[index];
            }
            else
            {
                ErrorFlag = true;
                return zero;
            }
        }
        set
        {
            if (IsWithinBounds(index))
            {
                underlyingArray[index] = value;
                ErrorFlag = false;
            }
            else
            {
                ErrorFlag = true;
            }
        }
    }

    // override
    public override int this[int index, int zero = 0, bool flag = false]
    {
        get
        {
            if (IsWithinBounds(index))
            {
                ErrorFlag = flag;
                return underlyingArray[index];
            }
            else
            {
                ErrorFlag = !flag;
                return zero;
            }
        }
        set
        {
            if (IsWithinBounds(index))
            {
                underlyingArray[index] = value;
                ErrorFlag = flag;
            }
            else
            {
                ErrorFlag = !flag;
            }
        }
    }
}
/******************************************************************************/


// event
/******************************************************************************/
public class SampleEventArgs : EventArgs // .NET Compliant
{
    public SampleEventArgs(string text) { Text = text; }
    public string Text { get; } // readonly
}

public class EventPublisher
{
    private object objectLock = new object();
    
    // Declare the delegate (if using non-generic pattern).
    public delegate void SampleEventHandler(object sender, SampleEventArgs e);

    // Declare the event.
    public event SampleEventHandler? SampleEvent;

    private SampleEventHandler? SampleEventWithAccessorsPlaceHolder;
    public event SampleEventHandler? SampleEventWithAccessors
    {
        add
        {
            lock (objectLock)
            {
                SampleEventWithAccessorsPlaceHolder += value;
            }
        }
        remove
        {
            lock (objectLock)
            {
                SampleEventWithAccessorsPlaceHolder -= value;
            }
        }
    }

    // On Event
    // Wrap the event in a protected virtual method
    // to enable derived classes to raise the event.
    // protected internal in this scenario
    protected internal virtual void RaiseSampleEvent()
    {
        // Raise the event in a thread-safe manner using the ?. operator.
        SampleEvent?.Invoke(this, new SampleEventArgs("Raise sample event!"));
    }

    // protected internal in this scenario
    protected internal virtual void RaiseSampleEventWithAccessors()
    {
        // Raise the event in a thread-safe manner using the ?. operator.
        SampleEvent?.Invoke(this, new SampleEventArgs("Raise sample event with accessors!"));
    }
}

class EventClient
{
    // EventHandler
    public void ReceiveSampleEvent(object sender, SampleEventArgs args)
    {
        Console.WriteLine($"Event received by {this} object: {args.Text}");
        Console.WriteLine($"Source: {sender}");
    }
}
/******************************************************************************/


// operator
/******************************************************************************/
interface IOperator<T> where T : IOperator<T>
{
    static abstract T operator ++(T operatorI);
}

// The operator receiver type should be a valid record/struct type
// struct OperatorImplementation : IOperator<OperatorImplementation>
record OperatorImplementation : IOperator<OperatorImplementation>
{
    private const char Ch = 'A';

    public string Text = new string(Ch, 1);

    // For struct // A 'struct' with field initializers must include an explicitly declared constructor. 
    // public OperatorImplementation() { }

    public static OperatorImplementation operator ++(OperatorImplementation other)
        => other with { Text = $"{other.Text + ' ' + Ch}" };

    public override string ToString() => Text;
}
/******************************************************************************/


// Pattern match Span<char> or ReadOnlySpan<char> on a constant string
/******************************************************************************/
ref struct RefStructSpan<T>
{
    string literal;

    // A 'struct' with field initializers must include an explicitly declared constructor.
    public RefStructSpan(string literal)
    {
        this.literal = literal;
    }

    public Span<char> CharArray => literal.ToCharArray();

    public Span<char> GetSlice(int start, int length) => CharArray.Slice(start, length);
}

readonly ref struct RefStructReadOnlySpan<T>
{
    readonly string literal;

    // A 'struct' with field initializers must include an explicitly declared constructor.
    public RefStructReadOnlySpan(string literal)
    {
        this.literal = literal;
    }

    public ReadOnlySpan<char> CharArray => literal.ToCharArray();

    public ReadOnlySpan<char> GetSlice(int start, int length) => CharArray.Slice(start, length);
}

class PatternMatchSpan
{
    public void Print()
    {
        string literal = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 1234567890~!@#$%^&*()_+|}{\":?><,./;'[]\\=-`12345123456789012345678901234567";

        Console.WriteLine("10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string: Span<char>");
        // Span<char> spanRef = literal.ToCharArray();
        // Span<char> sliceSpanRef = spanRef.Slice(63, 32);
        RefStructSpan<char> spanRefStruct = new(literal);
        Span<char> sliceSpanRef = spanRefStruct.GetSlice(63, 32);

        Console.WriteLine($"Literal: {literal}");
        Console.WriteLine($"PAttern: {sliceSpanRef.ToString()}");
        Console.WriteLine($"Pattern match Span<char>: {sliceSpanRef is "~!@#$%^&*()_+|}{\":?><,./;'[]\\=-`"}");

        Console.WriteLine("10. Pattern match Span<char> or ReadOnlySpan<char> on a constant string: ReadOnlySpan<char>");
        // ReadOnlySpan<char> spanRefReadOnly = literal.ToCharArray();
        // ReadOnlySpan<char> sliceSpanRefReadOnly = spanRefReadOnly.Slice(63, 32);
        RefStructReadOnlySpan<char> spanRefStructReadOnly = new(literal);
        ReadOnlySpan<char> sliceSpanRefReadOnly = spanRefStructReadOnly.GetSlice(63, 32);

        Console.WriteLine($"Pattern match ReadOnlySpan<char>: {sliceSpanRefReadOnly is "~!@#$%^&*()_+|}{\":?><,./;'[]\\=-`"}");
    }
}

/******************************************************************************/

// abstract class, abstract record, interface
/******************************************************************************/
interface IInterface { }

abstract class AbstractClass : object, IInterface
{
    static AbstractClass() { }
    public AbstractClass() { }
    ~AbstractClass() { }
    public abstract void AbstractMethod();
}

class ExtendAbstractClass : AbstractClass, IInterface
{
    static ExtendAbstractClass() { }
    public ExtendAbstractClass() : base() { }
    ~ExtendAbstractClass() { }
    public override void AbstractMethod() { }
}

abstract class AbstractExtendAbstractClass : AbstractClass, IInterface
{
    static AbstractExtendAbstractClass() { }
    public AbstractExtendAbstractClass() : base() { }
    ~AbstractExtendAbstractClass() { }
    public override void AbstractMethod() { }
}

class ConcreteClassBase : object, IInterface
{
    static ConcreteClassBase() { }
    public ConcreteClassBase() { }
    ~ConcreteClassBase() { }
    public virtual void VirtualMethod() { }
}

abstract class AbstractExtendConcreteBaseClass : ConcreteClassBase, IInterface
{
    static AbstractExtendConcreteBaseClass() { }
    public AbstractExtendConcreteBaseClass() : base() { }
    ~AbstractExtendConcreteBaseClass() { }
    public override void VirtualMethod() { }
}

// abstract record
abstract record AbstractRecord : object, IInterface
{
    static AbstractRecord() { }
    public AbstractRecord() { }
    ~AbstractRecord() { }
    public abstract void AbstractMethod();
}

record ExtendAbstractRecord : AbstractRecord, IInterface
{
    static ExtendAbstractRecord() { }
    public ExtendAbstractRecord() : base() { }
    ~ExtendAbstractRecord() { }
    public override void AbstractMethod() { }
}

abstract record AbstractExtendAbstractRecord : AbstractRecord, IInterface
{
    static AbstractExtendAbstractRecord() { }
    public AbstractExtendAbstractRecord() : base() { }
    ~AbstractExtendAbstractRecord() { }
    public override void AbstractMethod() { }
}

record ConcreteBaseRecord : object, IInterface
{
    static ConcreteBaseRecord() { }
    public ConcreteBaseRecord() { }
    ~ConcreteBaseRecord() { }
    public virtual void VirtualMethod() { }
}

abstract record AbstractExtendConcreteBaseRecord : ConcreteBaseRecord, IInterface
{
    static AbstractExtendConcreteBaseRecord() { }
    public AbstractExtendConcreteBaseRecord() : base() { }
    ~AbstractExtendConcreteBaseRecord() { }
    public override void VirtualMethod() { }
}
/******************************************************************************/


// NB
// Resource management techniques
// Declare a readonly struct to express that a type is immutable and enables the compiler to save copies when using in parameters.
// Use a ref readonly return when the return value is a struct larger than IntPtr.Size and the storage lifetime is greater than the method returning the value.
// When the size of a readonly struct is bigger than IntPtr.Size, you should pass it as an in parameter for performance reasons.
// Never pass a struct as an in parameter unless it's declared with the readonly modifier because it may negatively affect performance and could lead to an obscure behavior.
// Use a ref struct, or a readonly ref struct such as Span<T> or ReadOnlySpan<T> to work with memory as a sequence of bytes.

struct Point3D
{
    public Point3D(int i, int j, int k) { }
    private static Point3D origin = new Point3D(0, 0, 0);
    // Dangerous! returning a mutable reference to internal storage
    public ref Point3D Origin => ref origin; // By-value returns may only be used in methods that return by value.
}

// NB
// Using value types minimizes the number of allocation operations:
// Storage for value types is stack allocated for local variables and method arguments.
// Storage for value types that are members of other objects is allocated as part of that object, not as a separate allocation.
// Storage for value type return values is stack allocated.
// Contrast that with reference types in those same situations:
// Storage for reference types are heap allocated for local variables and method arguments. The reference is stored on the stack.
// Storage for reference types that are members of other objects are separately allocated on the heap. The containing object stores the reference.
// Storage for reference type return values is heap allocated. The reference to that storage is stored on the stack.


// Scenarios
/******************************************************************************/
// Scenarios for struct S with fields int x, y, and method void M()
/*
struct S
{
    int x, y;

    // Scenario 1
    public S()
    {
        // ok. Compiler inserts an assignment of `this = default`.
    }

    // Scenario 2
    public S()
    {
        // ok. Compiler inserts an assignment of `y = default`.
        x = 1;
    }

    // Scenario 3
    public S()
    {
        // valid since C# 1.0. Compiler inserts no implicit assignments.
        x = 1;
        y = 2;
    }

    // Scenario 4
    public S(bool b)
    {
        // ok. Compiler inserts assignment of `this = default`.
        if (b)
        {
            x = 1;
        }
        else
        {
            y = 2;
        }
    }

    // Scenario 5
    void M() { }
    public S(bool b)
    {
        // ok. Compiler inserts assignment of `y = default`.
        x = 1;
        if (b)
        {
            M();
        }
        y = 2;
    }
} 
*/
/******************************************************************************/


// Credit:
/*
https://dotnet.microsoft.com/
*/