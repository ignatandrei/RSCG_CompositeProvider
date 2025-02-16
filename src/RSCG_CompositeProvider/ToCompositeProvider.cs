using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;

namespace RSCG_CompositeProvider;
[Generator]
public class ToCompositeProvider : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        
        var classesToApplySortable = context.SyntaxProvider.ForAttributeWithMetadataName(
"RSCG_CompositeProvider_Common.CompositeProviderAttribute",
IsAppliedOnInterface,
FindAttributeDataExpose
)
.Collect()
.SelectMany((data, _) => data.Distinct())
.Collect()
;

        context.RegisterSourceOutput(classesToApplySortable,
(spc, data) =>
ExecuteSortGeneric1(spc, data));

    }

    private void ExecuteSortGeneric1(SourceProductionContext spc, ImmutableArray<DataFromExposeInterface?> data)
    {
         ExecuteSort(spc,data);
    }
    private void ExecuteSort(SourceProductionContext spc, ImmutableArray<DataFromExposeInterface?> dataFromExposeClasses)
    {
        var arr = dataFromExposeClasses.ToArray()
            .Where(it => it != null)
            .Select(it => it!)
            .ToArray();
        foreach (var classData in arr)
        {
            var c = new Prov(classData);
            var text = c.Render();
            spc.AddSource($"{classData.Name}_cp.cs", text);
        }
    }
  

    private DataFromExposeInterface? FindAttributeDataExpose(GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        var data = context.TargetSymbol as INamedTypeSymbol;
        if (data != null)
        {

            return new DataFromExposeInterface(data);
        }

        return null;
    }

    private static bool IsAppliedOnInterface(
    SyntaxNode syntaxNode,
    CancellationToken cancellationToken)
    {
        var isOK = syntaxNode is InterfaceDeclarationSyntax;
        return isOK;

    }
    

}
