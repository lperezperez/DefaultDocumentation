namespace DotNetToGitHubWiki.FCLP
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using DotNetToGitHubWiki.Properties;
    using Fclp;
    using Fclp.Internals.Errors;
    using Fclp.Internals.Extensions;
    using Humanizer;
    /// <summary>A parser error formatter designed to create error descriptions suitable for the console.</summary>
    internal sealed class ErrorFormatter : ICommandLineParserErrorFormatter
    {
        #region Methods
        /// <summary>
        ///     Formats the specified <paramref name="paramDescription"/>, <paramref name="paramSyntax"/> and <paramref name="errorMessage"/> to a <see cref="string"/> suitable for the end user.
        /// </summary>
        /// <param name="paramDescription">The description of the parameter.</param>
        /// <param name="paramSyntax">The parameter syntax.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>A <see cref="string"/> describing the specified errors.</returns>
        public static string Format(string paramDescription, string paramSyntax, string errorMessage) => $"{paramDescription.RemoveTrailingChar()} ({Resources.Parameters.Singularize()}: {paramSyntax}) {errorMessage.ToLowerFirstChar()}";
        /// <summary>
        ///     Displays errors during parameter parsing and exits the application indicating the <see href="https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-#ERROR_BAD_ARGUMENTS">corresponding error code</see>.
        /// </summary>
        /// <param name="message">A <see cref="string"/> containing error messages.</param>
        public static void ShowErrors(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Environment.Exit(160);
        }
        /// <summary>
        ///     Formats the specified list of <see cref="ICommandLineParserError"/> to a <see cref="string"/> suitable for the end user.
        /// </summary>
        /// <param name="parserErrors">The errors to format.</param>
        /// <returns>A <see cref="string"/> describing the specified errors.</returns>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public string Format(IEnumerable<ICommandLineParserError> parserErrors)
        {
            if (parserErrors.IsNullOrEmpty()) return null;
            var errors = new StringBuilder();
            foreach (var error in parserErrors)
                errors.AppendLine(this.Format(error));
            return errors.ToString();
        }
        /// <summary>Formats the specified <see cref="ICommandLineParserError"/> to a <see cref="string"/> suitable for the end user.</summary>
        /// <param name="parserError">The error to format. This must not be null.</param>
        /// <returns>A <see cref="string"/> describing the specified error.</returns>
        public string Format(ICommandLineParserError parserError) => parserError switch
        {
            OptionSyntaxParseError optionSyntaxParseError => ErrorFormatter.Format(parserError.Option.Description, optionSyntaxParseError.ParsedOption.RawKey, Resources.CannotConvert.FormatWith(optionSyntaxParseError.ParsedOption.Value ?? "<null>", optionSyntaxParseError.Option.SetupType.Name)), ExpectedOptionNotFoundParseError _ => ErrorFormatter.Format(parserError.Option.Description, parserError.Option.GetSyntax(), Resources.NotSpecified), _ => ErrorFormatter.Format(parserError.Option.Description, parserError.Option.GetSyntax(), Resources.CannotParse)
        };
        #endregion
    }
}