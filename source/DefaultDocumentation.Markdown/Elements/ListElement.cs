﻿using System.Globalization;
using System.Xml.Linq;
using DefaultDocumentation.Markdown.Extensions;
using DefaultDocumentation.Api;

namespace DefaultDocumentation.Markdown.Elements
{
    public sealed class ListElement : IElement
    {
        private static void WriteBullet(IWriter writer, XElement element)
        {
            foreach (XElement item in element.GetItems())
            {
                IWriter listWriter =
                    writer
                        .EnsureLineStart()
                        .ToPrefixedWriter("  ");

                writer.Append("- ");
                WriteItem(listWriter, item);
            }
        }

        private static void WriteNumber(IWriter writer, XElement element)
        {
            int count = 1;

            foreach (XElement item in element.Elements())
            {
                IWriter listWriter =
                    writer
                        .EnsureLineStart()
                        .ToPrefixedWriter("   ");

                writer.Append(count++.ToString(CultureInfo.InvariantCulture)).Append(". ");
                WriteItem(listWriter, item);
            }
        }

        private static void WriteItem(IWriter writer, XElement element)
        {
            XElement term = element.GetTerm(),
                     description = element.GetDescription();

            // If both a term and a description are present, separate them by an em dash 
            if (term is not null && description is not null)
            {
                writer
                    .AppendAsMarkdown(term)
                    .Append(" — ")
                    .AppendAsMarkdown(description);
            }
            // Otherwise, write one of the present items or the parent
            else
            {
                writer.AppendAsMarkdown(description ?? term ?? element);
            }
        }

        private static void WriteTable(IWriter writer, XElement element)
        {
            int columnCount = 0;

            writer
                .EnsureLineStartAndAppendLine()
                .Append("|");

            // Both include descriptions and terms
            foreach (XElement description in element.GetListHeader().Elements())
            {
                ++columnCount;

                writer
                    .SetDisplayAsSingleLine(true)
                    .AppendAsMarkdown(description)
                    .SetDisplayAsSingleLine(false)
                    .Append("|");
            }

            if (columnCount > 0)
            {
                writer
                    .EnsureLineStart()
                    .Append("|");

                while (columnCount-- > 0)
                {
                    writer.Append("-|");
                }

                foreach (XElement item in element.GetItems())
                {
                    writer
                        .EnsureLineStart()
                        .Append("|");

                    // Both include descriptions and terms
                    foreach (XElement description in item.Elements())
                    {
                        writer
                            .SetDisplayAsSingleLine(true)
                            .AppendAsMarkdown(description)
                            .SetDisplayAsSingleLine(false)
                            .Append("|");
                    }
                }

                writer.EnsureLineStartAndAppendLine();
            }
        }

        public string Name => "list";

        public void Write(IWriter writer, XElement element)
        {
            if (writer.GetDisplayAsSingleLine())
            {
                return;
            }

            switch (element.GetTypeAttribute())
            {
                case "bullet":
                    WriteBullet(writer, element);
                    break;

                case "number":
                    WriteNumber(writer, element);
                    break;

                case "table":
                    WriteTable(writer, element);
                    break;
            }
        }
    }
}
