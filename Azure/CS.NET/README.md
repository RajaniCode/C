---
languages:
- csharp
products:
- dotnet
- dotnet-core
page_type: sample
name: ".NET Core unit testing code coverage"
urlFragment: "unit-testing-code-coverage-cs"
description: ".NET Core unit testing code coverage and reporting with coverlet, and ReportGenerator."
---

# .NET Core unit testing code coverage

This sample solution includes a class library that is unit tested by two xUnit test projects. The corresponding article, [use code coverage for unit testing](https://docs.microsoft.com/dotnet/core/testing/unit-testing-code-coverage) details the usage of C#, xUnit, coverlet, and ReportGenerator.

## Sample prerequisites

This sample is written in C# and targets .NET Core 3.1. It requires the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1).

## Building the sample

The source code includes an MSBuild project file for C# (a *.csproj* file) that targets .NET Core 3.1. After you download the *.zip* file containing the example code, create a directory and select **Download ZIP** to download the sample code files to your computer. To build the example:

1. Download the *.zip* file containing.
1. Create the directory to which you want to copy the files.
1. Copy the files from the *.zip* file to the directory you just created.
1. If you are using Visual Studio 2019:
   1. In Visual Studio, select **Open a project or solution** (or **File** > **Open** > **Project/Solution** from the Visual Studio menu.
   1. Select **Debug** > **Start Debugging** from the Visual Studio menu to build and launch the application.
1. If you are working from the command line:
   1. Navigate to the directory that contains the sample.
   1. Type in the command `dotnet run` to build and launch the application.
