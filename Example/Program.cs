using Polferov.StringFromFile;

Console.WriteLine(SomeString());


partial class Program
{
    [StringFromFile("SomeString.txt")]
    public static partial string SomeString();
}

