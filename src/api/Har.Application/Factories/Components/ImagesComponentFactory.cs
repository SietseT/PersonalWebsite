using System;
using System.Linq;
using AngleSharp.Dom;
using Har.Domain.Components;

namespace Har.Application.Factories.Components
{
    public class ImagesComponentFactory : IHtmlComponentFactory
    {
        public IComponent Create(IElement htmlElement)
        {
            var imageComponent = new ImagesComponent();

            var imageNodes = htmlElement.QuerySelectorAll("img").ToArray();

            imageComponent.Images = imageNodes.Select(imageNode =>
            {
                var src = imageNode.GetAttribute("src");
                var alt = imageNode.GetAttribute("alt");
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