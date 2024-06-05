// 7.File - local types
namespace FileLocalCNamespace
{
    file class FileLocalC
    {
        public string Method() => "Method in FileC";
    }
}

namespace FileLocalCClientNamespace
{
    class FileLocalCClient
    {
        public void Print()
        {
            // Console.WriteLine(new FileLocalC().Method()); // Error CS0246: The type or namespace name 'FileLocalC' could not be found
            Console.WriteLine(new FileLocalCNamespace.FileLocalC().Method()); // Fine
        }
    }
}


// Credit:
/*
https://dotnet.microsoft.com/
*/