﻿// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (MS-LPL, http://go.microsoft.com/?linkid=9791213.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

// Factorial.cs
// compile with: /target:library
using System; 

// Declares a namespace. You need to package your libraries according
// to their namespace so the .NET runtime can correctly load the classes.
namespace Functions 
{ 
    public class Factorial 
    { 
// The "Calc" static method calculates the factorial value for the
// specified integer passed in:
        public static int Calc(int i) 
        { 
            return((i <= 1) ? 1 : (i * Calc(i-1))); 
        } 
    }
}

