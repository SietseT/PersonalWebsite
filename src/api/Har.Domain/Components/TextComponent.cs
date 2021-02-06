namespace Har.Domain.Components
{
    public class TextComponent : IComponent
    {
        public string Name => "text";
        public string Content { get; set; }
    }
}