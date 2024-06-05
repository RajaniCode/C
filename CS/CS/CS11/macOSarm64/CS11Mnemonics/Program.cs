// CS 11 // Subject to updates in newer versions of CS


// Version
/******************************************************************************/
using static System.Runtime.InteropServices.JavaScript.JSType;

var os = Environment.OSVersion;
Console.WriteLine("OS Information:");
Console.WriteLine("Platform: {0:G}", os.Platform);
Console.WriteLine("Version String: {0}", os.VersionString);
Console.WriteLine("Version Information:");
Console.WriteLine("   Major: {0}", os.Version.Major);
Console.WriteLine("   Minor: {0}", os.Version.Minor);
Console.WriteLine("Service Pack: '{0}'", os.ServicePack);

// Environment.Version property returns the .NET runtime version for .NET 5+ and .NET Core 3.x
// Not recommend for .NET Framework 4.5+
Console.WriteLine($"Environment.Version: {Environment.Version}");
// RuntimeInformation.FrameworkDescription property gets the name of the .NET installation on which an app is running
// .NET 5+ and .NET Core 3.x // .NET Framework 4.7.1+ // Mono 5.10.1+
Console.WriteLine($"RuntimeInformation.FrameworkDescription: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}");
Console.WriteLine();
/******************************************************************************/


// interface, static, generic interface, static abstract, static virtual
/******************************************************************************/
Console.WriteLine(IStatic.Constant);
Console.WriteLine(IStatic.StaticField);
Console.WriteLine(IStatic.StaticReadOnlyField);
Console.WriteLine(IStatic.StaticVolatileField);

Console.WriteLine(IStatic.StaticProperty);
IStatic staticI = new StaticImplementation();
Console.WriteLine(staticI.InstanceProperty);
Console.WriteLine(staticI.AbstractProperty);
Console.WriteLine(staticI.VirtualProperty);
IStatic.StaticMethod();
staticI.InstanceMethod();
staticI.AbstractMethod();
staticI.VirtualMethod();

// CS 11
// A static virtual or abstract interface member can be accessed only on a type parameter
// Console.WriteLine(IStaticGeneric<StaticGenericImplementation>.StaticAbstractProperty);
// IStaticGeneric<StaticGenericImplementation>.StaticAbstractMethod();
// Console.WriteLine(IStaticGeneric<StaticGenericImplementation>.StaticVirtualProperty);
//IStaticGeneric<StaticGenericImplementation>.StaticVirtualMethod();
Console.WriteLine(StaticGenericImplementation.StaticAbstractProperty);
StaticGenericImplementation.StaticAbstractMethod();
Console.WriteLine(StaticGenericImplementation.StaticVirtualProperty);
StaticGenericImplementation.StaticVirtualMethod();

// Note IStatic has no setter for the virtual indexer this[int i, int j, int k], otherwise the interface instance staticI can also be indexed
StaticImplementation implementationStatic = new();
// Random rand = new Random();
// .NET	6+
// Provides a thread-safe Random instance that may be used concurrently from any thread.
Random rand = Random.Shared;

// Elements 2
for (int i = 0; i < 2; i++)
{
    staticI[i] = rand.Next();
    // implementationStatic[i] = rand.Next();
}
for (int i = 0; i < 2; i++)
{
    Console.WriteLine($"Element #{i} = {staticI[i]}");
    // Console.WriteLine($"Element #{i} = {implementationStatic[i]}");
}

// Elements 2 x 3
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 3; j++)
    {
        staticI[i, j] = rand.Next();
        // implementationStatic[i, j] = rand.Next();
    }
}
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 3; j++)
    {
        Console.WriteLine($"Element #{i}, {j} = {staticI[i, j]}");
        // Console.WriteLine($"Element #{i}, {j} = {implementationStatic[i, j]}");
    }
}

// Elements 2 x 3 x 4
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 3; j++)
    {
        for (int k = 0; k < 4; k++)
        {
            staticI[i, j, k] = rand.Next();
            // implementationStatic[i, j, k] = rand.Next();
        }
    }
}
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 3; j++)
    {
        for (int k = 0; k < 4; k++)
        {
            Console.WriteLine($"Element #{i}, {j}, {k} = {staticI[i, j, k]}");
            // Console.WriteLine($"Element #{i}, {j}, {k} = {implementationStatic[i, j, k]}");
        }
    }
}

