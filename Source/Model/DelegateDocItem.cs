namespace DotNetToGitHubWiki.Model
{
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class DelegateDocItem : TypeDocItem, IParameterizedDocItem
    {
        #region Fields
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowBody | ConversionFlags.ShowDeclaringType | ConversionFlags.ShowDefinitionKeyword | ConversionFlags.ShowModifiers | ConversionFlags.ShowParameterList | ConversionFlags.ShowParameterModifiers | ConversionFlags.ShowParameterNames | ConversionFlags.ShowReturnType | ConversionFlags.ShowTypeParameterList | ConversionFlags.ShowTypeParameterVarianceModifier | ConversionFlags.UseFullyQualifiedTypeNames };
        #endregion
        #region Constructors
        public DelegateDocItem(DocItem parent, ITypeDefinition type, XElement documentation)
            : base(parent, type, documentation)
        {
            this.InvokeMethod = type.GetDelegateInvokeMethod();
            this.Parameters = this.InvokeMethod.Parameters.Select(p => new ParameterDocItem(this, p, documentation)).ToArray();
        }
        #endregion
        #region Properties
        public IMethod InvokeMethod { get; }
        public ParameterDocItem[] Parameters { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle(this.Name, this.Type.Kind.ToString());
            writer.Write(this, this.Documentation.GetSummary());
            writer.WriteLine("```csharp");
            writer.WriteLine(DelegateDocItem.CodeAmbience.ConvertSymbol(this.Type));
            writer.WriteLine("```");

            // attribute
            writer.WriteDocItems(this.TypeParameters, "#### Type parameters");
            writer.WriteDocItems(this.Parameters, "#### Parameters");
            if (this.InvokeMethod.ReturnType.Kind != TypeKind.Void)
            {
                writer.WriteLine("#### Returns");
                writer.WriteLine(writer.GetTypeLink(this.InvokeMethod.ReturnType) + "  ");
                writer.Write(this, this.Documentation.GetReturns());
            }
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}