using System;

namespace StructTypes
{
    public struct Struct
    {
        // Error CS0573: 'Struct': cannot have instance property or field initializers in structs
        private string data = String.Empty;
        // Error CS0568: Structs cannot contain explicit parameterless constructors
        public Struct() { }
    }
}

