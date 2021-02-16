using System;
using Har.Domain.Components;
using Har.Domain.Factories;
using HtmlAgilityPack;

namespace Har.Application.Factories.Components
{
    public static class HtmlComponentFactory
    {
        private static IHtmlComponentFactory[] _htmlComponentFactories;
        
        public static IComponent CreateComponent(HtmlNode htmlNode, Type htmlComponentType)
        {
            _htmlComponentFactories ??= ReflectionFactory.GetComponentInstances();

            foreach (var htmlComponentFactory in _htmlComponentFactories)
            {
                if (htmlComponentFactory.Accepts(htmlComponentType))
                    return htmlComponentFactory.Create(htmlNode);
            }

            throw new Exception($"No IHtmlComponentFactory implemented for type {htmlComponentType}");
        }
    }
}