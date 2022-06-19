namespace DotNetToGitHubWiki
{
    using System.ComponentModel.DataAnnotations;
    using DotNetToGitHubWiki.Properties;
    /// <summary>The diferent modes to specify the name of each Markdown file.</summary>
    public enum FileNameMode
    {
        /// <summary>Sets the full qualified name of the member as the Markdown file name.</summary>
        [Display(Description = "FileNameModeFullName", ResourceType = typeof(Resources))]
        FullName,
        /// <summary>
        ///     Sets the type and member name without the namespace as the Markdown page name. May get collision if project has multiple types with the same name in different namespaces.
        /// </summary>
        [Display(Description = "FileNameModeName", ResourceType = typeof(Resources))]
        Name,
        /// <summary>Sets an MD5 hash (from the full name of the member) as the Markdown file name.</summary>
        [Display(Description = "FileNameModeMd5", ResourceType = typeof(Resources))]
        Md5
    }
}