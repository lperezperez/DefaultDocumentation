namespace DotNetToGitHubWiki.Model
{
    internal interface ITypeParameterizedDocItem
    {
        #region Properties
        TypeParameterDocItem[] TypeParameters { get; }
        #endregion
    }
}