namespace DotNetToGitHubWiki.Model
{
    internal interface IParameterizedDocItem
    {
        #region Properties
        ParameterDocItem[] Parameters { get; }
        #endregion
    }
}