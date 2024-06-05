// CS 1.x

/*
Unified Type System - Data Types
|
|`-Value Types
| |
| |`--Built-in Value Types
| | |
| |  `---Built-in Structs
| |   |
| |   |`----Numeric Types
| |   | |
| |   |  `-----Integral Types
| |   |   |
| |   |   |`------sbyte
| |   |   |
| |   |   |`------byte
| |   |   |
| |   |   |`------short
| |   |   |
| |   |   |`------ushort
| |   |   |
| |   |   |`------int
| |   |   |
| |   |   |`------uint
| |   |   |
| |   |   |`------long
| |   |   |
| |   |   |`------ulong
| |   |   |
| |   |   |`------char
| |   |   |
| |   |   |`-----Floating-point Types
| |   |   |  |
| |   |   |  |`------float
| |   |   |  |
| |   |   |   `------double
| |   |   |
| |   |    `-----decimal
| |   |
| |    `----bool
| |
|  `--User-defined Value Types
|   |
|   |`---User-defined Structs [Auto-default Structs (>= CS 11)]
|   |    |
|   |     `---readonly struct (>= CS 7.2)
|   |    |
|   |     `---ref struct (>= CS 7.2)
|   |    |
|   |     `---readonly ref struct (>= CS 7.2)
|   |    |
|   |     `---record struct (>= CS 10)
|   |    |
|   |     `---readonly record struct (>= CS 10)
|   |
|    `---Enums
|     
|`-Reference Types
| |
| |`--User-defined Reference Types
| | |
| | |`---class
| | |
| | |`---interface
| | |
| | |`---delegate
| | |
| |  `---Array
| | |
| |  `---record (>= CS 9)
| |   |
| |    `---positional record (>= CS 9)
| |
|  `--Built-in Reference Types
|   |
|   |`---object
|   |
|   |`---string
|   |
|    `---dynamic (>= CS 4)
|
 `-Pointer Types (Unsafe)


Type Members
|
|`-data members (variables and consts can be local)
| |
| |`--consts
| |
| |`--static fields (including static volatile and static readonly) 
| |
|  `--instance fields (including instance volatile and instance readonly)
|
 `-function members
  |
  |`--methods (instance/static)
  |
  |`--construcutors (instance/static(which must be parameterless))
  |
  |`--destructors (called prior to garbage collection before program terminates [not called when object goes out of scope and differs from C++ destructors that are called when object goes out of scope]) 
  |
  |`--indexers (instance, cannot return void)
  |
  |`--properties (instance/static, cannot return void)
  |
  |`--events (instance/static)
  |
   `--operators (static)
*/


using System;
using System.Reflection;

enum Apple { Jonathan, GoldenDel, RedDel, Winesap, Cortland, McIntosh }; 

struct Structure  
{
    public string author;
    public string title;
    public int copyright;

    public Structure(string a, string t, int c)
    {
        author = a;
        title = t;
        copyright = c;
    }
}

class ValueTypes
{
    private sbyte sbyteMin;
    private sbyte sbyteMax;
    private byte byteMin;
    private byte byteMax;
    private short shortMin;
    private short shortMax;
    private ushort ushortMin;
    private ushort ushortMax;
    private int intMin;
    private int intMax;
    private uint uintMin;
    private uint uintMax;
    private long longMin;
    private long longMax;
    private ulong ulongMin;
    private ulong ulongMax;
    private float floatMin;
    private float floatMax;
    private double doubleMin;
    private double doubleMax;
    private decimal decimalMin;
    private decimal decimalMax;
    private char charMax;
    private char charMin;    

