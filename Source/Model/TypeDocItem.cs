namespace DotNetToGitHubWiki.Model
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal abstract class TypeDocItem : DocItem, ITypeParameterizedDocItem
    {
        #region Fields
        private static readonly CSharpAmbience BaseTypeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowTypeParameterList };
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowDeclaringType | ConversionFlags.ShowDefinitionKeyword | ConversionFlags.ShowModifiers | ConversionFlags.ShowTypeParameterList | ConversionFlags.ShowTypeParameterVarianceModifier };
        #endregion
        #region Constructors
        protected TypeDocItem(DocItem parent, ITypeDefinition type, XElement documentation)
            : base(parent, type, documentation)
        {
            this.Type = type;
            this.TypeParameters = this.Type.TypeParameters.Select(p => new TypeParameterDocItem(this, p, documentation)).ToArray();
        }
        #endregion
        #region Properties
        public ITypeDefinition Type { get; }
        public TypeParameterDocItem[] TypeParameters { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle(this.Name, this.Type.Kind.ToString());
            writer.Write(this, this.Documentation.GetSummary());
            var interfaces = this.Type.DirectBaseTypes.Where(t => t.Kind == TypeKind.Interface && t.GetDefinition().Accessibility == Accessibility.Public).ToList();
            writer.WriteLine("```csharp");
            writer.Write(TypeDocItem.CodeAmbience.ConvertSymbol(this.Type));
            var baseType = this.Type.DirectBaseTypes.FirstOrDefault(t => t.Kind == TypeKind.Class && !t.IsKnownType(KnownTypeCode.Object) && !t.IsKnownType(KnownTypeCode.ValueType));
            if (baseType != null)
            {
                writer.Write(" : ");
                writer.Write(TypeDocItem.BaseTypeAmbience.ConvertType(baseType));
            }
            foreach (var @interface in interfaces)
            {
                writer.WriteLine(baseType is null ? " :" : ",");
                baseType = this.Type;
                writer.Write(TypeDocItem.BaseTypeAmbience.ConvertType(@interface));
            }
            writer.Break();
            writer.WriteLine("```");
            var needBreak = false;
            if (this.Type.Kind == TypeKind.Class)
            {
                writer.Write("Inheritance ");
                writer.Write(string.Join(" &#129106; ", this.Type.GetNonInterfaceBaseTypes().Where(t => !t.Equals(this.Type)).Select(writer.GetTypeLink)));
                writer.Write(" &#129106; ");
                writer.Write(this.Name);
                writer.WriteLine("  ");
                needBreak = true;
            }
            var derived = writer.KnownItems.OfType<TypeDocItem>().Where(i => i.Type.DirectBaseTypes.Select(t => t is ParameterizedType g ? g.GetDefinition() : t).Contains(this.Type)).OrderBy(i => i.FullName).ToList();
            if (derived.Count > 0)
            {
                if (needBreak)
                    writer.Break();
                writer.Write("Derived  " + Environment.NewLine + "&#8627; ");
                writer.Write(string.Join("  " + Environment.NewLine + "&#8627; ", derived.Select(t => writer.GetLink(t))));
                writer.WriteLine("  ");
                needBreak = true;
            }

            // attribute
            if (interfaces.Count > 0)
            {
                if (needBreak)
                    writer.Break();
                writer.Write("Implements ");
                writer.Write(string.Join(", ", interfaces.Select(writer.GetTypeLink)));
                writer.WriteLine("  ");
            }
            writer.WriteDocItems(this.TypeParameters, "#### Type parameters");
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
            writer.WriteDirectChildrenLink<ConstructorDocItem>("Constructors");
            writer.WriteDirectChildrenLink<FieldDocItem>("Fields");
            writer.WriteDirectChildrenLink<PropertyDocItem>("Properties");
            writer.WriteDirectChildrenLink<MethodDocItem>("Methods");
            writer.WriteDirectChildrenLink<EventDocItem>("Events");
            writer.WriteDirectChildrenLink<OperatorDocItem>("Operators");
            if (writer.NestedTypeVisibility == NestedTypeVisibility.DeclaringType || writer.NestedTypeVisibility == NestedTypeVisibility.Everywhere)
            {
                writer.WriteDirectChildrenLink<ClassDocItem>("Classes");
                writer.WriteDirectChildrenLink<StructDocItem>("Structs");
                writer.WriteDirectChildrenLink<InterfaceDocItem>("Interfaces");
                writer.WriteDirectChildrenLink<EnumDocItem>("Enums");
                writer.WriteDirectChildrenLink<DelegateDocItem>("Delegates");
            }
        }
        #endregion
    }
}