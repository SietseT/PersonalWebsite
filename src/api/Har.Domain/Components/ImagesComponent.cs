using System.Collections.Generic;

namespace Har.Domain.Components
{
    public class ImagesComponent : IComponent, IHtmlComponent
    {
        public string Type => "images";
        public string ContainerDivClass => "image-slider";
        public IEnumerable<Image> Images { get; set; }
    }

    public class Image
    {
        public Image(string src, string alt)
        {
            Src = src;
            Alt = alt;
        }
        
        public string Src { get; set; }
        public string Alt { get; set; }
    }
}