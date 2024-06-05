// 7.File - local types
namespace FileLocalNamespace;

file class FileLocal
{
    public string Method() => "Method in FileB";
}


class FileLocalClient
{
    public void Print()
    {
        Console.WriteLine(new FileLocal().Method());
    }
}


// Credit:
/*
https://dotnet.microsoft.com/
*/