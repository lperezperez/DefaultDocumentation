namespace DotNetToGitHubWiki.Helper
{
    using System;
    using DotNetToGitHubWiki.Model;
    internal static class DocItemExtension
    {
        #region Methods
        public static DocItem GetPagedDocItem(this DocItem item)
        {
            while (item != null)
            {
                if (item.GeneratePage)
                    break;
                item = item.Parent;
            }
            return item;
        }
        public static bool TryGetParameterDocItem(this DocItem item, string name, out ParameterDocItem parameterDocItem)
        {
            parameterDocItem = null;
            while (item != null && parameterDocItem == null)
            {
                if (item is IParameterizedDocItem typeParameters)
                    parameterDocItem = Array.Find(typeParameters.Parameters, i => i.Parameter.Name == name);
                item = item.Parent;
            }
            return parameterDocItem != null;
        }
        public static bool TryGetTypeParameterDocItem(this DocItem item, string name, out TypeParameterDocItem typeParameterDocItem)
        {
            typeParameterDocItem = null;
            while (item != null && typeParameterDocItem == null)
            {
                if (item is ITypeParameterizedDocItem typeParameters)
                    typeParameterDocItem = Array.Find(typeParameters.TypeParameters, i => i.TypeParameter.Name == name);
                item = item.Parent;
            }
            return typeParameterDocItem != null;
        }
        #endregion
    }
}