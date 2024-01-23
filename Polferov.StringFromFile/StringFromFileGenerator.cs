using Microsoft.CodeAnalysis;

namespace Polferov.StringFromFile;

[Generator]
public class StringFromFileGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // Thread.Sleep(5000);
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var ms = GetMethodsToGenerate(context);
        var code = GenerateMethodCode.Code(ms);
        context.AddSource("StringFromFile.g.cs", code);
        Thread.Sleep(0);
    }

    private IReadOnlyCollection<MethodToGenerate> GetMethodsToGenerate(GeneratorExecutionContext context)
    {
        var methods = context.Compilation.Assembly.GlobalNamespace.GetMembers().OfType<INamedTypeSymbol>()
            .SelectMany(t => t.GetMembers().OfType<IMethodSymbol>());

        return methods.Select(m =>
        {
            var attr = m.GetAttributes().SingleOrDefault(a => a.AttributeClass?.GetFullName() ==
                                                              $"{nameof(Polferov)}.{nameof(StringFromFile)}.{nameof(StringFromFileAttribute)}");

            if (attr is null)
                return null;

            var path = GetPath(attr, m, context);


            var type = m.ContainingType;
            var typeType = GetAccessModifier(type.DeclaredAccessibility) + " ";

            typeType += type.IsStatic ? "static " : "";
            typeType += "partial ";
            if (type.TypeKind.HasFlag(TypeKind.Class))
                typeType += "class ";
            else if (type.TypeKind.HasFlag(TypeKind.Struct))
                typeType += "struct ";
            else
                throw new Exception("containing type is not struct or class");

            typeType += type.Name;

            if (type.ContainingType is not null)
                throw new Exception("can not handle types declared in other types");

            var mod = GetAccessModifier(m.DeclaredAccessibility) + " ";
            mod += m.IsStatic ? "static " : "";
            mod += "partial string " + m.Name;

            return new MethodToGenerate
            {
                Namespace = type.ContainingNamespace.GetFullName(),
                ContainingType = typeType,
                MethodType = mod,
                Path = path,
            };
        }).Where(m => m is not null).ToArray()!;
    }

    private static string GetPath(AttributeData attr, IMethodSymbol m, GeneratorExecutionContext context)
    {
        var path = (string)attr.ConstructorArguments.First().Value!;

        var syntax = m.DeclaringSyntaxReferences.Single();
        var filePath = syntax.SyntaxTree.GetLocation(syntax.Span).GetMappedLineSpan().Path;
        filePath = filePath[..filePath.LastIndexOf('/')];
        return Path.Combine(filePath, path);
    }

    private string GetAccessModifier(Accessibility a)
    {
        if (a.HasFlag(Accessibility.Private))
            return "private";
        if (a.HasFlag(Accessibility.Public))
            return "public";
        if (a.HasFlag(Accessibility.Internal))
            return "internal";
        throw new Exception("can not handle type/method that is not public, private, or internal");
    }
}