    private void BuiltinValueTypes()
    {
        sbyteMin = sbyte.MinValue;
        Console.WriteLine("sbyte Minimum Value: " + sbyteMin);

        sbyteMax = sbyte.MaxValue;
        Console.WriteLine("sbyte Maximum Value: " + sbyteMax);

        byteMin = byte.MinValue;
        Console.WriteLine("byte Minimum Value: " + byteMin);

        byteMax = byte.MaxValue;
        Console.WriteLine("byte Maximum Value: " + byteMax);

        shortMin = short.MinValue;
        Console.WriteLine("short Minimum Value: " + shortMin);

        shortMax = short.MaxValue;
        Console.WriteLine("short Maximum Value: " + shortMax);

        ushortMin = ushort.MinValue;
        Console.WriteLine("ushort Minimum Value: " + ushortMin);

        ushortMax = ushort.MaxValue;
        Console.WriteLine("ushort Maximum Value: " + ushortMax);

        intMin = int.MinValue;
        Console.WriteLine("int Minimum Value: " + intMin);

        intMax = int.MaxValue;
        Console.WriteLine("int Maximum Value: " + intMax);

        uintMin = uint.MinValue;
        Console.WriteLine("uint Minimum Value: " + uintMin);

        uintMax = uint.MaxValue;
        Console.WriteLine("uint Maximum Value: " + uintMax);

        longMin = long.MinValue;
        Console.WriteLine("long Minimum Value: " + longMin);

        longMax = long.MaxValue;
        Console.WriteLine("long Maximum Value: " + longMax);

        ulongMin = ulong.MinValue;
        Console.WriteLine("ulong Minimum Value: " + ulongMin);

        ulongMax = ulong.MaxValue;
        Console.WriteLine("ulong Maximum Value: " + ulongMax);

        floatMin = float.MinValue;
        Console.WriteLine("float Minimum Value: " + floatMin);

        floatMax = float.MaxValue;
        Console.WriteLine("float Maximum Value: " + floatMax);

        doubleMin = double.MinValue;
        Console.WriteLine("double Minimum Value: " + doubleMin);

        doubleMax = double.MaxValue;
        Console.WriteLine("double Maximum Value: " + doubleMax);

        decimalMin = decimal.MinValue;
        Console.WriteLine("decimal Minimum Value: " + decimalMin);

        decimalMax = decimal.MaxValue;
        Console.WriteLine("decimal Maximum Value: " + decimalMax);

        charMin = char.MinValue;
        Console.WriteLine("char Minimum Value: " + charMin);

        charMax = char.MaxValue;
        Console.WriteLine("char Maximum Value: " + charMax);
    }

    private void UserdefinedValueTypes()
    {
        string[] color = {"Green", "Blue", "Red", "Reddish Green", "Reddish Blue", "Greenish Blue"};
       
        for(Apple aapl = Apple.Jonathan; aapl <= Apple.McIntosh; aapl++)
        { 
            Console.WriteLine(aapl + " has value of " + (int)aapl); 
        }

        for(Apple aapl = Apple.Jonathan; aapl <= Apple.McIntosh; aapl++)
        {
            Console.WriteLine(aapl + " has color of " + color[(int)aapl]);
        }
    }

    private void UserdefinedStructure()
    {
        Structure strct = new Structure("Herbert Schildt", "C#: Complete Reference", 2002);
        Console.WriteLine(strct.title + " by " + strct.author + ", (c) " + strct.copyright);
    }

    public void Print()
    {
        BuiltinValueTypes();
        UserdefinedValueTypes();
        UserdefinedStructure();
    }
}

interface IInterface
{
    void InterfaceMedthod(string s);
}

class ImplementingInterface : IInterface
{
    public void InterfaceMedthod(string s)
    {
        Console.Write("Implementing " + s);
    } 

    public void Print()
    {
       InterfaceMedthod("Interface!");
       Console.WriteLine();
    }
}

delegate string DelegateSignature(string s);

public class Delegates
{
   private string Uppercase(string s)
   {
      return s.ToUpper();
   }

   public void Print()
   {
      // < CS 2
      DelegateSignature del = new DelegateSignature(Uppercase);
      string message = "Hello, World!";
      Console.WriteLine(del(message));
   }
}

public class Arrays
{
    private void PrintValuesUsingIndex(Array arry) 
    {
        for (int i = arry.GetLowerBound(0); i <= arry.GetUpperBound(0); i++)
        {
            Console.Write(arry.GetValue(i));            
        }
        Console.WriteLine();
   }

