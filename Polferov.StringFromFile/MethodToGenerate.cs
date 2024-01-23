namespace Polferov.StringFromFile;

internal class MethodToGenerate
{
    public required string Namespace { get; init; }

    /// <summary>
    /// like "class Xyz" or "struct Xyz" or "static class Xyz"
    /// </summary>
    public required string ContainingType { get; init; }

    /// <summary>
    /// like "private" or "private static SomeString"
    /// </summary>
    public required string MethodType { get; init; }

    public required string Path { get; init; }
}