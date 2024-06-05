// CS 8 // .NET 8
// Null Forgiving Operator


using System.Runtime.InteropServices;

// Redundant >= .NET 6 // default // enabled in .csproj
// #nullable enable

EnvironmentProperties propertiesEnvironment = new();
propertiesEnvironment.Print();

// ?? throw new ArgumentNullException(nameof(property));
// NullForgiving forgivingNull = new (null!); 

new CS8().Print();

class CS8
{
    // Error CS0236: A field initializer cannot reference the non-static field, method, or property 
    // Unless Search method is static
    NullForgiving? result = Search("A field initializer cannot reference the non-static field, method, or property");

    public void Print()
    {
        // error CS0844: Cannot use local variable 'result' before it is declared.
        // The declaration of the local variable hides the field 'CS8.result'.
        // Console.WriteLine($"Search Result: {result.Property}");

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

    private bool IsNotNull(NullForgiving? result) => result is not null && result.Property is not null; 

    private bool IsNotNullWhen([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] NullForgiving? result) => result is not null && result.Property is not null; 

    private static NullForgiving? Search(string property)
    {
        return new NullForgiving(property);
    }
}

class NullForgiving
{
    public NullForgiving(string property) => Property = property ?? throw new ArgumentNullException(nameof(property));
    public string Property { get; }
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


// Output
/*
/Users/rajaniapple/Desktop/GitHub/CS/CS/CS/CS8/macOSarm64/CS8.NET8NullForgivingOperator/Program.cs(38,49): warning CS8602: Dereference of a possibly null reference. [/Users/rajaniapple/Desktop/GitHub/CS/CS/CS/CS8/macOSarm64/CS8.NET8NullForgivingOperator/CS8.NET8NullForgivingOperator.csproj]
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