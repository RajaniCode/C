// CS 11


using System.Runtime.InteropServices;


EnvironmentProperties propertiesEnvironment = new();
propertiesEnvironment.Print();


// error CS9058: Feature 'primary constructors' is not available in C# 11.0. Please use language version 12.0 or greater. [/Users/rajaniapple/Desktop/Working/CS/CS12/macOS/CS11/CS11.csproj]
// class PrimaryConstructors(string Alpha, string Beta);


// Optionally, you can omit the class keyword to create a record class.
record RecordDefraultClass(bool IsPropertyGenerated);


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


// Credit:
/*
https://dotnet.microsoft.com/
*/