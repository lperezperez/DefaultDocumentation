namespace DotNetToGitHubWiki
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using DotNetToGitHubWiki.FCLP;
    using DotNetToGitHubWiki.Helper;
    using DotNetToGitHubWiki.Properties;
    using Fclp;
    using Humanizer;
    /// <summary>Entry class for the application.</summary>
    internal static class Program
    {
        #region Fields
        private static string assembly;
        private static string baseLink;
        private static IEnumerable<string> externalLinks;
        private static FileNameMode fileNameMode;
        private static string home;
        private static string linksFile;
        private static NestedTypeVisibility nestedTypeVisibility;
        private static string output;
        private static string xml;
        #endregion
        #region Methods
        /// <summary>Entry method for the application.</summary>
        /// <param name="args">The command-line arguments passed to the application.</param>
        private static void Main(string[] args)
        {
            var outputFolder = Program.ParseArgs(args);
            foreach (var file in outputFolder.GetFiles($"*{DocumentationGenerator.MarkdownExtension}"))
                file.Delete();
            var generator = new DocumentationGenerator(Program.assembly, Program.xml, Program.home, Program.fileNameMode, Program.nestedTypeVisibility, Program.externalLinks);
            generator.WriteDocumentation(Program.output);
            if (Program.linksFile != null)
            {
                if (File.Exists(Program.linksFile))
                    File.Delete(Program.linksFile);
                generator.WriteLinks(Program.baseLink, Program.linksFile);
            }
#if !DEBUG
            File.Delete(Program.xml);
#endif
        }
        /// <summary>Parses the <paramref name="args"/>.</summary>
        /// <param name="args">The command-line arguments passed to the application.</param>
        private static DirectoryInfo ParseArgs(string[] args)
        {
            var argsParser = new FluentCommandLineParser { ErrorFormatter = new ErrorFormatter(), IsCaseSensitive = false };
            argsParser.Setup<string>('a', nameof(Program.assembly)).WithDescription(Resources.Assembly).Callback(a => Program.assembly = a).Required();
            argsParser.Setup<string>('x', nameof(Program.xml)).WithDescription(Resources.Xml).Callback(x => Program.xml = x).Required();
            argsParser.Setup<string>('b', nameof(Program.baseLink)).WithDescription(Resources.BaseLink).Callback(b => Program.baseLink = b);
            argsParser.Setup<List<string>>('e', nameof(Program.externalLinks)).WithDescription(Resources.ExternalLinks).Callback(e => Program.externalLinks = e).SetDefault(new List<string>());
            argsParser.Setup<FileNameMode>('f', nameof(Program.fileNameMode)).WithDescription(Resources.FileNameMode).Callback(f => Program.fileNameMode = f).SetDefault(FileNameMode.FullName);
            argsParser.Setup<string>('h', nameof(Program.home)).WithDescription(Resources.Home).Callback(h => Program.home = h).SetDefault("Home");
            argsParser.Setup<string>('i', nameof(MarkdownExtensions.InvalidCharReplacement).Camelize()).WithDescription(Resources.InvalidCharReplacement).Callback(i => MarkdownExtensions.InvalidCharReplacement = i).SetDefault("-");
            argsParser.Setup<string>('l', nameof(Program.linksFile)).WithDescription(Resources.LinksFile).Callback(l => Program.linksFile = l);
            argsParser.Setup<NestedTypeVisibility>('n', nameof(Program.nestedTypeVisibility)).WithDescription(Resources.NestedTypeVisibility).Callback(n => Program.nestedTypeVisibility = n).SetDefault(NestedTypeVisibility.Namespace);
            argsParser.Setup<string>('o', nameof(Program.output)).WithDescription(Resources.Output).Callback(o => Program.output = o);
            argsParser.SetupHelp("?", "help").UseForEmptyArgs().WithCustomFormatter(new OptionFormatter()).Callback(h => OptionFormatter.ShowHelp(h));
            var parserResult = argsParser.Parse(args);
            if (parserResult.HasErrors)
                ErrorFormatter.ShowErrors(parserResult.ErrorText);
            var errors = new StringBuilder();
            if (!File.Exists(Program.assembly))
                errors.AppendLine(ErrorFormatter.Format(Resources.Assembly, argsParser.GetSyntax(nameof(Program.assembly)), Resources.NotFound.FormatWith(Program.assembly)));
            if (!File.Exists(Program.xml))
                errors.AppendLine(ErrorFormatter.Format(Resources.Xml, argsParser.GetSyntax(nameof(Program.xml)), Resources.NotFound.FormatWith(Program.xml)));
            if (errors.Length > 0)
                ErrorFormatter.ShowErrors(errors.ToString());
            Program.output ??= Path.GetDirectoryName(Program.xml);
            try
            {
                return Directory.CreateDirectory(Program.output);
            }
            catch
            {
                ErrorFormatter.ShowErrors($"{ErrorFormatter.Format(Resources.Output, argsParser.GetSyntax(nameof(Program.output)), Resources.CannotCreate.FormatWith(Program.output))}{Environment.NewLine}");
            }
            return null;
        }
        #endregion
    }
}