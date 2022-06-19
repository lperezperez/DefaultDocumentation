namespace DotNetToGitHubWiki
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using DotNetToGitHubWiki.Model;
    using Humanizer;
    using ICSharpCode.Decompiler.Documentation;
    using ICSharpCode.Decompiler.TypeSystem;
    using ICSharpCode.Decompiler.TypeSystem.Implementation;
    internal sealed class DocumentationWriter : IDisposable
    {
        private const string DotNetKeywordBaseLink = "https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/";
        #region Fields
        private static readonly ConcurrentQueue<StringBuilder> Builders = new ConcurrentQueue<StringBuilder>();
        private readonly StringBuilder builder;
        private readonly FileNameMode fileNameMode;
        private readonly string filePath;
        private readonly IReadOnlyDictionary<string, DocItem> items;
        private readonly IReadOnlyDictionary<string, string> links;
        private readonly DocItem mainItem;
        #endregion
        #region Constructors
        public DocumentationWriter(FileNameMode fileNameMode, NestedTypeVisibility nestedTypeVisibility, IReadOnlyDictionary<string, DocItem> items, IReadOnlyDictionary<string, string> links, string folderPath, DocItem item)
        {
            if (!DocumentationWriter.Builders.TryDequeue(out this.builder))
                this.builder = new StringBuilder(1024);
            this.fileNameMode = fileNameMode;
            this.NestedTypeVisibility = nestedTypeVisibility;
            this.items = items;
            this.links = links;
            this.mainItem = item;
            this.filePath = Path.Combine(folderPath, $"{item.GetLink(this.fileNameMode)}{DocumentationGenerator.MarkdownExtension}");
        }
        #endregion
        #region Properties
        public IEnumerable<DocItem> KnownItems => this.items.Values;
        public NestedTypeVisibility NestedTypeVisibility { get; }
        #endregion
        #region Methods
        public void Break() => this.builder.AppendLine();
        public void Dispose()
        {
            File.WriteAllText(this.filePath, this.builder.ToString());
            this.builder.Clear();
            DocumentationWriter.Builders.Enqueue(this.builder);
        }
        public string GetLink(DocItem item, string displayedName = null) => item.GeneratePage ? $"[{displayedName ?? item.Name}](./{item.GetLink(this.fileNameMode)}{DocumentationGenerator.MarkdownExtension} '{item.FullName}')" : this.GetInnerLink(item, displayedName);
        public string GetTypeLink(IType type)
        {
            string HandleParameterizedType(ParameterizedType genericType)
            {
                var typeDefinition = genericType.GetDefinition();
                if (typeDefinition != null && this.items.TryGetValue(typeDefinition.GetIdString(), out var docItem) && docItem is TypeDocItem typeDocItem)
                    return this.GetLink(docItem, typeDocItem.Type.FullName + "&lt;") + string.Join(this.GetLink(docItem, ","), genericType.TypeArguments.Select(this.GetTypeLink)) + this.GetLink(docItem, "&gt;");
                return genericType.GenericType.ReflectionName.ToDotNetApiLink(genericType.FullName + "&lt;") + string.Join(genericType.GenericType.ReflectionName.ToDotNetApiLink(","), genericType.TypeArguments.Select(this.GetTypeLink)) + genericType.GenericType.ReflectionName.ToDotNetApiLink("&gt;");
            }
            string HandleTupleType(TupleType tupleType) => tupleType.FullName.ToDotNetApiLink(tupleType.FullName + "&lt;") + string.Join(tupleType.FullName.ToDotNetApiLink(","), tupleType.ElementTypes.Select(this.GetTypeLink)) + tupleType.FullName.ToDotNetApiLink("&gt;");
            return type.Kind switch
            {
                TypeKind.Array when type is TypeWithElementType arrayType => this.GetTypeLink(arrayType.ElementType) + "System.Array".ToDotNetApiLink("[]"),
                TypeKind.Pointer when type is TypeWithElementType pointerType => this.GetTypeLink(pointerType.ElementType) + "*",
                TypeKind.ByReference when type is TypeWithElementType innerType => this.GetTypeLink(innerType.ElementType),
                TypeKind.TypeParameter => this.mainItem.TryGetTypeParameterDocItem(type.Name, out var typeParameter) ? this.GetInnerLink(typeParameter) : type.Name,
                TypeKind.Dynamic => "[dynamic](https://docs.microsoft.com/dotnet/csharp/programming-guide/types/using-type-dynamic 'dynamic')",
                TypeKind.Tuple when type is TupleType tupleType => HandleTupleType(tupleType),
                TypeKind.Unknown => type.FullName.ToDotNetApiLink(),
                _ when type is ParameterizedType genericType => HandleParameterizedType(genericType),
                _ => this.GetLink(type.GetDefinition().GetIdString())
            };
        }
        public void Write(string line) => this.builder.Append(line);
        public void Write(DocItem item, XElement element) => this.Write(null, element, item);
        public void Write(string title, XElement element, DocItem item)
        {
            if (element is null)
                return;
            if (title != null)
                this.WriteLine(title);
            var summary = this.WriteNodes(element.Nodes(), item);
            var lines = summary.Split('\n');
            var startIndex = 0;
            var firstLine = 0;
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    startIndex = line.Length - line.TrimStart().Length;
                    break;
                }
                ++firstLine;
            }
            summary = string.Join(Environment.NewLine, lines.Skip(firstLine).Select(l => l.StartsWith(" ") ? l.Substring(Math.Min(l.Length, startIndex)) : l));
            while (summary.EndsWith(Environment.NewLine))
                summary = summary.Substring(0, summary.Length - Environment.NewLine.Length);
            this.WriteLine(summary.TrimEnd() + "  ");
        }
        public void WriteChildrenLink<T>(string title)
            where T : DocItem => this.WriteChildrenLink<T>(this.mainItem, title, true);
        public void WriteDirectChildrenLink<T>(string title)
            where T : DocItem => this.WriteChildrenLink<T>(this.mainItem, title, false);
        public void WriteDocItems(IEnumerable<DocItem> docItems, string title)
        {
            var hasTitle = false;
            foreach (var docItem in docItems)
            {
                if (!hasTitle)
                {
                    hasTitle = true;
                    this.WriteLine(title);
                }
                docItem.WriteDocumentation(this);
                this.WriteLine("  ");
            }
        }
        public void WriteDocItems<T>(string title)
            where T : DocItem => this.WriteDocItems(this.KnownItems.OfType<T>().Where(i => i.Parent == this.mainItem), title);
        public void WriteExceptions(DocItem item)
        {
            var hasTitle = false;
            foreach (var exception in item.Documentation.GetExceptions())
            {
                if (!hasTitle)
                {
                    hasTitle = true;
                    this.WriteLine("#### Exceptions");
                }
                var typeName = exception.GetReferenceName();
                this.Write(this.items.TryGetValue(typeName, out var type) ? this.GetLink(type) : typeName.Substring(2).ToDotNetApiLink());
                this.WriteLine("  ");
                this.Write(item, exception);
            }
        }
        public void WriteHeader()
        {
            var home = this.KnownItems.OfType<HomeDocItem>().Single();
            if (home.GeneratePage)
                this.WriteLine($"#### {this.GetLink(home)}");
            var parents = new Stack<DocItem>();
            for (var parent = this.mainItem?.Parent; parent != home && parent != null; parent = parent.Parent)
                parents.Push(parent);
            if (parents.Count > 0)
                this.WriteLine($"### {string.Join(".", parents.Select(p => this.GetLink(p)))}");
        }
        public void WriteLine(string line) => this.builder.AppendLine(line);
        public void WriteLinkTarget(DocItem item) => this.WriteLine($"<a name='{item.GetLink(this.fileNameMode)}'></a>");
        public void WritePageTitle(string name, string title) => this.WriteLine($"## {name} {title}");
        private string GetInnerLink(DocItem item, string displayedName = null)
        {
            var pagedDocItem = item.GetPagedDocItem();
            return $"{(this.mainItem == pagedDocItem ? string.Empty : $"./{pagedDocItem.GetLink(this.fileNameMode)}{DocumentationGenerator.MarkdownExtension}")}#{item.GetLink(this.fileNameMode)}".ToLink(displayedName ?? item.Name, item.FullName);
        }
        private string GetLink(string id) => this.items.TryGetValue(id, out var item) ? this.GetLink(item) : this.links.TryGetValue(id, out var link) ? link.ToLink(id.Substring(2)) : id.Substring(2).ToDotNetApiLink();
        private string GetSeeLink(XElement element)
        {
            var see = element.GetReferenceName();
            if (see != null)
                return this.GetLink(see);
            see = element.GetLangWord();
            if (see != null)
                return (DocumentationWriter.DotNetKeywordBaseLink + see).ToLink(see);
            see = element.GetUrl();
            return see != null ? see.ToLink(element.Value) : string.Empty;
        }
        private bool WriteChildrenLink<T>(DocItem parent, string title, bool includeInnerChildren)
        {
            var hasTitle = title is null;
            foreach (var child in this.KnownItems.Where(i => i.Parent == parent).OrderBy(i => i.Id))
            {
                if (child is T)
                {
                    if (!hasTitle)
                    {
                        hasTitle = true;
                        this.WriteLine($"### {title}");
                    }
                    this.WriteLine($"- {this.GetLink(child)}");
                }
                if (includeInnerChildren)
                    hasTitle = this.WriteChildrenLink<T>(child, hasTitle ? null : title, true);
            }
            return hasTitle;
        }
        private string WriteNodes(IEnumerable<XNode> nodes, DocItem item) => string.Concat
            (
             nodes.Select
                 (
                  node => node switch
                  {
                      XText text => string.Join("  \n", text.Value.Split('\n')),
                      XElement element => element.Name.ToString() switch
                      {
                          "see" => this.GetSeeLink(element),
                          "seealso" => this.GetLink(element.GetReferenceName()),
                          "typeparamref" => item.TryGetTypeParameterDocItem(element.GetName(), out var typeParameter) ? this.GetInnerLink(typeParameter) : element.GetName(),
                          "list" => $"\n\n{this.WriteList(element)}\n",
                          "paramref" => item.TryGetParameterDocItem(element.GetName(), out var parameter) ? this.GetInnerLink(parameter) : element.GetName(),
                          "c" => $"`{element.Value}`",
                          "code" => $"\n```csharp\n{element.Value}\n```\n",
                          "para" => $"\n\n{this.WriteNodes(element.Nodes(), item)}\n\n",
                          _ => element.ToString()
                      },
                      _ => throw new Exception($"unhandled node type in summary {node.NodeType}")
                  }));
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private string WriteList(XElement list)
        {
            var output = new StringBuilder();
            var listHeader = list.Element("listheader");
            switch (list.Attribute("type")?.Value.DehumanizeTo<ListType>() ?? default)
            {
                case ListType.Bullet:
                    return output.ToString();
                case ListType.Number:
                    return output.ToString();
                default:
                    if (listHeader != null)
                    {
                        var columns = listHeader.Elements();
                        output.AppendJoin('|', columns.Select(n => n.Value));
                        output.Append('\n');
                        output.AppendJoin('|', Enumerable.Repeat('-', columns.Count()));
                        output.Append('\n');
                    }
                    foreach (var element in list.Elements("item"))
                    {
                        output.AppendJoin('|', from xElement in element.Elements() select xElement.Value);
                        output.Append('\n');
                    }
                    return output.ToString();
            }
        }
        #endregion
    }
}