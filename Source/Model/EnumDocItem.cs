namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class EnumDocItem : TypeDocItem
    {
        #region Fields
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowDeclaringType | ConversionFlags.ShowDefinitionKeyword | ConversionFlags.ShowModifiers | ConversionFlags.ShowTypeParameterList | ConversionFlags.ShowTypeParameterVarianceModifier };
        #endregion
        #region Constructors
        public EnumDocItem(DocItem parent, ITypeDefinition type, XElement documentation)
            : base(parent, type, documentation) { }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle(this.Name, this.Type.Kind.ToString());
            writer.Write(this, this.Documentation.GetSummary());
            writer.WriteLine("```csharp");
            writer.Write(EnumDocItem.CodeAmbience.ConvertSymbol(this.Type));
            var enumType = this.Type.GetEnumUnderlyingType();
            writer.WriteLine(enumType.IsKnownType(KnownTypeCode.Int32) ? string.Empty : $" : {enumType.FullName}");
            writer.WriteLine("```");

            // attribute
            writer.WriteDocItems<EnumFieldDocItem>("### Fields");
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}