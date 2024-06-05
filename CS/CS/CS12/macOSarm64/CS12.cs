// CS 12
/*
1. Primary constructors
2. Collection expressions
3. ref readonly parameters
4. Default lambda parameters
5. Alias any type
6. Inline arrays
7. Experimental attribute
8. Interceptors
*/


// NB
// default
// <ImplicitUsings>enable</ImplicitUsings>
// <Nullable>enable</Nullable>
// Implicit using directives
// % cat $HOME/Desktop/Working/CS/CS12/macOS/CS12/obj/Debug/net8.0/CS12.GlobalUsings.g.cs


/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
*/


using System.Runtime.InteropServices;


/******************************************************************************/
// Environment Properties
/******************************************************************************/
EnvironmentProperties propertiesEnvironment = new();
propertiesEnvironment.Print();
/******************************************************************************/


/******************************************************************************/
// 1. Primary constructors
/******************************************************************************/
Console.WriteLine("1. Primary constructors");
PrimaryConstructors constructorsPrimary = new("A", "B");
Console.WriteLine(constructorsPrimary);
Console.WriteLine( new PrimaryConstructors() );

Console.WriteLine( new PrimaryConStruct("C") );
Console.WriteLine( new PrimaryConStruct("E").InstanceFieldInStruct );

Console.WriteLine($"IsPropertyGenerated for record class: {new RecordClass(true).IsPropertyGenerated}");
Console.WriteLine($"IsPropertyGenerated for record default class: {new RecordDefraultClass(true).IsPropertyGenerated}");
Console.WriteLine($"IsPropertyGenerated for record struct: {new RecordStruct(true).IsPropertyGenerated}");

// error CS1061: 'NonrecordClass' does not contain a definition for 'IsPropertyGenerated' and no accessible extension method 'IsPropertyGenerated' accepting a first argument of type 'NonrecordClass' could be found
// Console.WriteLine($"IsPropertyGenerated for Nonrecord class: {new NonrecordClass(true).IsPropertyGenerated}");
// error CS1061: 'NonrecordStruct' does not contain a definition for 'IsPropertyGenerated' and no accessible extension method 'IsPropertyGenerated' accepting a first argument of type 'NonrecordStruct'
// Console.WriteLine($"IsPropertyGenerated for Nonrecord struct: {new NonrecordStruct(true).IsPropertyGenerated}");
/******************************************************************************/


/******************************************************************************/
// 1. Primary constructors
/******************************************************************************/

// You can now create primary constructors in any class and struct. Primary constructors are no longer restricted to record types.

// Adding a primary constructor to a class prevents the compiler from declaring an implicit parameterless constructor.
class PrimaryConstructors(string Alpha, string Beta) 
// Primary constructor parameters are in scope for the entire body of the class.
{ 
    // warning CS9113: Parameter 'Alpha' is unread.
    // warning CS9113: Parameter 'Beta' is unread.
    // If commented out
    public override string ToString() => $"Alpha: {Alpha}, Beta: {Beta}";

    // To ensure that all primary constructor parameters are definitely assigned, all explicitly declared constructors must call the primary constructor using this() syntax. 
   // In other words, a constructor declared in a type with parameter list must have 'this' constructor initializer.
    // Explicit parameterless constructor
    public PrimaryConstructors() : this("Explicit A", "Explicit B") { }
}

struct PrimaryConStruct(string Gamma, string Delta = "G")
// Primary constructor parameters are in scope for the entire body of the struct.
{ 
    // warning CS9113: Parameter 'Gamma' is unread.
    // warning CS9113: Parameter 'Delta' is unread. 
    // If commented out
    public override string ToString() => $"Gamma: {Gamma}, Delta: {Delta}";

    // warning CS0649: Field 'PrimaryConStruct.InstanceFieldInStruct' is never assigned to, and will always have its default value 0
    public int InstanceFieldInStruct;
     
    //  In a struct, the implicit parameterless constructor initializes all fields, including primary constructor parameters to the 0-bit pattern.
    // CS 11 // Auto-default structs
    // Comment out the explicit parameterless constructor to verify
    // Explicit parameterless constructor
    public PrimaryConStruct() : this("Explicit C", "Explicit D") { }
}


// The compiler generates public properties for primary constructor parameters only in record types, either record class or record struct types. 
record class RecordClass(bool IsPropertyGenerated);
// Optionally, you can omit the class keyword to create a record class.
record RecordDefraultClass(bool IsPropertyGenerated);
record struct RecordStruct(bool IsPropertyGenerated);

// Nonrecord classes and structs might not always want this behavior for primary constructor parameters.
class NonrecordClass(bool IsPropertyGenerated); // warning CS9113: Parameter 'IsPropertyGenerated' is unread.
struct NonrecordStruct(bool IsPropertyGenerated); // warning CS9113: Parameter 'IsPropertyGenerated' is unread.
/******************************************************************************/


/******************************************************************************/
// Environment Properties
/******************************************************************************/
class EnvironmentProperties
{
    public void Print()
    {
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
    }
}
/******************************************************************************/


// Output
/*
/Users/rajaniapple/Desktop/Working/CS/CS12/macOS/CS12/Program.cs(113,29): warning CS9113: Parameter 'IsPropertyGenerated' is unread. [/Users/rajaniapple/Desktop/Working/CS/CS12/macOS/CS12/CS12.csproj]
/Users/rajaniapple/Desktop/Working/CS/CS12/macOS/CS12/Program.cs(112,27): warning CS9113: Parameter 'IsPropertyGenerated' is unread. [/Users/rajaniapple/Desktop/Working/CS/CS12/macOS/CS12/CS12.csproj]
/Users/rajaniapple/Desktop/Working/CS/CS12/macOS/CS12/Program.cs(95,16): warning CS0649: Field 'PrimaryConStruct.InstanceFieldInStruct' is never assigned to, and will always have its default value 0 [/Users/rajaniapple/Desktop/Working/CS/CS12/macOS/CS12/CS12.csproj]
Environment.OSVersion: Unix 14.4.1
Environment.OSVersion.Platform: Unix
Environment.OSVersion.Version: 14.4.1
Environment.OSVersion.VersionString: Unix 14.4.1
Environment.OSVersion.Version.Major: 14
Environment.OSVersion.Version.Minor: 4
Environment.Version: 8.0.4
Environment.Is64BitOperatingSystem: True
Environment.Is64BitProcess: True
RuntimeInformation.FrameworkDescription: .NET 8.0.4
RuntimeInformation.ProcessArchitecture: Arm64
RuntimeInformation.OSArchitecture: Arm64
RuntimeInformation.OSDescription): Darwin 23.4.0 Darwin Kernel Version 23.4.0: Fri Mar 15 00:10:42 PDT 2024; root:xnu-10063.101.17~1/RELEASE_ARM64_T6000
RuntimeInformation.RuntimeIdentifier: osx-arm64

1. Primary constructors
Alpha: A, Beta: B
Alpha: Explicit A, Beta: Explicit B
Gamma: C, Delta: G
0
IsPropertyGenerated for record class: True
IsPropertyGenerated for record default class: True
IsPropertyGenerated for record struct: True
*/


// Credit:
/*
https://dotnet.microsoft.com/
*/