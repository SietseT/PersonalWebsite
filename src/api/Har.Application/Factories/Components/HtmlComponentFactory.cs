using System;
using AngleSharp.Dom;
using Har.Domain.Components;
using Har.Domain.Factories;

namespace Har.Application.Factories.Components
{
    public static class HtmlComponentFactory
    {
        private static IHtmlComponentFactory[] _htmlComponentFactories;
        
        public static IComponent CreateComponent(IElement htmlElement, Type htmlComponentType)
        {
            _htmlComponentFactories ??= ReflectionFactory.GetComponentInstances();

            foreach (var htmlComponentFactory in _htmlComponentFactories)
            {
                if (htmlComponentFactory.Accepts(htmlComponentType))
                    return htmlComponentFactory.Create(htmlElement);
            }

            throw new Exception($"No IHtmlComponentFactory implemented for type {htmlComponentType}");
        }
    }
}