EventClient clientEvent = new();
IStatic.StaticEvent += clientEvent.ReceiveInterfaceEvent;
IStatic.StaticEventWithAccessors += clientEvent.ReceiveInterfaceEvent;
staticI.RaisetStaticEvent();
staticI.RaisetStaticEventWithAccessors();

implementationStatic.InstanceEvent += clientEvent.ReceiveInterfaceEvent;
implementationStatic.InstanceEventPlaceHolderExplicit += clientEvent.ReceiveInterfaceEvent;
implementationStatic.RaiseImplicitInstanceeEvent();
implementationStatic.RaiseExplicitInstanceeEvent();


// CS 11
// A static virtual or abstract interface member can be accessed only on a type parameter
// IStaticGeneric<StaticGenericImplementation>.StaticAbstractEvent += clientEvent.ReceiveInterfaceEvent;
// IStaticGeneric<StaticGenericImplementation>.StaticVirtualEvent += clientEvent.ReceiveInterfaceEvent;
// IStaticGeneric<StaticGenericImplementation>.StaticVirtualEventWithAccessors += clientEvent.ReceiveInterfaceEvent;
StaticGenericImplementation implementationStaticGeneric = new();
StaticGenericImplementation.StaticAbstractEvent += clientEvent.ReceiveInterfaceEvent;
StaticGenericImplementation.StaticAbstractEventPlaceHolderExplicit += clientEvent.ReceiveInterfaceEvent;
implementationStaticGeneric.RaisetImplicitStaticAbstractEvent();
implementationStaticGeneric.RaisetExplicitStaticAbstractEvent();

StaticGenericImplementation.StaticVirtualEvent += clientEvent.ReceiveInterfaceEvent;
StaticGenericImplementation.StaticVirtualEventPlaceHolderExplicit += clientEvent.ReceiveInterfaceEvent;
implementationStaticGeneric.RaisetImplicitStaticVirtualEvent();
implementationStaticGeneric.RaisetExplicitStaticVirtualEvent();


implementationStaticGeneric.StaticVirtualEventWithAccessors += clientEvent.ReceiveInterfaceEvent;
StaticGenericImplementation.StaticVirtualEventWithAccessorsPlaceHolderImplementationExplicit += clientEvent.ReceiveInterfaceEvent;
implementationStaticGeneric.RaisetImplicitStaticVirtualEventWithAccessors();
implementationStaticGeneric.RaisetExplicitStaticVirtualEventWithAccessors();


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
/******************************************************************************/


// interface, static, generic interface, static abstract, static virtual
/******************************************************************************/
class InterfaceEventArgs : EventArgs // .NET Compliant
{
    public InterfaceEventArgs(string text) { Text = text; }
    public string Text { get; } // readonly
}

delegate void InterfaceEventHandler(object sender, InterfaceEventArgs e);

class EventClient
{
    public void ReceiveInterfaceEvent(object sender, InterfaceEventArgs args)
    {
        Console.WriteLine($"Event received by {this} object: {args.Text}");
        Console.WriteLine($"Source: {sender}");
    }
}

interface IStatic
{
    // NB
    // For interface data/function members, hiding will be intended only in the inheriting inteface, however not in the implementing type viz. class, struct, or record

    // interface data members
    // const
    // static field

    // Interface members are public by default because the purpose of an interface is to enable other types to access a class or struct.
    // CS 8
    // Interface member declarations may include any access modifier viz. public, protected, internal, protected internal, private protected, private.
    // This is most useful for static methods to provide common implementations needed by all implementors of a class.
    const string Constant = "const in interface";
    static string StaticField;
    static readonly string StaticReadOnlyField;
    static volatile string StaticVolatileField;

    // interface function members
    // static constructor
    // static/instance/abstract/virtual property
    // static/instance/abstract/virtual method
    // instance/abstract/virtual indexer
    // event
    // operator

