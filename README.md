# Polferov.StringFromFile

A source generator that allows you to read the string contents of a file at compile time.

## Installation

Install the [Polferov.StringFromFile](https://www.nuget.org/packages/Polferov.StringFromFile) NuGet package.

## Usage

```csharp
using Polferov.StringFromFile;

// currently allowed modifiers: public, internal, private
// and static (class may also be non-static)
// may be class or struct
// must be partial
// must have no generic parameters
public static partial class MyClass {
    // same allowed modifiers as obove
    // must be partial
    // must be a method returning a string
    // must have no parameters
    // must not be in a nested type
    [StringFromFile("SomeString.txt")]
    public static partial MyString();
} 