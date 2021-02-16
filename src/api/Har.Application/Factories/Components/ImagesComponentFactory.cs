using System;
using System.Linq;
using Har.Domain.Components;
using HtmlAgilityPack;

namespace Har.Application.Factories.Components
{
    public class ImagesComponentFactory : IHtmlComponentFactory
    {
        public IComponent Create(HtmlNode htmlNode)
        {
            var imageComponent = new ImagesComponent();

            var imageNodes = htmlNode.SelectNodes("//img").ToArray();

            imageComponent.Images = imageNodes.Select(imageNode =>
            {
                var src = imageNode.GetAttributeValue("src", string.Empty);
                var alt = imageNode.GetAttributeValue("alt", string.Empty);
                return new Image(src, alt);
            });

            return imageComponent;
        }

        public bool Accepts(Type type)
        {
            return type == typeof(ImagesComponent);
        }
    }
}