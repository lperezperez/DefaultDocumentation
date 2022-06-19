namespace DotNetToGitHubWiki.Model
{
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class ConstructorDocItem : DocItem, IParameterizedDocItem
    {
        #region Fields
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowBody | ConversionFlags.ShowModifiers | ConversionFlags.ShowParameterDefaultValues | ConversionFlags.ShowParameterList | ConversionFlags.ShowParameterModifiers | ConversionFlags.ShowParameterNames | ConversionFlags.ShowReturnType | ConversionFlags.ShowTypeParameterList | ConversionFlags.ShowTypeParameterVarianceModifier | ConversionFlags.UseFullyQualifiedTypeNames };
        #endregion
        #region Constructors
        public ConstructorDocItem(TypeDocItem parent, IMethod method, XElement documentation)
            : base(parent, method, documentation)
        {
            this.Method = method;
            this.Parameters = method.Parameters.Select(p => new ParameterDocItem(this, p, documentation)).ToArray();
        }
        #endregion
        #region Properties
        public IMethod Method { get; }
        public ParameterDocItem[] Parameters { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle(this.Name, "Constructor");
            writer.Write(this, this.Documentation.GetSummary());
            writer.WriteLine("```csharp");
            writer.WriteLine(ConstructorDocItem.CodeAmbience.ConvertSymbol(this.Method));
            writer.WriteLine("```");

            // attributes
            writer.WriteDocItems(this.Parameters, "#### Parameters");
            writer.WriteExceptions(this);
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}