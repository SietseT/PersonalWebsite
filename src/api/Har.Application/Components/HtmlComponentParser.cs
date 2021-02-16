using System;
using System.Collections.Generic;
using System.Linq;
using Har.Application.Factories.Components;
using Har.Domain.Components;
using Har.Domain.Factories;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace Har.Application.Components
{
    public class HtmlComponentParser : IComponentParser<string>
    {
        private readonly ILogger<HtmlComponentParser> _logger;
        private static IHtmlComponent[] _htmlComponents;

        public HtmlComponentParser(ILogger<HtmlComponentParser> logger)
        {
            _logger = logger;
        }

        public IEnumerable<IComponent> Parse(string value)
        {
            _htmlComponents ??= ReflectionFactory.GetHtmlComponentInstances();

            var components = new List<IComponent>();
            
            var componentFound = true;
            while (!string.IsNullOrEmpty(value) || !componentFound)
            {
                var nextComponent = GetNextComponent(ref value);
                componentFound = nextComponent != null;
                
                if(componentFound)
                    components.Add(nextComponent);
            }

            return components;
        }

        private IComponent GetNextComponent(ref string htmlString)
        {
            var nextComponentIndex = -1;
            IHtmlComponent nextHtmlComponent = null;
            
            foreach (var htmlComponent in _htmlComponents)
            {
                var componentContainerDiv = $"<div class\"{htmlComponent.ContainerDivClass}\">";
                var containerDivIndex = htmlString.IndexOf(componentContainerDiv, StringComparison.OrdinalIgnoreCase);

                if (nextComponentIndex == -1 || containerDivIndex < nextComponentIndex)
                {
                    nextComponentIndex = containerDivIndex;
                    nextHtmlComponent = htmlComponent;
                }
            }

            if (nextComponentIndex == -1)
                return new TextComponent
                {
                    Content = htmlString
                };

            IComponent component;

            if (nextComponentIndex > 0)
            {
                var htmlUntilNextComponent = htmlString.Substring(0, nextComponentIndex);
                component = new TextComponent
                {
                    Content = htmlUntilNextComponent
                };
            }
            else
                component = GetHtmlComponent(htmlString, nextHtmlComponent);


            //Use substring, so this component is not processed again
            htmlString = htmlString.Substring(0, nextComponentIndex);
            
            return component;
        }

        private IComponent GetHtmlComponent(string htmlString, IHtmlComponent htmlComponent)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var componentNode = htmlDocument.DocumentNode.SelectSingleNode($"//div[@class=\"{htmlComponent.ContainerDivClass}\"[0]");
            if (componentNode == null)
            {
                LogComponentParserWarning(htmlString, htmlComponent);
                return default;
            }

            return HtmlComponentFactory.CreateComponent(componentNode, htmlComponent.GetType());
        }

        private void LogComponentParserWarning(string htmlString, IHtmlComponent htmlComponent)
        {
            _logger.LogWarning("'{HtmlString}' does not contain valid HTML for {HtmlComponent}", htmlString, htmlComponent.GetType().Name);
        }
    }
}