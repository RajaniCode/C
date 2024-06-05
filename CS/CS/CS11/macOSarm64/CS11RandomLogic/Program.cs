/*
// Multidimensional Array
// Three-dimensional Array
// 2 * 4 * 6 = 48 elements
int[ , , ] array3D = new int[2, 4, 6]; // int[ , , , ] array4D = new int[8, 6, 4, 2]; . . .

for (int i = 0; i < array3D.GetLength(0); i++)
{
    for (int j = 0; j < array3D.GetLength(1); j++)
    {
        for (int k = 0; k < array3D.GetLength(2); k++)
        {
            array3D[i, j, k] = Random.Shared.Next();
        }
    }
}

for (int i = 0; i < array3D.GetLength(0); i++)
{
    for (int j = 0; j < array3D.GetLength(1); j++)
    {
        for (int k = 0; k < array3D.GetLength(2); k++)
        {
            Console.WriteLine($"array3D[{i}, {j} , {k}] = {array3D[i, j, k]}");
        }
    }
}
*/


/*
// Java Multidimensional Array
// Three-dimensional Array
public class Program {
    public static void main(String args[]) {
        // 2 * 4 * 6 = 48 elements
        int[][][] array3D = new int[2][4][6]; // int[][][][] array4D = new int[7][5][3][1]; . . .
        for (int i = 0; i < array3D.length; i++) {
            for (int j = 0; j < array3D[i].length; j++) {
                for (int k = 0; k < array3D[i][j].length; k++) {
                    array3D[i][j][k] = new java.util.Random().nextInt(100);
                }
            }
        }
        
        
        for (int i = 0; i < array3D.length; i++) {
            for (int j = 0; j < array3D[i].length; j++) {
                for (int k = 0; k < array3D[i][j].length; k++) {
                    System.out.printf("array3D[%d][%d][%d] = %d\n", i, j, k, array3D[i][j][k]);
                }
            }
        }


        // for each loop
        // for (int[][] array2D: array3D) {
            // for (int[] array1D: array2D) {
                // for(int element: array1D) {
                    // System.out.println(element);
                // }
            // }
        // }

    }
}
*/


/*
// Jagged Array
int[][] jaggedArray = new int[2][];  // int[][][] jaggedArray = new int[2][][]; . . .

// 4 + 8 = 12 elements
for (int i = 0; i < jaggedArray.Length; i++)
{
    jaggedArray[0] = new int[4]; // jaggedArray[0] = new int[10]; . . .
    jaggedArray[1] = new int[8]; // jaggedArray[1] = new int[5]; . . .
}

#if comment
for (int i = 0; i < jaggedArray[0].Length; i++)
{
    jaggedArray[0][i] = Random.Shared.Next();
}

for (int i = 0; i < jaggedArray[1].Length; i++)
{
    jaggedArray[1][i] = Random.Shared.Next();
}
#endif

for (int i = 0; i < jaggedArray.Length; i++)
{
    for (int j = 0; j < jaggedArray[i].Length; j++)
    {
        jaggedArray[i][j] = Random.Shared.Next();
    }
}

#if comment
for (int i = 0; i < jaggedArray[0].Length; i++)
{
    Console.WriteLine($"jaggedArray[{0}][{i}] = {jaggedArray[0][i]}");
}

for (int i = 0; i < jaggedArray[1].Length; i++)
{
    Console.WriteLine($"jaggedArray[{1}][{i}] = {jaggedArray[1][i]}");
}
#endif

for (int i = 0; i < jaggedArray.Length; i++)
{
    for (int j = 0; j < jaggedArray[i].Length; j++)
    {
        Console.WriteLine($"jaggedArray[{i}][{j}] = {jaggedArray[i][j]}");
    }
}
*/


