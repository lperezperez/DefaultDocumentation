namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class ClassDocItem : TypeDocItem
    {
        #region Constructors
        public ClassDocItem(DocItem parent, ITypeDefinition type, XElement documentation)
            : base(parent, type, documentation) { }
        #endregion
    }
}