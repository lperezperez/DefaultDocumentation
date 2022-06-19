﻿namespace DotNetToGitHubWiki.Model
{
    using System.Linq;
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class OperatorDocItem : DocItem, IParameterizedDocItem
    {
        #region Fields
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowBody | ConversionFlags.ShowModifiers | ConversionFlags.ShowParameterDefaultValues | ConversionFlags.ShowParameterList | ConversionFlags.ShowParameterModifiers | ConversionFlags.ShowParameterNames | ConversionFlags.ShowReturnType | ConversionFlags.ShowTypeParameterList | ConversionFlags.ShowTypeParameterVarianceModifier | ConversionFlags.UseFullyQualifiedTypeNames };
        #endregion
        #region Constructors
        public OperatorDocItem(TypeDocItem parent, IMethod method, XElement documentation)
            : base(parent, method, documentation)
        {
            this.Method = method;
            this.Parameters = method.Parameters.Select(p => new ParameterDocItem(this, p, documentation)).ToArray();
        }
        #endregion
        #region Properties
        public IMethod Method { get; }
        public ParameterDocItem[] Parameters { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle($"{this.Parent.Name}.{this.Name}", "Operator");
            writer.Write(this, this.Documentation.GetSummary());
            writer.WriteLine("```csharp");
            writer.WriteLine(OperatorDocItem.CodeAmbience.ConvertSymbol(this.Method));
            writer.WriteLine("```");

            // attributes
            writer.WriteDocItems(this.Parameters, "#### Parameters");
            if (this.Method.ReturnType.Kind != TypeKind.Void)
            {
                writer.WriteLine("#### Returns");
                writer.WriteLine(writer.GetTypeLink(this.Method.ReturnType) + "  ");
                writer.Write(this, this.Documentation.GetReturns());
            }
            writer.WriteExceptions(this);
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}