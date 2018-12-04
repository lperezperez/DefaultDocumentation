﻿using System.IO;
using System.Xml.Linq;

namespace DefaultApiDocumentation
{
    internal static class Program
    {
        private static void Main()
        {
            foreach (FileInfo file in new DirectoryInfo(@"D:\Projects\DefaultEcs.wiki").GetFiles("*.md"))
            {
                file.Delete();
            }

            Converter.Convert(
                "DefaultEcs",
                XDocument.Parse(File.ReadAllText(@"D:\Projects\DefaultEcs\documentation\DefaultEcs.xml")),
                @"D:\Projects\DefaultEcs\documentation\api\");
        }
    }
}
