namespace DotNetToGitHubWiki.FCLP
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using DotNetToGitHubWiki.Properties;
    using Fclp;
    using Fclp.Internals;
    using Fclp.Internals.Parsing.OptionParsers;
    using Humanizer;
    internal sealed class OptionFormatter : ICommandLineOptionFormatter
    {
        #region Constants
        private const byte TabSize = 2;
        #endregion
        #region Methods
        /// <summary>Displays the <paramref name="help"/> and exits the application.</summary>
        /// <param name="help">The application help.</param>
        public static void ShowHelp(string help)
        {
            Console.Write(help);
            Environment.Exit(Environment.ExitCode);
        }
        /// <summary>Gets a <typeparamref name="TAttribute"/> of the entry <see cref="Assembly"/>.</summary>
        /// <typeparam name="TAttribute">An <see cref="Attribute"/> <see langword="class"/></typeparam>
        /// <returns>A <typeparamref name="TAttribute"/> instance.</returns>
        private static TAttribute GetAssemblyAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            var attributes = Assembly.GetEntryAssembly()?.GetCustomAttributes(typeof(TAttribute), false);
            return attributes?.Length == 0 ? null : attributes?.OfType<TAttribute>().SingleOrDefault();
        }
        private static int PadLine(StringBuilder stringBuilder, string text, int padLeft, int writtenLength = 0)
        {
            var length = Math.Min(Console.WindowWidth - padLeft, text.Length - writtenLength);
            if (writtenLength != 0)
                stringBuilder.Append(' ', padLeft);
            stringBuilder.AppendLine(text.Substring(writtenLength, length));
            return length;
        }
        private static void PadText(StringBuilder stringBuilder, string text, int padLeft)
        {
            var writtenLength = OptionFormatter.PadLine(stringBuilder, text, padLeft);
            while (writtenLength < text.Length)
                writtenLength += OptionFormatter.PadLine(stringBuilder, text, padLeft, writtenLength);
        }
        private static IEnumerable<KeyValuePair<string, ICommandLineOption>> ParseOptions(IEnumerable<ICommandLineOption> options) => options.Select(option => new KeyValuePair<string, ICommandLineOption>($"{option.GetSyntax()} ".PadLeft(OptionFormatter.TabSize), option));
        /// <summary>Formats the list of <see cref="ICommandLineOption"/> to be displayed to the user.</summary>
        /// <param name="options">The list of <see cref="ICommandLineOption"/> to format.</param>
        /// <returns>A <see cref="System.String"/> representing the format</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="options"/> is <c>null</c>.</exception>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public string Format(IEnumerable<ICommandLineOption> options)
        {
            // Parse help
            var help = new StringBuilder($"{Environment.NewLine}{OptionFormatter.GetAssemblyAttribute<AssemblyProductAttribute>().Product} {OptionFormatter.GetAssemblyAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion} ({OptionFormatter.GetAssemblyAttribute<AssemblyCopyrightAttribute>().Copyright})");
            help.AppendLine();
            help.AppendLine(Resources.Description);
            help.AppendLine();
            help.AppendLine($"{Resources.Syntax.ToUpper()}:");
            var parsedOptions = OptionFormatter.ParseOptions(options.Where(o => !string.IsNullOrEmpty(o.Description)).Concat(new[] { new CommandLineOption<string>("?", "help", new StringCommandLineOptionParser()) { Description = Resources.Help } }));
            help.Append(Assembly.GetEntryAssembly()?.ManifestModule.Name);
            help.Append(' ');
            help.AppendLine(string.Concat(parsedOptions.Select(p => p.Key).ToArray()));
            help.AppendLine();
            help.AppendLine($"{Resources.Parameters.ToUpper()}:");
            var maxSyntaxLength = parsedOptions.Max(o => o.Key.Length);
            var padded = maxSyntaxLength < Console.WindowWidth / 2;
            foreach (var (key, value) in parsedOptions)
            {
                if (padded)
                {
                    help.Append(key);
                    help.Append(' ', maxSyntaxLength - key.Length);
                    OptionFormatter.PadText(help, value.Description, maxSyntaxLength);
                    if (value.SetupType.IsEnum)
                    {
                        help.AppendLine($"{Resources.Values.ToUpper()}:");
                        var enumNames = Enum.GetNames(value.SetupType);
                        var maxEnumLength = Math.Max(maxSyntaxLength, enumNames.Max(e => e.Length) + OptionFormatter.TabSize + 2);
                        foreach (var enumName in enumNames)
                        {
                            help.Append(' ', OptionFormatter.TabSize);
                            help.Append(enumName + ':');
                            help.Append(' ', maxEnumLength - (OptionFormatter.TabSize + enumName.Length + 1));
                            OptionFormatter.PadText(help, ((Enum)Enum.Parse(value.SetupType, enumName)).Humanize(), maxEnumLength);
                        }
                    }
                }
                else
                {
                    help.AppendLine($"{key.RemoveTrailingChar()}:");
                    help.AppendLine(value.Description);
                    if (value.SetupType.IsEnum)
                    {
                        help.AppendLine($"{Resources.Values.ToUpper()}:");
                        var enumNames = Enum.GetNames(value.SetupType);
                        var maxEnumLength = enumNames.Max(e => e.Length) + OptionFormatter.TabSize + 2;
                        foreach (var enumName in enumNames)
                        {
                            help.Append(' ', OptionFormatter.TabSize);
                            help.Append(enumName + ':');
                            help.Append(' ', maxEnumLength - (OptionFormatter.TabSize + enumName.Length + 1));
                            OptionFormatter.PadText(help, ((Enum)Enum.Parse(value.SetupType, enumName)).Humanize(), maxEnumLength);
                        }
                    }
                }
                help.AppendLine();
            }
            return help.ToString();
        }
        #endregion
    }
}