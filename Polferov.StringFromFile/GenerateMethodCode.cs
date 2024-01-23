using System.Text;
using Microsoft.CodeAnalysis.CSharp;

namespace Polferov.StringFromFile;

internal static class GenerateMethodCode
{
    public static string Code(IEnumerable<MethodToGenerate> ms)
    {
        var sb = new StringBuilder();
        foreach (var m in ms)
            Single(sb, m);
        return sb.ToString();
    }

    private static void Single(StringBuilder sb, MethodToGenerate m)
    {
        if (string.IsNullOrWhiteSpace(m.Namespace))
        {
            sb.Append(
                $$"""
                  {{m.ContainingType}} {
                      {{m.MethodType}}() => "{{EscapedFile(m.Path)}}";
                  }
                  """
            );
            return;
        }
        
        sb.Append(
            $$"""
              namespace {{m.Namespace}} {
                 {{m.ContainingType}} {
                     {{m.MethodType}}() => "{{EscapedFile(m.Path)}}";
                 }
              }
              """
        );
    }

    private static string EscapedFile(string path)
    {
        var content = File.ReadAllText(path);
        return SymbolDisplay.FormatLiteral(content, false);
    }
}