using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using Har.Application.Factories.Components;
using Har.Domain.Components;
using Har.Domain.Factories;
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
            var (nextComponentIndex, nextHtmlComponent) = GetNextComponentIndex(htmlString);

            switch (nextComponentIndex)
            {
                case -1:
                {
                    //No components are found, it's all a TextComponent
                    var textComponent = new TextComponent
                    {
                        Content = htmlString
                    };

                    htmlString = string.Empty;

                    return textComponent;
                }
                case > 0:
                {
                    //Component is found, but until that index we handle it as a textcomponent
                    var htmlUntilNextComponent = htmlString.Substring(0, nextComponentIndex);
                    
                    htmlString = htmlString.Substring(nextComponentIndex);
                
                    return new TextComponent
                    {
                        Content = htmlUntilNextComponent
                    };
                }
            }

            //Index is 0, so the next HTML will be a component
            var (component, componentHtml) = GetHtmlComponent(htmlString, nextHtmlComponent);
            htmlString = htmlString.Replace(componentHtml, string.Empty);

            return component;
        }

        private static (int NextComponentIndex, IHtmlComponent NextHtmlComponent) GetNextComponentIndex(string htmlString)
        {
            var nextComponentIndex = -1;
            IHtmlComponent nextHtmlComponent = null;
            
            foreach (var htmlComponent in _htmlComponents.Where(c => !string.IsNullOrWhiteSpace(c.ContainerDivClass)))
            {
                var componentContainerDiv = $"<div class=\"{htmlComponent.ContainerDivClass}\">";
                var containerDivIndex = htmlString.IndexOf(componentContainerDiv, StringComparison.OrdinalIgnoreCase);

                if (nextComponentIndex == -1 && containerDivIndex != -1 || containerDivIndex < nextComponentIndex)
                {
                    nextComponentIndex = containerDivIndex;
                    nextHtmlComponent = htmlComponent;
                }
            }
            
            return (nextComponentIndex, nextHtmlComponent);
        }

        private (IComponent Component, string ComponentHtml) GetHtmlComponent(string htmlString, IHtmlComponent htmlComponent)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var htmlDocument = context.OpenAsync(req => req.Content(htmlString)).Result;

            var componentNode = htmlDocument.QuerySelector($"div.{htmlComponent.ContainerDivClass}");
            if (componentNode == null)
            {
                LogComponentParserWarning(htmlString, htmlComponent);
                return default;
            }

            return (HtmlComponentFactory.CreateComponent(componentNode, htmlComponent.GetType()), componentNode.OuterHtml);
        }

        private void LogComponentParserWarning(string htmlString, IHtmlComponent htmlComponent)
        {
            _logger.LogWarning("'{HtmlString}' does not contain valid HTML for {HtmlComponent}", htmlString, htmlComponent.GetType().Name);
        }
    }
}