    static IStatic()
    {
        StaticField = "static feild in interface";
        StaticReadOnlyField = "static readonly feild in interface";
        StaticVolatileField = "static volatile feild in interface";
        StaticProperty = "static property in interface";
    }


    // not found among members of the interface that can be implemented
    // although the implementing type can have simililar static property with the same name
    // interface static property can have initializers
    static string StaticProperty { get; set; } = "interface static property can have initializers";

    // interface instance property must be (implicitly or explicitly) implemented in the implementing type
    // interface instance property cannot have initializers
    string InstanceProperty { get; set; }

    // interface abstract property must be (implicitly or explicitly) implemented in the implementing type without the override keyword
    abstract string AbstractProperty { get; set; }

    // optional to implement interface virtual property (implicitly or explicitly) in the implementing type, however without the override keyword if implemented 
    //virtual string VirtualProperty =>  "interface virtual property can have initializers without explict accessors";
    static string setValue = "interface virtual property can have initializers";
    virtual string VirtualProperty
    {
        get { return setValue; }
        set { setValue = value; }
    }

    // CS 8
    // (default) static method implementation
    static void StaticMethod()
    {
        const string localConstant = "local const in interface (default) static method"; ;
        Console.WriteLine(localConstant);
    }

    // CS 8
    // (default) instance method implementation
    void InstanceMethod()
    {
        const string localConstant = "local const in interface (default) instance method"; ;
        Console.WriteLine(localConstant);
    }

    // interface abstract method must be implemented (implicitly or explicitly) in the implementing type without the override keyword
    abstract void AbstractMethod();

    // optional to implement interface virtual method (implicitly or explicitly) in the implementing type, however without the override keyword if implemented 
    virtual void VirtualMethod()
    {
        const string localConstant = "local const in interface virtual method"; ;
        Console.WriteLine(localConstant);
    }


    // NB
    // modifier 'static' is not valid for 'this'
    // interface instance indexer must be (implicitly or explicitly) implemented in the implementing type 
    int this[int i] { get; set; }
    // interface abstract indexer must be (implicitly or explicitly) implemented in the implementing type without the override keyword
    abstract int this[int i, int j] { get; set; }
    // optional to implement interfaace virtual indexer (implicitly or explicitly) in the implementing type, however without the override keyword if implemented 
    static int[, ,] array = new int[1, 1, 1];
    virtual int this[int i, int j, int k]
    {
        get { return array[i, j, k]; }
        set { array[i, j, k] = i; }
    }

    // not found among members of the interface that can be implemented
    static event InterfaceEventHandler? StaticEvent;

