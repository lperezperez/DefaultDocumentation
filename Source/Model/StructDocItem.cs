namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class StructDocItem : TypeDocItem
    {
        #region Constructors
        public StructDocItem(DocItem parent, ITypeDefinition type, XElement documentation)
            : base(parent, type, documentation) { }
        #endregion
    }
}