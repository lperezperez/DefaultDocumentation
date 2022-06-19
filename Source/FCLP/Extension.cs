namespace DotNetToGitHubWiki.FCLP
{
    using System.Linq;
    using System.Text;
    using Fclp;
    using Fclp.Internals;
    internal static class Extension
    {
        #region Methods
        /// <summary>Gets the syntax for the <paramref name="optionName"/> in the specified <paramref name="fluentCommandLineParser"/>.</summary>
        /// <param name="fluentCommandLineParser">A <see cref="FluentCommandLineParser"/> instance.</param>
        /// <param name="optionName">The long name of the <see cref="ICommandLineOption"/> in <see cref="FluentCommandLineParser.Options"/>.</param>
        /// <returns>
        ///     The syntax for the <paramref name="optionName"/> in the specified <paramref name="fluentCommandLineParser"/> or <see langword="null"/> if not found.
        /// </returns>
        public static string GetSyntax(this FluentCommandLineParser fluentCommandLineParser, string optionName) => fluentCommandLineParser.Options.FirstOrDefault(o => o.LongName == optionName)?.GetSyntax();
        /// <summary>Gets the syntax for the name of the <paramref name="option"/>.</summary>
        /// <param name="option">An <see cref="ICommandLineOption"/> instance.</param>
        /// <returns>The syntax for the name of the <paramref name="option"/>.</returns>
        public static string GetSyntax(this ICommandLineOption option)
        {
            var syntax = new StringBuilder();
            // Add option prefix.
            if (option.HasShortName)
                syntax.Append($"-{option.ShortName}");
            if (option.HasShortName && option.HasLongName)
                syntax.Append('|');
            if (option.HasLongName)
                syntax.Append($"--{option.LongName}");
            // Add option type.
            syntax.Append($" <{option.SetupType.Name}>");
            // Mark option as not required.
            if (!option.IsRequired)
            {
                syntax.Insert(0, '[');
                syntax.Append(']');
            }

            // Return option.
            return syntax.ToString();
        }
        /// <summary>Gets the specified <paramref name="string"/> with the first character converted to lower case.</summary>
        /// <param name="string">The <see cref="string"/> to convert.</param>
        /// <returns>The specified <paramref name="string"/> with the first character converted to lower case.</returns>
        public static string RemoveTrailingChar(this string @string) => new[] { ' ', '.', ',', ';' }.Any(c => c == @string.Last()) ? @string.Remove(@string.Length - 1, 1) : @string;
        /// <summary>Gets the specified <paramref name="string"/> with the first character converted to lower case.</summary>
        /// <param name="string">The <see cref="string"/> to convert.</param>
        /// <returns>The specified <paramref name="string"/> with the first character converted to lower case.</returns>
        public static string ToLowerFirstChar(this string @string) => !string.IsNullOrEmpty(@string) && char.IsUpper(@string[0]) ? char.ToLower(@string[0]) + @string.Substring(1) : @string;
        #endregion
    }
}