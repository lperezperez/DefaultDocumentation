namespace DotNetToGitHubWiki.Helper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    internal static class MarkdownExtensions
    {
        #region Constants
        private const string DefaultChar = "-";
        #endregion
        #region Fields
        private static readonly Dictionary<string, string> InvalidStrings = new Dictionary<string, string>(Path.GetInvalidFileNameChars().ToDictionary(c => $"{c}", _ => MarkdownExtensions.DefaultChar)) { ["="] = string.Empty, [" "] = string.Empty, [","] = "_", ["."] = MarkdownExtensions.DefaultChar, ["["] = MarkdownExtensions.DefaultChar, ["]"] = MarkdownExtensions.DefaultChar, ["&lt;"] = MarkdownExtensions.DefaultChar, ["&gt;"] = MarkdownExtensions.DefaultChar };
        #endregion
        #region Methods
        public static string ToDotNetApiLink(this string value, string displayedName = null)
        {
            var link = value;
            var parametersIndex = link.IndexOf("(", StringComparison.Ordinal);
            if (parametersIndex > 0)
            {
                var methodName = link.Substring(0, parametersIndex);
                link = $"{methodName}#{link.Replace('.', '_').Replace('`', '_').Replace('(', '_').Replace(')', '_')}";
            }
            return $"https://docs.microsoft.com/dotnet/api/{link.Replace('`', '-')}".ToLink(displayedName ?? value, value);
        }
        public static string ToLink(this string value, string text, string title = null) => $"[{text.Prettify()}]({value} '{title ?? value}')";
        public static string InvalidCharReplacement
        {
            get => MarkdownExtensions.InvalidStrings.FirstOrDefault().Value;
            set
            {
                foreach (var key in MarkdownExtensions.InvalidStrings.Keys.ToList())
                    MarkdownExtensions.InvalidStrings[key] = value;
            }
        }
        public static string Clean(this string value) => MarkdownExtensions.InvalidStrings.Aggregate(value, (current, pair) => current.Replace(pair.Key, pair.Value));
        private static string Prettify(this string value)
        {
            var genericIndex = value.IndexOf('`');
            if (genericIndex > 0)
            {
                var memberIndex = value.IndexOf('.', genericIndex);
                var argsIndex = value.IndexOf('(', genericIndex);
                if (memberIndex > 0)
                    value = $"{value.Substring(0, genericIndex)}&lt;&gt;{value.Substring(memberIndex).Prettify()}";
                else if (argsIndex > 0)
                    value = $"{value.Substring(0, genericIndex)}&lt;&gt;{value.Substring(argsIndex).Prettify()}";
                else if (value.IndexOf('(') < 0)
                    value = $"{value.Substring(0, genericIndex)}&lt;&gt;";
            }
            return value.Replace('`', '@');
        }
        #endregion
    }
}