    public void Print()
    {
        string message = "Hello, World!";
        Array arry = Array.CreateInstance(typeof(char), message.Length);
       
        for(int i = 0; i < message.Length; i++) 
        {
            arry.SetValue(message[i], i);
        }
        Console.WriteLine("Original string:");
        PrintValuesUsingIndex(arry);
        Array.Reverse(arry);
        Console.WriteLine("Reversed string:");
        PrintValuesUsingIndex(arry);
    }
}

struct StructureContainer
{
    public int number;
}

struct StructureAssignment
{
    public void Print()
    {
        StructureContainer x;
        StructureContainer y;
        x.number = 10;
        y.number = 20;
        Console.WriteLine("x.number {0}, y.number {1}", x.number, y.number);
        Console.WriteLine("Copy struct");
        x = y;
        x.number = 30;
        Console.WriteLine("x.number {0}, y.number {1}", x.number, y.number);    }
}

class ClassContainer 
{
    public int number;
}

class ClassAssignment
{
    public void Print()
    {
        ClassContainer x = new ClassContainer();;
        ClassContainer y = new ClassContainer();
        x.number = 10;
        y.number = 20;
        Console.WriteLine("x.number {0}, y.number {1}", x.number, y.number);
        Console.WriteLine("Copy class");
        x = y;
        x.number = 30;
        Console.WriteLine("x.number {0}, y.number {1}", x.number, y.number);    }
}

class BoxingUnboxing 
{
    public void Print()
    {
        int x;
        x = 10;
        Console.WriteLine("Boxing and unboxing using object");
        object ob; // System.Object
        ob = x; // boxing
        int y = (int)ob; // unboxing using explicit cast
        Console.WriteLine(y);
    }
}

class StringReverser
{
    private string ReverseString(string s)
    {
        char[] arry = s.ToCharArray();
        Array.Reverse(arry);
        return new string(arry);
    }

    public void Print()
    {
        string message = "Hola!";
        Console.WriteLine("Original string:");
        Console.WriteLine(message);
        Console.WriteLine("Reversed string:");
        Console.WriteLine(ReverseString(message));
    } 
}

class Maths
{
    public int IntegerSquare(int integerNumber)
    {
        return integerNumber * integerNumber;
    }
}

/*
// >= CS 4
class DynamicBinding
{
    public void Print()
    {
        // class
        object instance = new Maths();
        Type instanceType = instance.GetType();
        object result = instanceType.InvokeMember("IntegerSquare", BindingFlags.InvokeMethod, null, instance, new object[] { 1 });

        int number = (int)result;
        Console.WriteLine("Number using Reflection: " + number);
 
        // Dynamic Binding
        dynamic runtime = new Maths();
        number = runtime.IntegerSquare(2);
        Console.WriteLine("Number using dynamic: " + number);
    }
}
*/

/*
// Pointer unsafe
class UnsafePointer 
{
    // unsafe
    public unsafe void Print() 
    {
        int count = 555;
        int* pointer; // int Pointer // Mark Main as unsafe
        pointer = &count; // Assign address of count to pointer
        Console.WriteLine("Initial value of count is " + *pointer);
        *pointer = 777; // assign 777 to count via pointer
        Console.WriteLine("New value of count is " + *pointer);
    }
}
*/

class Members
{
    public const int constantNumber = 1;
    public static int statNumber = 2;
    public static volatile int statVolatileNumber = 3;
    public static readonly int statReadonlyNumber = 4;
    public int number = 5;
    public volatile int volatileNumber = 6;
    public readonly int readonlyNumber = 7;
    private string propertyName = "Hello, World!";
    public string[] arry = new string[8];

    static Members()
    {
        Console.WriteLine("static Constructor");
        Console.WriteLine("statReadonlyNumber is assigned in static Constructor");
        statReadonlyNumber = int.MaxValue;
    }

    public Members()
    {
        Console.WriteLine("Constructor");
        Console.WriteLine("readonlyNumber is assigned in Constructor");
        readonlyNumber = int.MaxValue;
    }

    ~Members()
    {
        Console.WriteLine("Destructor");
    }


