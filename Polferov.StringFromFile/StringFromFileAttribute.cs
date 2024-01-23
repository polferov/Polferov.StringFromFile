namespace Polferov.StringFromFile;

[AttributeUsage(AttributeTargets.Method)]
public class StringFromFileAttribute : Attribute
{
    public StringFromFileAttribute(string path)
    {
    }
}