/*
// Jagged Array with Multidimensional Arrays
// Jagged Array with Three-dimensional Arrays
int[][ , , ] jaggedArray = new int[2][ , , ];  // int[][ , , ][ , , , ] jaggedArray = new int[4][ , , ][ , , , ]; . . .

// (2 * 4 * 6) + (5 * 3 * 1) = 63 elements
for (int i = 0; i < jaggedArray.Length; i++)
{
    jaggedArray[0] = new int[2, 4, 6];
    jaggedArray[1] = new int[5, 3, 1];
}

#if comment
for (int i = 0; i < jaggedArray[0].GetLength(0); i++)
{
    for (int j = 0; j < jaggedArray[0].GetLength(1); j++)
    {
        for (int k = 0; k < jaggedArray[0].GetLength(2); k++)
        {
            jaggedArray[0][i, j, k] = Random.Shared.Next();
        }
    }
}

for (int i = 0; i < jaggedArray[1].GetLength(0); i++)
{
    for (int j = 0; j < jaggedArray[1].GetLength(1); j++)
    {
        for (int k = 0; k < jaggedArray[1].GetLength(2); k++)
        {
            jaggedArray[1][i, j, k] = Random.Shared.Next();
        }
    }
}
#endif

for (int index = 0; index < jaggedArray.Length; index++)
{
    for (int i = 0; i < jaggedArray[index].GetLength(0); i++)
    {
        for (int j = 0; j < jaggedArray[index].GetLength(1); j++)
        {
            for (int k = 0; k < jaggedArray[index].GetLength(2); k++)
            {
                jaggedArray[index][i, j, k] = Random.Shared.Next();
            }
        }
    }
}

#if comment
for (int i = 0; i < jaggedArray[0].GetLength(0); i++)
{
    for (int j = 0; j < jaggedArray[0].GetLength(1); j++)
    {
        for (int k = 0; k < jaggedArray[0].GetLength(2); k++)
        {
            Console.WriteLine($"jaggedArray[0][{i}, {j} , {k}] = {jaggedArray[0][i, j, k]}");
        }
    }
}

for (int i = 0; i < jaggedArray[1].GetLength(0); i++)
{
    for (int j = 0; j < jaggedArray[1].GetLength(1); j++)
    {
        for (int k = 0; k < jaggedArray[1].GetLength(2); k++)
        {
            Console.WriteLine($"jaggedArray[1][{i}, {j} , {k}] = {jaggedArray[1][i, j, k]}");
        }
    }
}
#endif

for (int index = 0; index < jaggedArray.Length; index++)
{
    for (int i = 0; i < jaggedArray[index].GetLength(0); i++)
    {
        for (int j = 0; j < jaggedArray[index].GetLength(1); j++)
        {
            for (int k = 0; k < jaggedArray[index].GetLength(2); k++)
            {
                Console.WriteLine($"jaggedArray[{index}][{i}, {j} , {k}] = {jaggedArray[index][i, j, k]}");
            }
        }
    }
}
*/


/*
// Java Array concat
public class Program {
    public static void main(String args[]) {
        int[] a = { 1, 3, 5 };
        int[] b = { 0, 2, 3, 6 };
        a = java.util.stream.IntStream
            .concat(java.util.stream.IntStream.of(a), java.util.stream.IntStream.of(b))
            .sorted()
            .toArray();
        System.out.println(java.util.Arrays.toString(a));
    }
}
*/

/*
// Java Array concat distinct
public class Program {
    public static void main(String args[]) {
        int[] a = { 1, 3, 5 };
        int[] b = { 0, 2, 3, 6 };
        a = java.util.stream.IntStream
            .concat(java.util.stream.IntStream.of(a), java.util.stream.IntStream.of(b))
            .distinct()
            .sorted()
            .toArray();
        System.out.println(java.util.Arrays.toString(a));
    }
}
*/

/*
// Java Array concat // for loop 
public class Program {
    public static void main(String args[]) {
        int[] a = { 1, 3, 5 };
        int[] b = { 0, 2, 3, 6 };
        int originalLength = a.length;
        a = java.util.Arrays.copyOf(a, a.length + b.length);
        for (int i = originalLength, j = 0; j < b.length; i++, j++) {
            a[i] = b[j];
        }
        System.out.println(java.util.Arrays.toString(a));
    }
}
*/

/*
// Java Array Copy
public class Program {
    public static void main(String args[]) {
        int[] a = { 1, 3, 5 };
        int[] b = { 0, 2, 3, 6 };
        a = new int[b.length];
        System.arraycopy(b, 0, a, 0, b.length );
        System.out.println(java.util.Arrays.toString(a));
    }
}
*/



/*
// Array Concatenation
// using System.Linq;
int[] a = { 1, 3, 5 };
int[] b = { 0, 2, 3, 6 };
var c = a.Concat(b).OrderBy(x => x);
Console.WriteLine(string.Join(", ", c));
*/

/*
// Array.Resize, Array.Copy, and Array.Sort
// using System.Array;
// using System.Linq;
int[] a = { 1, 3, 5 };
int[] b = { 0, 2, 3, 6 };
int array1OriginalLength = a.Length;
Array.Resize<int>(ref a, array1OriginalLength + b.Length);
Array.Copy(b, 0, a, array1OriginalLength, b.Length);
Array.Sort(a);
Console.WriteLine(string.Join(", ", a));
*/

/*
// Array.Resize, Array.Copy, Array.Sort, and new Array
// using System.Array;
// using System.Linq;
int[] a = { 1, 3, 5 };
int[] b = { 0, 2, 3, 6 };
int[] c = new int[a.Length + b.Length];
Array.Copy(a, c, a.Length);
Array.Copy(b, 0, c, a.Length, b.Length);
Array.Sort(c);
Console.WriteLine(string.Join(", ", c));
*/


// Java String Reverse
/*
// 1. java.util.stream.Stream.of
String text = "Reverse a string!";
String reversedText = java.util.stream.Stream.of(text)
    .map(string -> new StringBuilder(string).reverse())
    .collect(java.util.stream.Collectors.joining());
System.out.println(reversedText);
*/

/*
// 2. java.util.stream.IntStream.range and java.lang.StringBuilder
String text = "Reverse a string!";
String reversedText = java.util.stream.IntStream.range(0, text.length())
    .mapToObj(i -> text.toCharArray()[text.length() - i - 1])
    .collect(StringBuilder::new, StringBuilder::append, StringBuilder::append).toString();
System.out.println(reversedText);
*/

