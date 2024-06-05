// CS 8 // .NET Core 3.1
// ! (null-forgiving) operator


using System;
using System.Runtime.InteropServices;

// Redundant >= .NET 6 // default // enabled in .csproj
#nullable enable

class CS8
{
    static void Main()
    {
        var propertiesEnvironment = new EnvironmentProperties();
        propertiesEnvironment.Print();

        // ! (null-forgiving) operator
        new NullForgiving().Print();
        // ?? throw new ArgumentNullException(nameof(property));
        // var forgivingNull = new NullForgiving(null!);
    }

}

// ! (null-forgiving) operator
class NullForgiving
{
    // warning CS8618: Non-nullable property 'Property' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public NullForgiving() { }

    public NullForgiving(string property) => Property = property ?? throw new ArgumentNullException(nameof(property));

    public string? Property { get; }

    public void Print()
    {

        NullForgiving? result = Search("Without ! (null-forgiving) operator compiler generates warning CS8602: Dereference of a possibly null reference");

        if (IsNotNull(result))
        {
            // warning CS8602: Dereference of a possibly null reference.
            // Search method returns nullable
            // ! (null-forgiving) operator
            Console.WriteLine("! (null-forgiving) operator");
            Console.WriteLine($"Search Result: {result.Property}");
            Console.WriteLine("Use: result!.Property instead of result.Property");
        }

        result = Search("No warning");
        if (IsNotNullWhen(result))
        {
            Console.WriteLine("Use the NotNullWhen attribute to inform the compiler that an argument of the IsNotNullWhen method can't be null when the method returns true");
            // No warning
            Console.WriteLine($"Search Result: {result.Property}");
        }
    }

    private bool IsNotNull(NullForgiving? result) => result != null && result.Property != null;

    private bool IsNotNullWhen([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] NullForgiving? result) => result != null && result.Property != null;

    private static NullForgiving? Search(string property)
    {
        return new NullForgiving(property);
    }
}

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
        // .NET Core 3.1 does not contain a definition for `RuntimeIdentifier'
        // error CS0117: 'RuntimeInformation' does not contain a definition for 'RuntimeIdentifier' 
        // Console.WriteLine($"RuntimeInformation.RuntimeIdentifier: {RuntimeInformation.RuntimeIdentifier}");

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


// Output
/*
Environment.OSVersion: Unix 23.4.0.0
Environment.OSVersion.Platform: Unix
Environment.OSVersion.Version: 23.4.0.0
Environment.OSVersion.VersionString: Unix 23.4.0.0
Environment.OSVersion.Version.Major: 23
Environment.OSVersion.Version.Minor: 4
Environment.Version: 3.1.32
Environment.Is64BitOperatingSystem: True
Environment.Is64BitProcess: True
RuntimeInformation.FrameworkDescription: .NET Core 3.1.32
RuntimeInformation.ProcessArchitecture: X64
RuntimeInformation.OSArchitecture: X64
RuntimeInformation.OSDescription): Darwin 23.4.0 Darwin Kernel Version 23.4.0: Fri Mar 15 00:10:42 PDT 2024; root:xnu-10063.101.17~1/RELEASE_ARM64_T6000

! (null-forgiving) operator
Search Result: Without ! (null-forgiving) operator compiler generates warning CS8602: Dereference of a possibly null reference
Use: result!.Property instead of result.Property
Use the NotNullWhen attribute to inform the compiler that an argument of the IsNotNullWhen method can't be null when the method returns true
Search Result: No warning
*/


// Credit:
/*
https://dotnet.microsoft.com/
*/