    public string Property
    {
        get { return propertyName; }
        set { propertyName = value; }
    }

    public static void StatMethod()
    {
       Console.WriteLine("static Method");
    }

    public void Method()
    {
       Console.WriteLine("Method");
    }

    public string this[int i]
    {
       get { return arry[i]; }
       set { arry[i] = value; }
    }
}

class MembersClient
{
    public void Print()
    {
        Members member = new Members();
        Console.WriteLine("constantNumber = {0}", Members.constantNumber);        
        Console.WriteLine("statNumber = {0}", Members.statNumber);       
        Console.WriteLine("statVolatileNumber = {0}", Members.statVolatileNumber);
        Console.WriteLine("statReadonlyNumber = {0}", Members.statReadonlyNumber);
        Console.WriteLine("number = {0}", member.number);
        Console.WriteLine("volatileNumber = {0}", member.volatileNumber);
        Console.WriteLine("readonlyNumber = {0}", member.readonlyNumber);

        Members.StatMethod();
        member.Method();

        member[0] = "string array-like";
        Console.WriteLine("Indexer value is this: {0}" , member[0]);
    }
}

// .NET-compatible event
class EventArguments : EventArgs
{
    public int eventnumber;  
}

delegate void EventHandlers(object sender, EventArguments args);

class EventContainer
{
    private static int count = 0;

    public event EventHandlers RegisterEventHandler;

    public void OnRegisterEventHandler()
    {
        EventArguments eventArgs = new EventArguments();

        if(RegisterEventHandler != null)
        {
            eventArgs.eventnumber = count++; 
            RegisterEventHandler(this, eventArgs);        
            
        }
    }
}

class X
{
    public void XEventHandler(object sender, EventArguments args)
    {
        Console.WriteLine("Event #" + args.eventnumber + " received by X object");
        Console.WriteLine("Source: " + sender);
    }
}
           
class Y
{
    public void YEventHandler(object sender, EventArguments args)
    {
        Console.WriteLine("Event #" + args.eventnumber + " received by Y object");
        Console.WriteLine("Source: " + sender);
    }
}    
    
class EventClient
{
    public void Print()
    {
        EventContainer eventContain = new EventContainer();
        X x = new X();
        Y y = new Y();
        eventContain.RegisterEventHandler += x.XEventHandler;
        eventContain.RegisterEventHandler += y.YEventHandler; 
        eventContain.OnRegisterEventHandler();
        eventContain.OnRegisterEventHandler();
    }
}

// operator overloading // binary operators +, - // unary operator -, ++
class OperatorOverloading
{
    int x;
    int y;
    int z;

    public OperatorOverloading()
    {
        x = 0;
        y = 0;
        z = 0;
    }

    public OperatorOverloading(int a, int b, int c)
    {
        x = a;
        y = b;
        z = c;
    }

    public static OperatorOverloading operator +(OperatorOverloading operator1, OperatorOverloading operator2) // adding objects
    {
        OperatorOverloading operatorOverload = new OperatorOverloading();
         
        operatorOverload.x = operator1.x + operator2.x;
        operatorOverload.y = operator1.y + operator2.y;
        operatorOverload.z = operator1.z + operator2.z;
    
        return operatorOverload;
    }

    public static OperatorOverloading operator -(OperatorOverloading operator1, OperatorOverloading operator2) // subtracting objects
    {
        OperatorOverloading operatorOverload = new OperatorOverloading();
         
        operatorOverload.x = operator1.x - operator2.x;
        operatorOverload.y = operator1.y - operator2.y;
        operatorOverload.z = operator1.z - operator2.z;
    
        return operatorOverload;
    }

    public static OperatorOverloading operator -(OperatorOverloading operator1) // receiving negation of object
    {
        OperatorOverloading operatorOverload = new OperatorOverloading();
         
        operatorOverload.x = - operator1.x;
        operatorOverload.y = - operator1.y;
        operatorOverload.z = - operator1.z;
    
        return operatorOverload;
    }

