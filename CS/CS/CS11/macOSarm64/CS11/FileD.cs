// 7.File - local types
namespace FileLocalCNamespace
{
    class FileLocalCClient
    {
        public void Print()
        {
            // Console.WriteLine(new FileLocalC().Method()); // Error CS0246: The type or namespace name 'FileLocalC' could not be found
            // Console.WriteLine(new FileLocalCNamespace.FileLocalC().Method()); // Error CS0234: The type or namespace name 'FileLocalC' does not exist in the namespace 'FileLocalCNamespace'
        }
    }
}


// Credit:
/*
https://dotnet.microsoft.com/
*/