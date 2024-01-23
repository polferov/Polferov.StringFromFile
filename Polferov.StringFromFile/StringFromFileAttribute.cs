namespace Polferov.StringFromFile;

/// <summary>
/// Attribute that causes the <see cref="StringFromFileGenerator"/> to generate a method that returns the string contents of a file.
/// The file path is relative to the source file.
///
/// <example>
/// using Polferov.StringFromFile;
/// 
/// // currently allowed modifiers: public, internal, private
/// // and static (class may also be non-static)
/// // may be class or struct
/// // must be partial
/// // must have no generic parameters
/// public static partial class MyClass {
///     // same allowed modifiers as obove
///     // must be partial
///     // must be a method returning a string
///     // must have no parameters
///     // must not be in a nested type
///     // the file path is relative to the source file
///     [StringFromFile("SomeString.txt")]
///     public static partial MyString();
/// } 
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class StringFromFileAttribute : Attribute
{
    public StringFromFileAttribute(string path)
    {
    }
}