    public static OperatorOverloading operator ++(OperatorOverloading operator1) // incrementing object
    {
        // OperatorOverloading operatorOverload = new OperatorOverloading();
         
        operator1.x++;
        operator1.y++;
        operator1.z++;
    
        return operator1;
    }    

    public static OperatorOverloading operator --(OperatorOverloading operator1) // decrementing object
    {
        // OperatorOverloading operatorOverload = new OperatorOverloading();
         
        operator1.x--;
        operator1.y--;
        operator1.z--;
    
        return operator1; 
    }  


    public void Method()
    {
        Console.WriteLine("x = {0}, y = {1}, z = {2}", x, y, z);
    }
}

class OperatorOverloadingClient
{
    public void Print()
    {
        OperatorOverloading operatorOverload1 = new OperatorOverloading(1, 2, 3);
        OperatorOverloading operatorOverload2 = new OperatorOverloading(10, 20, 30);
        OperatorOverloading operatorOverload3 = new OperatorOverloading();
        
        Console.WriteLine("Object operatorOverload1:");
        operatorOverload1.Method();
        Console.WriteLine("Object operatorOverload2:");
        operatorOverload2.Method();
        Console.WriteLine("Object operatorOverload3:");
        operatorOverload3.Method();

        operatorOverload3 = operatorOverload1 + operatorOverload2; // adding objects
        Console.WriteLine("Adding objects operatorOverload3 = operatorOverload1 + operatorOverload2:");
        operatorOverload3.Method();   

        operatorOverload3 = operatorOverload1 + operatorOverload2 + operatorOverload3; // adding objects
        Console.WriteLine("Adding objects operatorOverload3 = operatorOverload1 + operatorOverload2 + operatorOverload3:");
        operatorOverload3.Method();   
    
        operatorOverload3 = operatorOverload3 - operatorOverload1; // subtracting objects
        Console.WriteLine("Subtracting objects operatorOverload3 = operatorOverload3 - operatorOverload1:");
        operatorOverload3.Method();   

        operatorOverload3 = operatorOverload3 - operatorOverload2; // subtracting objects
        Console.WriteLine("Subtracting objects operatorOverload3 = operatorOverload3 - operatorOverload2:");
        operatorOverload3.Method();   
        
        operatorOverload3 = -operatorOverload1; // receiving negation of object
        Console.WriteLine("Negation of object operatorOverload3 = -operatorOverload1:");
        operatorOverload2.Method();   

        operatorOverload1++; // so : ++operatorOverload1 // incrementing object
        Console.WriteLine("Incrementing object operatorOverload1++:");
        operatorOverload1.Method();   

        operatorOverload1--; // so : --operatorOverload1 // decrementing object
        Console.WriteLine("Decrementing object operatorOverload1--:");
        operatorOverload1.Method();         
    }
}

class Program
{   
    // Pointer unsafe // csc CS1.cs /unsafe
    // unsafe static void Main()
    static void Main()
    {
        ValueTypes valTypes = new ValueTypes();
        valTypes.Print();

        ImplementingInterface implementInterface = new ImplementingInterface();
        implementInterface.Print();

        Delegates del = new Delegates();
        del.Print();

        Arrays arrys = new Arrays();
        arrys.Print();

        StructureAssignment structureAssign = new StructureAssignment();
        structureAssign.Print();

        ClassAssignment classAssign = new ClassAssignment();
        classAssign.Print(); 

        BoxingUnboxing boxUnbox = new BoxingUnboxing();
        boxUnbox.Print();

        StringReverser stringReverse = new StringReverser();
        stringReverse.Print();

        /*
        // >= CS 4
        DynamicBinding dynamicBind  = new DynamicBinding();
        dynamicBind.Print();
        */

        /*
        // unsafe
        UnsafePointer unsafePoint = new UnsafePointer();
        unsafePoint.Print();
        */

        MembersClient memberClient = new MembersClient();
        memberClient.Print();

        EventClient evntClient = new EventClient();
        evntClient.Print();

        OperatorOverloadingClient operatorOverloadClient = new OperatorOverloadingClient();
        operatorOverloadClient.Print();
    }
}


// Credit:
/*
https://dotnet.microsoft.com/
*/