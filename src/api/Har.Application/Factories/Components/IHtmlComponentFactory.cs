using System;
using AngleSharp.Dom;
using Har.Domain.Components;

namespace Har.Application.Factories.Components
{
    public interface IHtmlComponentFactory
    {
        IComponent Create(IElement htmlElement);
        bool Accepts(Type type);
    }
}