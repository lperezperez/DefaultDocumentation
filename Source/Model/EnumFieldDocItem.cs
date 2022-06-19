namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class EnumFieldDocItem : DocItem
    {
        #region Constructors
        public EnumFieldDocItem(EnumDocItem parent, IField field, XElement documentation)
            : base(parent, field, documentation) => this.Field = field;
        #endregion
        #region Properties
        public IField Field { get; }
        public override bool GeneratePage => false;
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteLinkTarget(this);
            writer.WriteLine($"`{this.Name}` {this.Field.GetConstantValue()}  ");
            writer.Write(this, this.Documentation.GetSummary());
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}