    static event InterfaceEventHandler? StaticEventWithAccessorsPlaceHolder;
    static event InterfaceEventHandler? StaticEventWithAccessors
    {
        add
        {
            StaticEventWithAccessorsPlaceHolder += value;
        }
        remove
        {
            StaticEventWithAccessorsPlaceHolder -= value;
        }
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    void RaisetStaticEvent()
    {
        StaticEvent?.Invoke(this, new InterfaceEventArgs("Raise interface static event in the interface!"));
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    void RaisetStaticEventWithAccessors()
    {
        StaticEventWithAccessorsPlaceHolder?.Invoke(this, new InterfaceEventArgs("Raise interface static event with accessors in the interface!"));
    }

    // interface instance event must be (implicitly or explicitly) implemented in the implementing type
    event InterfaceEventHandler? InstanceEvent;

    // not found among members of the interface that can be implemented
    static IStatic operator ++(IStatic staticI) => staticI;
}

// CS 11
// The static virtual and static abstract methods declared in interfaces don't have a runtime dispatch mechanism analogous to virtual or abstract methods declared in classes.
// Instead, the compiler uses type information available at compile time.
// Therefore, static virtual methods are almost exclusively declared in generic interfaces.
// Furthermore, most interfaces that declare static virtual or static abstract methods declare that one of the type parameters must implement the declared interface. 
interface IStaticGeneric<T> where T : IStaticGeneric<T>
{
    // interface function members
    // static abstract/static virtual property
    // static abstract/static virtual method

    // CS 11
    // static abstract only in interface
    // interface static abstract property must be implemented (implicitly or explicitly) in the implementing type without the override keyword
    static abstract string StaticAbstractProperty { get; set; }

    // CS 11
    // static abstract only in interface
    // interface static abstract method must be implemented (implicitly or explicitly) in the implementing type without the override keyword
    static abstract void StaticAbstractMethod();

    // CS 11
    // static virtual only in interface
    // optional to implement interfaace static virtual property in the implementing type, however without the override keyword if implemented 
    static virtual string StaticVirtualProperty { get; set; } = "static virtual property initialized in the interface";

    // CS 11
    // static virtual only in interface
    // optional to implement interface static virtual method in the implementing type, however without the override keyword if implemented 
    static virtual void StaticVirtualMethod()
    {
        const string localConstant = "local const in interface static virtual method"; ;
        Console.WriteLine(localConstant);
    }


    // CS 11
    // static abstract only in interface
    // interface static abstract event must be implemented (implicitly or explicitly) in the implementing type without the override keyword
    // interface static abstract event cannot use event accessor syntax
    static abstract event InterfaceEventHandler? StaticAbstractEvent;

    // CS 11
    // static virtual only in interface
    // optional to implement interface static virtual event (implicitly or explicitly) in the implementing type, however without the override keyword if implemented 
    static virtual event InterfaceEventHandler? StaticVirtualEvent;

    static event InterfaceEventHandler? StaticVirtualEventWithAccessorsPlaceHolder;

    // CS 11
    // static virtual only in interface
    // optional to implement interface static virtual event (implicitly or explicitly) in the implementing type, however without the override keyword if implemented 
    // interface static virtual event can use event accessor syntax
    static virtual event InterfaceEventHandler? StaticVirtualEventWithAccessors
    {
        add
        {
            StaticVirtualEventWithAccessorsPlaceHolder += value;
        }
        remove
        {
            StaticVirtualEventWithAccessorsPlaceHolder -= value;
        }
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    void RaisetStaticVirtualEvent()
    {
        StaticVirtualEvent?.Invoke(this, new InterfaceEventArgs("Raise interface static virtual event in the interface!"));
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    void RaisetImplicitStaticVirtualEventWithAccessors()
    {
        StaticVirtualEventWithAccessorsPlaceHolder?.Invoke(this, new InterfaceEventArgs("Raise interface static virtual event with accessors in the interface!"));
    }
}

interface IGenericOperator<T> where T: IGenericOperator<T>
{
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

class StaticImplementation : IStatic
{
    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public string InstanceProperty { get; set; } = "interface instance property implicitly implemented";
    // Or
    // explicit implementation
    string IStatic.InstanceProperty { get; set; } = "interface instance property explicitly implemented";

    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public string AbstractProperty { get; set; } = "interface abstract property implicitly implemented";
    // Or
    // explicit implementation
    string IStatic.AbstractProperty { get; set; } = "interface abstract property explicitly implemented";

    // optional (implicit or explicit) implementation
    // implicit implementation
    public string VirtualProperty { get; set; } = "interface virtual property implicitly implemented";
    // Or
    // explicit implementation
    string setValue = "interface virtual property explicitly implemented";
    string IStatic.VirtualProperty
    {
        get { return setValue; }
        set { setValue = value; }
    }

    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public void AbstractMethod()
    {
        Console.WriteLine("interface abstract method implicitly implemented");
    }
    // Or
    // explicit implementation
    void IStatic.AbstractMethod()
    {
        Console.WriteLine("interface abstract method explicitly implemented");
    }

    // optional (implicit or explicit) implementation
    // implicit implementation
    public void VirtualMethod()
    {
        Console.WriteLine("interface virtual method implicitly implemented");
    }
    // Or
    // explicit implementation
    void IStatic.VirtualMethod()
    {
        Console.WriteLine("interface virtual method explicitly implemented");
    }


    private int[] indexerArray1D = new int[100];
    private int[,] indexerArray2D = new int[100, 100];
    private int[,,] indexerArray3D = new int[100, 100, 100];

    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public int this[int i] // indexer declaration
    {
        // The array object will throw IndexOutOfRange exception.
        get => indexerArray1D[i];
        set => indexerArray1D[i] = value;
    }
    // Or
    // explicit implementation
    int IStatic.this[int i] // indexer declaration
    {
        // The array object will throw IndexOutOfRange exception.
        get => indexerArray1D[i];
        set => indexerArray1D[i] = value;
    }

    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public int this[int i, int j] // indexer declaration
    {
        // The array object will throw IndexOutOfRange exception.
        get => indexerArray2D[i, j];
        set => indexerArray2D[i, j] = value;
    }
    // Or
    // explicit implementation
    int IStatic.this[int i, int j] // indexer declaration
    {
        // The array object will throw IndexOutOfRange exception.
        get => indexerArray2D[i, j];
        set => indexerArray2D[i, j] = value;
    }

    // optional (implicit or explicit) implementation
    // implicit implementation
    public int this[int i, int j, int k] // indexer declaration
    {
        // The array object will throw IndexOutOfRange exception.
        get => indexerArray3D[i, j, k];
        set => indexerArray3D[i, j, k] = value;
    }
    // Or
    // explicit implementation
    int IStatic.this[int i, int j, int k] // indexer declaration
    {
        // The array object will throw IndexOutOfRange exception.
        get => indexerArray3D[i, j, k];
        set => indexerArray3D[i, j, k] = value;
    }

    public event InterfaceEventHandler? InstanceEventPlaceHolderExplicit;
    private object objectLockInstanceEvent = new object();

    // mandatory (implicit or explicit) implementation
    // mandatory implementation
    public event InterfaceEventHandler? InstanceEvent;
    // Or
    // explicit implementation
    // explicit interface implementation of an event must use event accessor syntax
    event InterfaceEventHandler? IStatic.InstanceEvent
    {
        add
        {
            lock (objectLockInstanceEvent)
            {
                InstanceEventPlaceHolderExplicit += value;
            }
        }
        remove
        {
            lock (objectLockInstanceEvent)
            {
                InstanceEventPlaceHolderExplicit -= value;
            }
        }
    }
    
    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaiseImplicitInstanceeEvent()
    {
        InstanceEvent?.Invoke(this, new InterfaceEventArgs("Raise implicit interface instance event in the implementing type!"));
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaiseExplicitInstanceeEvent()
    {
        InstanceEventPlaceHolderExplicit?.Invoke(this, new InterfaceEventArgs("Raise explicit interface instance event in the implementing type!"));
    }
}

class StaticGenericImplementation : IStaticGeneric<StaticGenericImplementation>
{
    // CS 11
    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public static string StaticAbstractProperty { get; set; } = "interface static abstract property implicitly implemented";
    // Or
    // explicit implementation
    static string IStaticGeneric<StaticGenericImplementation>.StaticAbstractProperty { get; set; } = "interface static abstract property explicitly implemented";

    // CS 11
    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public static void StaticAbstractMethod()
    {
        Console.WriteLine("interface static abstract implicitly method implemented");
    }
    // Or
    // explicit implementation
    static void IStaticGeneric<StaticGenericImplementation>.StaticAbstractMethod()
    {
        Console.WriteLine("interface static abstract method explicitly implemented");
    }

    // CS 11
    // optional (implicit or explicit) implementation
    // implicit implementation
    public static string StaticVirtualProperty { get; set; } = "interface static virtual property implicitly implemented";
    // Or
    // explicit implementation
    static string IStaticGeneric<StaticGenericImplementation>.StaticVirtualProperty { get; set; } = "interface static virtual property explicitlyimplemented";

    // CS 11
    // optional (implicit or explicit) implementation
    // implicit implementation
    public static void StaticVirtualMethod()
    {
        Console.WriteLine("interface static virtual method implicitly implemented");
    }
    // Or
    // explicit implementation
    static void IStaticGeneric<StaticGenericImplementation>.StaticVirtualMethod()
    {
        Console.WriteLine("interface static virtual method explicitly implemented");
    }

    public static event InterfaceEventHandler? StaticAbstractEventPlaceHolderExplicit;
    private static object objectLockStaticAbstractEvent = new object();

    // CS 11
    // mandatory (implicit or explicit) implementation
    // implicit implementation
    public static event InterfaceEventHandler? StaticAbstractEvent;
    // Or
    // explicit implementation
    // explicit interface implementation of an event must use event accessor syntax
    static event InterfaceEventHandler? IStaticGeneric<StaticGenericImplementation>.StaticAbstractEvent
    {
        add
        {
            lock (objectLockStaticAbstractEvent)
            {
                StaticAbstractEventPlaceHolderExplicit += value;
            }
        }
        remove
        {
            lock (objectLockStaticAbstractEvent)
            {
                StaticAbstractEventPlaceHolderExplicit -= value;
            }
        }
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaisetImplicitStaticAbstractEvent()
    {
        StaticAbstractEvent?.Invoke(this, new InterfaceEventArgs("Raise implicit interface static abstract event in the implementing type!!"));
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaisetExplicitStaticAbstractEvent()
    {
        StaticAbstractEventPlaceHolderExplicit?.Invoke(this, new InterfaceEventArgs("Raise explicit interface static abstract event in the implementing type!!"));
    }

    public static event InterfaceEventHandler? StaticVirtualEventPlaceHolderExplicit;
    private static object objectLockStaticVirtualEvent = new object();

    // CS 11
    // optional (implicit or explicit) implementation
    // implicit implementation
    public static event InterfaceEventHandler? StaticVirtualEvent;
    // Or
    // explicit implementation
    // explicit interface implementation of an event must use event accessor syntax
    static event InterfaceEventHandler? IStaticGeneric<StaticGenericImplementation>.StaticVirtualEvent
    {
        add
        {
            lock (objectLockStaticVirtualEvent)
            {
                StaticVirtualEventPlaceHolderExplicit += value;
            }
        }
        remove
        {
            lock (objectLockStaticVirtualEvent)
            {
                StaticVirtualEventPlaceHolderExplicit -= value;
            }
        }
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaisetImplicitStaticVirtualEvent()
    {
        StaticVirtualEvent?.Invoke(this, new InterfaceEventArgs("Raise implicit interface static virtual event in the implementing type!!"));
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaisetExplicitStaticVirtualEvent() 
    {
        StaticVirtualEventPlaceHolderExplicit?.Invoke(this, new InterfaceEventArgs("Raise explicit interface static virtual event in the implementing type!!"));
    }

    private event InterfaceEventHandler? StaticVirtualEventWithAccessorsPlaceHolderImplementation;
    private static object objectLockStaticVirtualEventWithAccessors = new object();

    public static event InterfaceEventHandler? StaticVirtualEventWithAccessorsPlaceHolderImplementationExplicit;
    private static object objectLockStaticVirtualEventWithAccessorsExplicit = new object();

    // CS 11
    // optional (implicit or explicit) implementation
    // implicit implementation
    public event InterfaceEventHandler? StaticVirtualEventWithAccessors
    {
        add
        {
            lock (objectLockStaticVirtualEventWithAccessors)
            {
                StaticVirtualEventWithAccessorsPlaceHolderImplementation += value;
            }
        }
        remove
        {
            lock (objectLockStaticVirtualEventWithAccessors)
            {
                StaticVirtualEventWithAccessorsPlaceHolderImplementation -= value;
            }
        }
    }
    // Or
    // explicit implementation
    // explicit interface implementation of an event must use event accessor syntax
    static event InterfaceEventHandler? IStaticGeneric<StaticGenericImplementation>.StaticVirtualEventWithAccessors
    {
        add
        {
            lock (objectLockStaticVirtualEventWithAccessorsExplicit)
            {
                StaticVirtualEventWithAccessorsPlaceHolderImplementationExplicit += value;
            }
        }
        remove
        {
            lock (objectLockStaticVirtualEventWithAccessorsExplicit)
            {
                StaticVirtualEventWithAccessorsPlaceHolderImplementationExplicit -= value;
            }
        }
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaisetImplicitStaticVirtualEventWithAccessors()
    {
        StaticVirtualEventWithAccessorsPlaceHolderImplementation?.Invoke(this, new InterfaceEventArgs("Raise implicit interface static virtual event with accessors in the implementing type!"));
    }

    // NB
    // Wrap event invocations inside a protected virtual method
    // to allow derived classes to override the event invocation behavior
    public void RaisetExplicitStaticVirtualEventWithAccessors()
    {
        StaticVirtualEventWithAccessorsPlaceHolderImplementationExplicit?.Invoke(this, new InterfaceEventArgs("Raise explicit interface static virtual event with accessors in the implementing type!"));
    }
}

// CS 11
// static abstract operator receiver type should be a valid record/struct type
// ref structs cannot implement interfaces and may not be used as a type argument
// struct OperatorImplementation : IGenericOperator<OperatorImplementation>
record OperatorImplementation : IGenericOperator<OperatorImplementation>
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
}
/******************************************************************************/


// class, interface, struct, readonly struct, ref struct, readonly ref struct, record, positional record, record struct, readonly record struct
/******************************************************************************/
// Reference Type 
class C : I
{
    static C() { }
    public C() { }
    ~C() { }
}

// Reference Type 
interface I
{
    static I() { }
}

// Value Type
// struct can implement interface only
struct S : I
{
    static S() { }
    public S() { }
}

// CS 7.2 // Value Type
readonly struct ReadOnlyStructure : I
{
    static ReadOnlyStructure() { }
    public ReadOnlyStructure() { }
}
// ref structs cannot implement interface 
ref struct RefStructure
{
    static RefStructure() { }
    public RefStructure() { }
}
readonly ref struct ReadOnlyRefStructure
{
    static ReadOnlyRefStructure() { }
    public ReadOnlyRefStructure() { }
}

// CS 9 // Reference Type 
// Can inherit object or another record only and implement interface
// Cannot inherit class and cannot be base for class
record BaseRecord : I
{
    static BaseRecord() { }
    public BaseRecord() { }
    ~BaseRecord() { }
}
record DerivedRecord : BaseRecord, I
{
    static DerivedRecord() { }
    public DerivedRecord() { }
    ~DerivedRecord() { }
}
record ObjectRecord : object, I
{
    static ObjectRecord() { }
    public ObjectRecord() { }
    ~ObjectRecord() { }
}
// Positional records
// The primary constructor parameters to a record are referred to as positional parameters.
record BasePositionalRecord(string Alpha, string Beta) : I
{
    static BasePositionalRecord() { }
    public BasePositionalRecord() : this(string.Empty, string.Empty) { }
    ~BasePositionalRecord() { }
}
record DerivedPositionalRecord(string Alpha, string Beta, string Gamma) : BasePositionalRecord(Alpha, Beta), I
{
    static DerivedPositionalRecord() { }
    public DerivedPositionalRecord() : this(string.Empty, string.Empty, string.Empty) { }
    ~DerivedPositionalRecord() { }
}
record ObjectPositionalRecord(string Epsilon, string Zeta) : object, I
{
    static ObjectPositionalRecord() { }
    public ObjectPositionalRecord() : this(string.Empty, string.Empty) { }
    ~ObjectPositionalRecord() { }
}
// Positional record extending record
record ExtendedPositionalRecord(string Alpha, string Beta, string Gamma) : BaseRecord, I
{
    static ExtendedPositionalRecord() { }
    public ExtendedPositionalRecord() : this(string.Empty, string.Empty, string.Empty) { }
    ~ExtendedPositionalRecord() { }
}
// Record extending positional record 
record ExtentedRecord : BasePositionalRecord, I
{
    static ExtentedRecord() { }
    public ExtentedRecord() : this(string.Empty, string.Empty, string.Empty) { }
    public ExtentedRecord(string Alpha, string Beta, string Gamma) : base(Alpha, Beta) { }
    ~ExtentedRecord() { }
}

// CS 10 // Value Type
record struct RecordStructure : I
{
    static RecordStructure() { }
    public RecordStructure() { }
}
readonly record struct ReadOnlyRecordStructure : I
{
    static ReadOnlyRecordStructure() { }
    public ReadOnlyRecordStructure() { }
}
/******************************************************************************/


// Credit:
/*
https://dotnet.microsoft.com/
*/