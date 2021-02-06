using System.Collections.Generic;

namespace Har.Domain.Components
{
    public class ImagesComponent : IComponent
    {
        public string Name => "images";
        public IEnumerable<Image> Images { get; set; }
    }

    public class Image
    {
        public string ImageUrl { get; set; }
    }
}