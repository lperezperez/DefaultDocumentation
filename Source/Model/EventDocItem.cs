namespace DotNetToGitHubWiki.Model
{
    using System.Xml.Linq;
    using DotNetToGitHubWiki.Helper;
    using ICSharpCode.Decompiler.CSharp.OutputVisitor;
    using ICSharpCode.Decompiler.Output;
    using ICSharpCode.Decompiler.TypeSystem;
    internal sealed class EventDocItem : DocItem
    {
        #region Fields
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience { ConversionFlags = ConversionFlags.ShowAccessibility | ConversionFlags.ShowBody | ConversionFlags.ShowDefinitionKeyword | ConversionFlags.ShowModifiers };
        #endregion
        #region Constructors
        public EventDocItem(TypeDocItem parent, IEvent @event, XElement documentation)
            : base(parent, @event, documentation) => this.Event = @event;
        #endregion
        #region Properties
        public IEvent Event { get; }
        #endregion
        #region Methods
        public override void WriteDocumentation(DocumentationWriter writer)
        {
            writer.WriteHeader();
            writer.WritePageTitle($"{this.Parent.Name}.{this.Name}", "Event");
            writer.Write(this, this.Documentation.GetSummary());
            writer.WriteLine("```csharp");
            writer.WriteLine(EventDocItem.CodeAmbience.ConvertSymbol(this.Event));
            writer.WriteLine("```");
            // attributes
            writer.WriteLine("#### Event type");
            writer.WriteLine(writer.GetTypeLink(this.Event.ReturnType));
            writer.Write("### Example", this.Documentation.GetExample(), this);
            writer.Write("### Remarks", this.Documentation.GetRemarks(), this);
        }
        #endregion
    }
}