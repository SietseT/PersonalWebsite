using Har.Domain.Components;
using Har.Infrastructure.Data.Kontent.Resolvers;
using Har.Infrastructure.Data.Kontent.Types;
using Kentico.Kontent.Delivery.Abstractions;
using Moq;
using Xunit;

namespace Har.Infrastructure.Tests.Kontent.Resolvers
{
    public class ImageSliderResolverTests
    {
        [Fact]
        public void ImageSliderWithTwoImages_ReturnsCorrectHtml()
        {
            // Arrange
            var imageResolver = new ImageSliderResolver();
            var imageSlider = new ImageSlider()
            {
                Images = new[]
                {
                    GetAsset("https://elunduscore.com/image1.jpg", "Description 1"),
                    GetAsset("https://elunduscore.com/image2.jpg", "Description 2"),
                }
            };

            // Act
            var resolvedHtml = imageResolver.Resolve(imageSlider);

            // Assert
            Assert.Contains($"<div class=\"{new ImagesComponent().ContainerDivClass}\">", resolvedHtml);
            Assert.Contains("https://elunduscore.com/image1.jpg", resolvedHtml);
            Assert.Contains("https://elunduscore.com/image2.jpg", resolvedHtml);
            Assert.Contains("Description 1", resolvedHtml);
            Assert.Contains("Description 2", resolvedHtml);
            Assert.Contains("<img", resolvedHtml);
        }
        
        [Fact]
        public void ImageSliderWithoutImages_ReturnsEmptyString()
        {
            // Arrange
            var imageResolver = new ImageSliderResolver();
            var imageSlider = new ImageSlider();

            // Act
            var resolvedHtml = imageResolver.Resolve(imageSlider);

            // Assert
            Assert.Equal(string.Empty, resolvedHtml);
        }

        private static IAsset GetAsset(string url, string description)
        {
            var imageMock = new Mock<IAsset>();
            imageMock.SetupGet(mock => mock.Url).Returns(url);
            imageMock.SetupGet(mock => mock.Description).Returns(description);
            
            return imageMock.Object;
        }
    }
}