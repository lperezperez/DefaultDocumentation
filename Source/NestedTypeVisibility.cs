namespace DotNetToGitHubWiki
{
    using System.ComponentModel.DataAnnotations;
    using DotNetToGitHubWiki.Properties;
    /// <summary>The visibility type for nested types.</summary>
    public enum NestedTypeVisibility
    {
        /// <summary>Nested type links are showed on the namespace page.</summary>
        [Display(Description = "NestedTypeVisibilityNamespace", ResourceType = typeof(Resources))]
        Namespace,
        /// <summary>Nested type links are showed on their declaring type page.</summary>
        [Display(Description = "NestedTypeVisibilityDeclaringType", ResourceType = typeof(Resources))]
        DeclaringType,
        /// <summary>Nested type links are showed on both the namespace and their declaring type page.</summary>
        [Display(Description = "NestedTypeVisibilityEverywhere", ResourceType = typeof(Resources))]
        Everywhere
    }
}