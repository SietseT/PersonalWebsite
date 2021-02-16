using System;
using Har.Domain.Components;
using HtmlAgilityPack;

namespace Har.Application.Factories.Components
{
    public interface IHtmlComponentFactory
    {
        IComponent Create(HtmlNode htmlNode);
        bool Accepts(Type type);
    }
}