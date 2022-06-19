namespace DotNetToGitHubWiki.Helper
{
    using ICSharpCode.Decompiler.TypeSystem;
    using ICSharpCode.Decompiler.TypeSystem.Implementation;
    internal static class TypeExtension
    {
        #region Methods
        public static IType RemoveReference(this IType type) => type is TypeWithElementType realType ? realType.ElementType : type;
        #endregion
    }
}