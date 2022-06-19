namespace DotNetToGitHubWiki.Model
{
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class PropertyDocItem : DocItem, IParameterizedDocItem
    {
        #region Fields
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowBody | ConversionFlags.ShowModifiers | ConversionFlags.ShowParameterDefaultValues | ConversionFlags.ShowParameterList | ConversionFlags.ShowParameterModifiers | ConversionFlags.ShowParameterNames | ConversionFlags.ShowReturnType | ConversionFlags.UseFullyQualifiedTypeNames };
        #endregion
        #region Constructors
        public PropertyDocItem(TypeDocItem parent, IProperty property, XElement documentation)
            : base(parent, property, documentation)
        {
            this.Property = property;
            this.Parameters = this.Property.Parameters.Select(p => new ParameterDocItem(this, p, documentation)).ToArray();
        }
        #endregion
        #region Properties
        public ParameterDocItem[] Parameters { get; }
        public IProperty Property { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle($"{this.Parent.Name}.{this.Name}", "Property");
            writer.Write(this, this.Documentation.GetSummary());
            writer.WriteLine("```csharp");
            writer.WriteLine(PropertyDocItem.CodeAmbience.ConvertSymbol(this.Property));
            writer.WriteLine("```");

            // attributes
            writer.WriteDocItems(this.Parameters, "#### Parameters");
            if (this.Property.ReturnType.Kind != TypeKind.Void)
            {
                writer.WriteLine("#### Property Value");
                writer.WriteLine(writer.GetTypeLink(this.Property.ReturnType) + "  ");
                writer.Write(this, this.Documentation.GetValue());
            }
            writer.WriteExceptions(this);
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}