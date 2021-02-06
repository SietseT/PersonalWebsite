using System;
using System.Collections.Generic;
using System.Linq;
using Har.Infrastructure.Data.KontentTypes;
using Kentico.Kontent.Delivery.Abstractions;

namespace Har.Infrastructure.Data.Kontent.Types
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> Codenames = new Dictionary<Type, string>
        {
            {typeof(KontentTypes.Blog), "blog"},
            {typeof(ImageSlider), "image_slider"},
            {typeof(Domain.Models.Project), "project"}
        };

        public Type GetType(string contentType)
        {
            return Codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return Codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}