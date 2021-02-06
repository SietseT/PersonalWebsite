namespace Har.Domain.Components
{
    public class TextComponent : IComponent
    {
        public string Type => "text";
        public string Content { get; set; }
    }
}