using System;
using System.Linq;
using System.Text;
using Har.Domain.Components;
using Har.Infrastructure.Data.Kontent.Types;
using Kentico.Kontent.Delivery.Abstractions;

namespace Har.Infrastructure.Data.Kontent.Resolvers
{
    public class ImageSliderResolver : IInlineContentItemsResolver<ImageSlider>
    {
        public string Resolve(ImageSlider data)
        {
            if (data?.Images == null || !data.Images.Any())
                return string.Empty;

            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append($"<div class=\"{new ImagesComponent().ContainerDivClass}\">");

            foreach (var image in data.Images)
            {
                var imageHtml = $"<img src=\"{image.Url}\" alt=\"{image.Description}\" />";
                htmlBuilder.Append(imageHtml);
            }

            htmlBuilder.Append("</div>");
            return htmlBuilder.ToString();
        }
    }
}