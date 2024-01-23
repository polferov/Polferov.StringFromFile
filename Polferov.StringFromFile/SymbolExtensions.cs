using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Polferov.StringFromFile;

internal static class SymbolExtensions
{
    public static string GetFullName(this ISymbol s)
    {
        if (s.ContainingNamespace?.ContainingNamespace is null)
            return s.Name;
        return $"{s.ContainingNamespace.GetFullName()}.{s.Name}";
    }


    public static ImmutableArray<INamedTypeSymbol> GetImplementedInterfaces(this INamedTypeSymbol symbol)
    {
        var interfaces = symbol.Interfaces;
        return interfaces.Concat(interfaces.SelectMany(i => i.GetImplementedInterfaces())).ToImmutableArray();
    }
    
    
    
    public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamespaceSymbol ns)
    {
        return ns.GetTypeMembers().Concat(ns.GetNamespaceMembers().SelectMany(GetAllTypes));
    }
}