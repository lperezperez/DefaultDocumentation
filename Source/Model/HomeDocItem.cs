namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    internal sealed class HomeDocItem : DocItem
    {
        #region Fields
        private readonly bool explicitGenerate;
        #endregion
        #region Constructors
        public HomeDocItem(string pageName, string name, XElement documentation)
            : base(null, string.Empty, pageName ?? "index", name, documentation) => this.explicitGenerate = !string.IsNullOrEmpty(pageName) || documentation != null;
        #endregion
        #region Properties
        public override bool GeneratePage => this.explicitGenerate || this.HasMultipleNamespaces;
        public bool HasMultipleNamespaces { get; set; }
        #endregion
        #region Methods
        public override string GetLink(FileNameMode fileNameMode) => this.FullName.Clean();
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.Write(this, this.Documentation.GetSummary());
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
            writer.WriteChildrenLink<NamespaceDocItem>("Namespaces");
        }
        #endregion
    }
}