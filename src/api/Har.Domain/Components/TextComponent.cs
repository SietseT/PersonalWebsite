namespace Har.Domain.Components
{
    public class TextComponent : IComponent, IHtmlComponent
    {
        public string Type => "text";
        public string ContainerDivClass => string.Empty;
        public string Content { get; set; }
    }
}