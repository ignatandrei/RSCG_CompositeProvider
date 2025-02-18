
using Microsoft.CodeAnalysis;
using System.Text;

namespace RSCG_CompositeProvider;

internal class DataFromExposeInterface
{
    public string Name { get; private set; }
    public string DebuggerDisplay()  
    {
        
        StringBuilder sb = new StringBuilder("[System.Diagnostics.DebuggerDisplay(");
        sb.Append("\"");
        foreach (var item in props)
        {
            sb.Append(item.Name);
            sb.Append(" = {");
            sb.Append(item.Name);
            sb.Append("} " );
        }
        sb.Append("\")]");
        return sb.ToString();
    }
    internal IPropertySymbol[] props;
    public IMethodSymbol[] functions;
    //private INamedTypeSymbol type;
    public string CallFunc(IMethodSymbol methodSymbol)
    {
        
        var sdf = new SymbolDisplayFormat(
                globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                memberOptions:
                    SymbolDisplayMemberOptions.IncludeParameters |
                    SymbolDisplayMemberOptions.IncludeType |
                    SymbolDisplayMemberOptions.IncludeRef |
                    SymbolDisplayMemberOptions.IncludeContainingType,
                kindOptions:
                    SymbolDisplayKindOptions.None,
                parameterOptions:
                    SymbolDisplayParameterOptions.IncludeName |
                    SymbolDisplayParameterOptions.IncludeParamsRefOut ,
                localOptions: SymbolDisplayLocalOptions.IncludeType,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                miscellaneousOptions:
                    SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers |
                    SymbolDisplayMiscellaneousOptions.UseSpecialTypes |
                    SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

        var str = methodSymbol.ToDisplayString(sdf);
        //remove the interface name that can appear before arguments

        int dot = 0;
        do
        {
            var index = str.IndexOf('(');
            dot = str.IndexOf('.');
            if (dot < index && dot > 0) str = str.Substring(dot + 1);
            if (dot > index) break;

        } while (dot > 0);


        return str;
    }
    private bool IsAwaitable(IMethodSymbol methodSymbol)
    {

        var returnType = methodSymbol.ReturnType;
        if (returnType.Name == nameof(Task) ||
               (returnType.OriginalDefinition != null && returnType.OriginalDefinition.Name == nameof(Task)))
            return true;

        if (returnType.Name == nameof(ValueTask) ||
               (returnType.OriginalDefinition != null && returnType.OriginalDefinition.Name == nameof(ValueTask)))
            return true;
        if (returnType.AllInterfaces.Any(i => i.Name == nameof(IAsyncResult)))
            return true;

        return false;

    }
    public string AsyncMethod(IMethodSymbol methodSymbol)
    {
        return IsAwaitable( methodSymbol)? " async " : "";
    }
    public string AwaitMethod(IMethodSymbol methodSymbol)
    {
        return IsAwaitable(methodSymbol)? " await " : "";
    }
    public string DisplayFunc(IMethodSymbol methodSymbol)
    {
        //string IEmployee.GetFullName()
        var sdf=new  SymbolDisplayFormat(
                globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                memberOptions:
                    SymbolDisplayMemberOptions.IncludeParameters |
                    SymbolDisplayMemberOptions.IncludeType |
                    SymbolDisplayMemberOptions.IncludeRef |
                    SymbolDisplayMemberOptions.IncludeContainingType,
                kindOptions:
                    SymbolDisplayKindOptions.IncludeMemberKeyword,
                parameterOptions:
                    SymbolDisplayParameterOptions.IncludeName |
                    SymbolDisplayParameterOptions.IncludeType |
                    SymbolDisplayParameterOptions.IncludeParamsRefOut |
                    SymbolDisplayParameterOptions.IncludeDefaultValue,
                localOptions: SymbolDisplayLocalOptions.IncludeType,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                miscellaneousOptions:
                    SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers |
                    SymbolDisplayMiscellaneousOptions.UseSpecialTypes |
                    SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

        var str = methodSymbol.ToDisplayString(sdf);
        //remove the interface name that can appear before arguments
        
        int dot = 0;
        do
        {
            var index = str.IndexOf('(');
            dot = str.IndexOf('.');
            if (dot < index && dot > 0) str = str.Substring(dot + 1);
            if (dot > index) break;            

        } while (dot > 0 );
        

        return str;
    }
    public string HelpVisible(IMethodSymbol methodSymbol)
    {
        var key = methodSymbol.ReturnType.ToString();
        key = replaceString(key);
        return key;
    }
    private string replaceString(string key)
    {
        key = key.Replace(".", "_");
        key = key.Replace("[]", "_Array");
        key = key.Replace("<", "_Of_");
        key = key.Replace(">", "_EndOf");
        return key;
    }
    public string DefaultValueReturnFunc(IMethodSymbol methodSymbol)
    {
        var defReturn  = methodSymbol.ReturnType.ToString();
        var defaultReturn = $"return default({defReturn})";
        var key = defReturn;
        key = replaceString(key);
        return defaultReturn;
    }
    public string FullName { get; private set; }

    public DataFromExposeInterface(INamedTypeSymbol type)
    {
        //this.type = type;
        FullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        Name = type.Name;
        props = type.GetMembers()
                .OfType<IPropertySymbol>()
                .ToArray();
        functions = type.GetMembers()
                .OfType<IMethodSymbol>()
                .Where(x => x.MethodKind == MethodKind.Ordinary)
                .ToArray();
    }
}
