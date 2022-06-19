namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class FieldDocItem : DocItem
    {
        #region Fields
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowReturnType | ConversionFlags.ShowDefinitionKeyword | ConversionFlags.ShowModifiers };
        #endregion
        #region Constructors
        public FieldDocItem(TypeDocItem parent, IField field, XElement documentation)
            : base(parent, field, documentation) => this.Field = field;
        #endregion
        #region Properties
        public IField Field { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle($"{this.Parent.Name}.{this.Name}", "Field");
            writer.Write(this, this.Documentation.GetSummary());
            writer.WriteLine("```csharp");
            writer.WriteLine($"{FieldDocItem.CodeAmbience.ConvertSymbol(this.Field)}{(this.Field.IsConst ? $" = {this.Field.GetConstantValue()}" : string.Empty)};");
            writer.WriteLine("```");
            // TODO: Write attributes
            writer.WriteLine("#### Field Value");
            writer.WriteLine($"{writer.GetTypeLink(this.Field.Type)}  ");
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}