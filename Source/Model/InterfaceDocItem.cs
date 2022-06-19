namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class InterfaceDocItem : TypeDocItem
    {
        #region Constructors
        public InterfaceDocItem(DocItem parent, ITypeDefinition type, XElement documentation)
            : base(parent, type, documentation) { }
        #endregion
    }
}