using System.Linq;
using Har.Application.Components;
using Har.Domain.Components;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Har.Application.Tests.Components
{
    public class HtmlComponentParserTests
    {
        private readonly NullLogger<HtmlComponentParser> _mockLogger;

        public HtmlComponentParserTests()
        {
            _mockLogger = new NullLogger<HtmlComponentParser>();
        }
        
        [Fact]
        public void HtmlString_ReturnsSingleTextcomponent()
        {
            var htmlString = GetHtmlStringWithOneTextComponent();
            var componentParser = new HtmlComponentParser(_mockLogger);

            var components = componentParser.Parse(htmlString).ToArray();
            
            Assert.Single(components);
            Assert.Collection(components, component => Assert.IsType<TextComponent>(component));
            Assert.Contains("Amazon Polly", ((TextComponent) components.FirstOrDefault())?.Content ?? string.Empty);
        }
        
        [Fact]
        public void HtmlString_ReturnsSingleImageComponent()
        {
            var htmlString = GetHtmlStringWithOneImageComponent();
            var componentParser = new HtmlComponentParser(_mockLogger);

            var components = componentParser.Parse(htmlString).ToArray();
            
            Assert.Single(components);
            Assert.Collection(components, component => Assert.IsType<ImagesComponent>(component));
        }
        
        [Fact]
        public void HtmlString_ReturnsSingleImageComponentWithTwoImages()
        {
            var htmlString = GetHtmlStringWithOneImageComponent();
            var componentParser = new HtmlComponentParser(_mockLogger);

            var components = componentParser.Parse(htmlString).ToArray();
            var imageComponent = components.FirstOrDefault() as ImagesComponent;
            
            Assert.NotNull(imageComponent);
            Assert.Equal(2, imageComponent.Images.Count());
            Assert.Collection(imageComponent.Images, image =>
            {
                Assert.Equal("Elundus Core desktop view", image.Alt);
                Assert.Equal("https://assets-eu-01.kc-usercontent.com:443/7f506e42-9290-01aa-7e18-b07c3b2d973c/0ee58a9c-48c5-433f-aa89-671d665a6ce3/msedge_2DBLUL4Hgp.jpg", image.Src);
            }, image => {
                Assert.Equal("Elundus Core mobile view", image.Alt);
                Assert.Equal("https://assets-eu-01.kc-usercontent.com:443/7f506e42-9290-01aa-7e18-b07c3b2d973c/e4a8b4e5-86c4-4b56-9557-bd4aa1fc53f1/msedge_4Ig7LdiDyX.jpg", image.Src);
            });
        }

        private static string GetHtmlStringWithOneTextComponent()
        {
            return "<p>Text before</p>\n<p><br></p>\n<p><br></p>\n<h1>Elundus Core</h1>\n<p>Elundus Core is a website where you can convert text-to-speech using <a href=\"https://aws.amazon.com/polly/\" data-new-window=\"true\" target=\"_blank\" rel=\"noopener noreferrer\">Amazon Polly</a>. I've started building this tool when I used to watch <a href=\"https://www.twitch.tv/\" data-new-window=\"true\" target=\"_blank\" rel=\"noopener noreferrer\">Twitch</a> streams regularly.&nbsp;</p>\n<p><br></p>";
        }
        
        private static string GetHtmlStringWithOneImageComponent()
        {
            return "<div class=\"image-slider\">\n<img src=\"https://assets-eu-01.kc-usercontent.com:443/7f506e42-9290-01aa-7e18-b07c3b2d973c/0ee58a9c-48c5-433f-aa89-671d665a6ce3/msedge_2DBLUL4Hgp.jpg\" alt=\"Elundus Core desktop view\">\n<img src=\"https://assets-eu-01.kc-usercontent.com:443/7f506e42-9290-01aa-7e18-b07c3b2d973c/e4a8b4e5-86c4-4b56-9557-bd4aa1fc53f1/msedge_4Ig7LdiDyX.jpg\" alt=\"Elundus Core mobile view\">\n</div>";
        }
    }
}