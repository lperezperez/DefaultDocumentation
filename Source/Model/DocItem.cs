namespace DotNetToGitHubWiki.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Documentation;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal abstract class DocItem
    {
        #region Fields
        private static readonly CSharpAmbience EntityNameAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowParameterList | ConversionFlags.ShowTypeParameterList | ConversionFlags.UseFullyQualifiedTypeNames };
        private static readonly CSharpAmbience FullNameAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowParameterList | ConversionFlags.ShowTypeParameterList | ConversionFlags.UseFullyQualifiedTypeNames | ConversionFlags.ShowDeclaringType | ConversionFlags.UseFullyQualifiedEntityNames };
        private static readonly CSharpAmbience NameAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowParameterList | ConversionFlags.ShowTypeParameterList };
        private static readonly CSharpAmbience TypeNameAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowParameterList | ConversionFlags.ShowTypeParameterList | ConversionFlags.ShowDeclaringType | ConversionFlags.UseFullyQualifiedTypeNames };
        private readonly IEntity entity;
        #endregion
        #region Constructors
        protected DocItem(DocItem parent, string id, string fullName, string name, XElement documentation)
        {
            this.Parent = parent;
            this.Id = id;
            this.Documentation = documentation;
            this.FullName = fullName.Replace("<", "&lt;").Replace(">", "&gt;").Replace("this ", string.Empty);
            this.Name = name.Replace("<", "&lt;").Replace(">", "&gt;").Replace("this ", string.Empty);
        }
        protected DocItem(DocItem parent, IEntity entity, XElement documentation)
            : this(parent, entity.GetIdString(), DocItem.GetName(entity, DocItem.FullNameAmbience), (entity is ITypeDefinition ? DocItem.TypeNameAmbience : DocItem.EntityNameAmbience).ConvertSymbol(entity), documentation) => this.entity = entity;
        #endregion
        #region Properties
        public XElement Documentation { get; }
        public string FullName { get; }
        public virtual bool GeneratePage => true;
        public string Id { get; }
        public string Name { get; }
        public DocItem Parent { get; }
        #endregion
        #region Methods
        private static string GetName(IEntity entity, CSharpAmbience ambience)
        {
            var fullName = ambience.ConvertSymbol(entity);
            if (entity.SymbolKind == SymbolKind.Operator)
            {
                var offset = 17;
                var index = fullName.IndexOf("implicit operator ", StringComparison.Ordinal);
                if (index < 0)
                {
                    index = fullName.IndexOf("explicit operator ", StringComparison.Ordinal);
                    if (index < 0)
                    {
                        index = fullName.IndexOf("operator ", StringComparison.Ordinal);
                        offset = fullName.IndexOf('(') - index;
                    }
                }
                if (index >= 0)
                    fullName = fullName.Substring(0, index) + entity.Name + fullName.Substring(index + offset);
            }
            return fullName;
        }
        public abstract void WriteDocumentation(DocumentationWriter writer);
        public virtual string GetLink(FileNameMode fileNameMode) => (fileNameMode switch
                                                                        {
                                                                            FileNameMode.Md5 => Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(this.FullName))),
                                                                            FileNameMode.Name => this.entity is null ? this.FullName : string.Join(".", this.GetHierarchy().Reverse()),
                                                                            _ => this.FullName
                                                                        }).Clean();
        private IEnumerable<string> GetHierarchy()
        {
            yield return DocItem.GetName(this.entity, DocItem.NameAmbience);
            var parent = this.Parent;
            while (parent is TypeDocItem)
            {
                yield return DocItem.GetName(parent.entity, DocItem.NameAmbience);
                parent = parent.Parent;
            }
        }
        #endregion
    }
}