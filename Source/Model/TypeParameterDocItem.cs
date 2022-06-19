namespace DotNetToGitHubWiki.Model
{
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class TypeParameterDocItem : DocItem
    {
        #region Constructors
        public TypeParameterDocItem(DocItem parent, ITypeParameter entity, XElement documentation)
            : base(parent, entity.Name, $"{parent.FullName}.{entity.Name}", entity.Name, documentation.GetTypeParameters()?.FirstOrDefault(d => d.GetName() == entity.Name)) => this.TypeParameter = entity;
        #endregion
        #region Properties
        public override bool GeneratePage => false;
        public ITypeParameter TypeParameter { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteLinkTarget(this);
            writer.WriteLine($"`{this.TypeParameter.Name}`  ");
            writer.Write(this, this.Documentation);
        }
        #endregion
    }
}