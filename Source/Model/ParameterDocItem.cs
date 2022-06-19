namespace DotNetToGitHubWiki.Model
{
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class ParameterDocItem : DocItem
    {
        #region Constructors
        public ParameterDocItem(DocItem parent, IParameter entity, XElement documentation)
            : base(parent, entity.Name, $"{parent.FullName}.{entity.Name}", entity.Name, documentation.GetParameters()?.FirstOrDefault(d => d.GetName() == entity.Name)) => this.Parameter = entity;
        #endregion
        #region Properties
        public override bool GeneratePage => false;
        public IParameter Parameter { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteLinkTarget(this);
            writer.WriteLine($"`{this.Parameter.Name}` {writer.GetTypeLink(this.Parameter.Type)}  ");
            writer.Write(this, this.Documentation);
        }
        #endregion
    }
}