namespace DotNetToGitHubWiki.Helper
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    internal static class XElementExtension
    {
        #region Methods
        public static XElement GetExample(this XElement element) => element?.Element("example");
        public static IEnumerable<XElement> GetExceptions(this XElement element) => element?.Elements("exception");
        public static string GetLangWord(this XElement element) => element.Attribute("langword")?.Value;
        public static string GetName(this XElement element) => element.Attribute("name")?.Value;
        public static IEnumerable<XElement> GetParameters(this XElement element) => element?.Elements("param");
        public static string GetReferenceName(this XElement element) => element.Attribute("cref")?.Value;
        public static XElement GetRemarks(this XElement element) => element?.Element("remarks");
        public static XElement GetReturns(this XElement element) => element?.Element("returns");
        public static XElement GetSummary(this XElement element) => element?.Element("summary");
        public static IEnumerable<XElement> GetTypeParameters(this XElement element) => element?.Elements("typeparam");
        public static string GetUrl(this XElement element) => element.Attribute("href")?.Value;
        public static XElement GetValue(this XElement element) => element?.Element("value");
        #endregion
    }
}