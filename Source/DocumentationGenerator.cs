namespace DotNetToGitHubWiki
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Model;
    using DotNetToGitHubWiki.Properties;
    using Fclp.Internals.Extensions;
    using Humanizer;
    using ICSharpCode.Decompiler;
    using ICSharpCode.Decompiler.CSharp;
    using ICSharpCode.Decompiler.Documentation;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class DocumentationGenerator
    {
        #region Fields
        /// <summary>The <see href="https://daringfireball.net/projects/markdown">Markdown</see> file extension.</summary>
        public static string MarkdownExtension = ".md";
        private readonly CSharpDecompiler decompiler;
        private readonly Dictionary<string, DocItem> docItems;
        private readonly XmlDocumentationProvider documentationProvider;
        private readonly FileNameMode fileNameMode;
        private readonly Dictionary<string, string> links;
        private readonly NestedTypeVisibility nestedTypeVisibility;
        #endregion
        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="DocumentationGenerator"/> <see langword="class"/>.</summary>
        /// <param name="assemblyFilePath">The assembly file path to parse.</param>
        /// <param name="documentationFilePath">The path of the file that was generated as the XML documentation file of the <paramref name="assemblyFilePath"/>.</param>
        /// <param name="homePageName">The name of the home page.</param>
        /// <param name="fileNameMode">The <see cref="FileNameMode"/> type.</param>
        /// <param name="nestedTypeVisibility">The <see cref="NestedTypeVisibility"/>.</param>
        /// <param name="linksFiles">A collection of files with links to other types.</param>
        public DocumentationGenerator(string assemblyFilePath, string documentationFilePath, string homePageName, FileNameMode fileNameMode, NestedTypeVisibility nestedTypeVisibility, IEnumerable<string> linksFiles)
        {
            this.decompiler = new CSharpDecompiler(assemblyFilePath, new DecompilerSettings { ThrowOnAssemblyResolveErrors = false });
            this.documentationProvider = new XmlDocumentationProvider(documentationFilePath);
            this.fileNameMode = fileNameMode;
            this.nestedTypeVisibility = nestedTypeVisibility;
            this.docItems = new Dictionary<string, DocItem>();
            foreach (var item in this.GetDocItems(homePageName))
                this.docItems.Add(item.Id, item);
            this.links = new Dictionary<string, string>();
            foreach (var (id, link) in DocumentationGenerator.GetExternalLinks(linksFiles))
                this.links[id] = link;
        }
        #endregion
        #region Methods
        private static IEnumerable<(string, string)> GetExternalLinks(IEnumerable<string> linksFiles)
        {
            foreach (var linksFile in linksFiles.Select(l => l.Trim()).Where(l => File.Exists(l)))
            {
                using var reader = File.OpenText(linksFile);
                var baseLink = string.Empty;
                while (!reader.EndOfStream)
                {
                    var items = reader.ReadLine()?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (items?.Length)
                    {
                        case 0:
                            baseLink = string.Empty;
                            break;
                        case 1:
                            baseLink = items[0];
                            if (!baseLink.EndsWith("/"))
                                baseLink += "/";
                            break;
                        case 2:
                            yield return (items[0], baseLink + items[1]);
                            break;
                    }
                }
            }
        }
        /// <summary>Writes the documentation in the specified <paramref name="path"/>.</summary>
        /// <param name="path">The output folder path.</param>
        public void WriteDocumentation(string path) =>
#if DEBUG
            this.docItems.Values.Where(docItem => docItem.GeneratePage).ForEach
#else
            this.docItems.Values.Where(docItem => docItem.GeneratePage).AsParallel().ForAll
#endif
                (
                 docItem =>
                 {
                     try
                     {
                         using var writer = new DocumentationWriter(this.fileNameMode, this.nestedTypeVisibility, this.docItems, this.links, path, docItem);
                         docItem.WriteDocumentation(writer);
                     }
                     catch (Exception exception)
                     {
                         throw new Exception(Resources.ErrorWrittingDoc.FormatWith(docItem.FullName), exception);
                     }
                 });
        /// <summary>Writes the links specified in <see cref="linksFilePath"/></summary>
        /// <param name="baseLinkPath">The path of the base links.</param>
        /// <param name="linksFilePath">The path to store the links.</param>
        public void WriteLinks(string baseLinkPath, string linksFilePath)
        {
            using var writer = File.CreateText(linksFilePath);
            if (!string.IsNullOrEmpty(baseLinkPath))
                writer.WriteLine(baseLinkPath);
            foreach (var item in this.docItems.Values)
                switch (item)
                {
                    case HomeDocItem _:
                        break;
                    case EnumFieldDocItem _:
                        writer.WriteLine($"{item.Id} {item.Parent.GetLink(this.fileNameMode)}{DocumentationGenerator.MarkdownExtension}#{item.GetLink(this.fileNameMode)}");
                        break;
                    default:
                        writer.WriteLine($"{item.Id} {item.GetLink(this.fileNameMode)}{DocumentationGenerator.MarkdownExtension}");
                        break;
                }
        }
        private IEnumerable<DocItem> GetDocItems(string homePageName)
        {
            static XElement ConvertToDocumentation(string documentationString) => documentationString is null ? null : XElement.Parse($"<doc>{documentationString}</doc>");
            bool TryGetDocumentation(IEntity entity, out XElement documentation)
            {
                documentation = ConvertToDocumentation(this.documentationProvider.GetDocumentation(entity));
                return documentation != null;
            }
            var homeDocItem = new HomeDocItem(homePageName, this.decompiler.TypeSystem.MainModule.AssemblyName, ConvertToDocumentation(this.documentationProvider.GetDocumentation($"T:{this.decompiler.TypeSystem.MainModule.AssemblyName}.AssemblyDoc")));
            yield return homeDocItem;
            foreach (var type in this.decompiler.TypeSystem.MainModule.TypeDefinitions.Where(t => t.Name != "NamespaceDoc" && t.Name != "AssemblyDoc"))
            {
                var showType = TryGetDocumentation(type, out var documentation);
                var newNamespace = false;
                var namespaceId = $"N:{type.Namespace}";
                if (!this.docItems.TryGetValue(type.DeclaringType?.GetDefinition().GetIdString() ?? namespaceId, out var parentDocItem))
                {
                    newNamespace = true;
                    parentDocItem = new NamespaceDocItem(homeDocItem, type.Namespace, ConvertToDocumentation(this.documentationProvider.GetDocumentation(namespaceId) ?? this.documentationProvider.GetDocumentation($"T:{type.Namespace}.NamespaceDoc")));
                }
                TypeDocItem typeDocItem = type.Kind switch
                {
                    TypeKind.Class => new ClassDocItem(parentDocItem, type, documentation),
                    TypeKind.Struct => new StructDocItem(parentDocItem, type, documentation),
                    TypeKind.Interface => new InterfaceDocItem(parentDocItem, type, documentation),
                    TypeKind.Enum => new EnumDocItem(parentDocItem, type, documentation),
                    TypeKind.Delegate => new DelegateDocItem(parentDocItem, type, documentation),
                    _ => throw new NotSupportedException()
                };
                foreach (var entity in Enumerable.Empty<IEntity>().Concat(type.Fields).Concat(type.Properties).Concat(type.Methods).Concat(type.Events).Where(entity => TryGetDocumentation(entity, out documentation)))
                {
                    showType = true;
                    yield return entity switch
                    {
                        IField field when typeDocItem is EnumDocItem enumDocItem => new EnumFieldDocItem(enumDocItem, field, documentation),
                        IField field => new FieldDocItem(typeDocItem, field, documentation),
                        IProperty property => new PropertyDocItem(typeDocItem, property, documentation),
                        IMethod { IsConstructor: true } method => new ConstructorDocItem(typeDocItem, method, documentation),
                        IMethod { IsOperator: true } method => new OperatorDocItem(typeDocItem, method, documentation),
                        IMethod method => new MethodDocItem(typeDocItem, method, documentation),
                        IEvent @event => new EventDocItem(typeDocItem, @event, documentation),
                        _ => throw new NotSupportedException()
                    };
                }
                if (!showType)
                    continue;
                if (newNamespace)
                    yield return parentDocItem;
                yield return typeDocItem;
            }
            homeDocItem.HasMultipleNamespaces = this.docItems.Values.OfType<NamespaceDocItem>().Count() > 1;
        }
        #endregion
    }
}