/*
// 3. java.lang.StringBuilder
String text = "Reverse a string!";
String reversedText = new StringBuilder(text).reverse() .toString();
System.out.println(reversedText);
*/

/*
// 4. String.chars
String text = "Reverse a string!";
String reversedText = text.chars()
    .mapToObj(x -> (char) x)
    .reduce("", (a, b) -> b + a, (c, d) -> d + c);
System.out.println(reversedText);
*/

/*
// 5. for loop
String text = "Reverse a string!";
String reversedText = "";
for (int i = text.length() - 1; i >= 0; i--) {
    reversedText = reversedText + text.charAt(i);
}
System.out.println(reversedText);
*/

// CS string Reverse
/*
// 1. using System.Linq;
string text = "Reverse a string!";
string reversedText = new string(Enumerable.Range(1, text.Length).Select(i => text[text.Length - i]).ToArray());
Console.WriteLine(reversedText);
*/

/*
// 2. using System.Text; // StringBuilder
string text = "Reverse a string!";
string reversedText = new string(new System.Text.StringBuilder().Append(text).ToString().Reverse().ToArray());
Console.WriteLine(reversedText);
*/

/*
// 3. String.ToCharArray
string text = "Reverse a string!";
char[] chararray = text.ToCharArray();
Array.Reverse(chararray);
string reversedText = new string(chararray);
Console.WriteLine(reversedText);
*/


/*
// Java Access Private Method
public class Program {
  public static void main(String args[]) throws Exception { //
    // try {
      var inquisite = new Inquisitive();
      inquisite.print();
    // } catch (Exception e) {
      // e.printStackTrace();
    // }
  }
}

class PrivateType {
  private static void staticcMethod() {
    System.out.println("Private static method");
  }

  private void instanceMethod() {
    System.out.println("Private instance method");
  }
}

class Inquisitive {
  public void print() throws Exception { //
    // try {
      Class classForName = Class.forName("PrivateType");
      Object instance = classForName.newInstance();
      java.lang.reflect.Method meth = instance.getClass().getDeclaredMethod("staticcMethod");
      meth.setAccessible(true);
      meth.invoke(instance);
      meth = instance.getClass().getDeclaredMethod("instanceMethod");
      meth.setAccessible(true);
      meth.invoke(instance);
    // } catch (ClassNotFoundException e) {
      // e.printStackTrace();
    // } catch (InstantiationException e) {
      // e.printStackTrace();
    // } catch (IllegalAccessException e) {
      // e.printStackTrace();
    // } catch (NoSuchMethodException e) {
      // e.printStackTrace();
    // } catch (java.lang.reflect.InvocationTargetException e) {
      // e.printStackTrace();
    // }
  }
}
*/


/*
// Access Private Method
Inquisitive inquisite = new();
inquisite.Print();

class PrivateType
{
    private static void StaticcMethod()
    {
        Console.WriteLine("Private static method");
    }

    private void InstanceMethod()
    {
        Console.WriteLine("Private instance method");
    }
}

class Inquisitive
{
    public void Print()
    {
        typeof(PrivateType).GetMethod("StaticcMethod", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.Invoke(new PrivateType(), null);
        typeof(PrivateType).GetMethod("InstanceMethod", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.Invoke(new PrivateType(), null);
    }
}
*/


/*
// Java enum
// % java Planet.java 1
enum Planet {
    MERCURY (3.303e+23, 2.4397e6),
    VENUS   (4.869e+24, 6.0518e6),
    EARTH   (5.976e+24, 6.37814e6),
    MARS    (6.421e+23, 3.3972e6),
    JUPITER (1.9e+27,   7.1492e7),
    SATURN  (5.688e+26, 6.0268e7),
    URANUS  (8.686e+25, 2.5559e7),
    NEPTUNE (1.024e+26, 2.4746e7);

    private final double mass;   // in kilograms
    private final double radius; // in meters
    Planet(double mass, double radius) {
        this.mass = mass;
        this.radius = radius;
    }
    private double mass() { return mass; }
    private double radius() { return radius; }

    // universal gravitational constant  (m3 kg-1 s-2)
    public static final double G = 6.67300E-11;

    double surfaceGravity() {
        return G * mass / (radius * radius);
    }
    double surfaceWeight(double otherMass) {
        return otherMass * surfaceGravity();
    }
    public static void main(String[] args) {
        if (args.length != 1) {
            System.err.println("Usage: java Planet <earth_weight>");
            System.exit(-1);
        }
        double earthWeight = Double.parseDouble(args[0]);
        double mass = earthWeight/EARTH.surfaceGravity();
        for (Planet p : Planet.values()) {
           System.out.printf("Your weight on %s is %f%n",
               p, p.surfaceWeight(mass));
        }
    }
}
*/


Console.WriteLine();


// Credit:
/*
https://dotnet.microsoft.com/
*/
