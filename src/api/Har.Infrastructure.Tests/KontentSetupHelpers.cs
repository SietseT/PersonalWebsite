using System;
using Har.Infrastructure.Data.Kontent.Types;
using Kentico.Kontent.Delivery.Abstractions;
using Moq;

namespace Har.Infrastructure.Tests
{
    public static class KontentSetupHelpers
    {
        public const string Name = "Elundus Core";
        public const string Author = "Sietse Trommelen";
        public const string Content = "This is some content";
        public const string ShortDescription = "This is a short description";
        public const string SiteUrl = "https://elunduscore.com";
        
        public static Project GetKontentProject(string codeName, DateTime? onlineSince = null, string technologies = "")
        {
            var customTypeProvider = new CustomTypeProvider();

            var contentTypeSystemAttributesMock = new Mock<IContentItemSystemAttributes>();
            contentTypeSystemAttributesMock
                .SetupGet(c => c.Codename)
                .Returns(customTypeProvider.GetCodename(typeof(Project)));

            contentTypeSystemAttributesMock
                .SetupGet(c => c.Name)
                .Returns(Name);
            
            contentTypeSystemAttributesMock
                .SetupGet(c => c.Codename)
                .Returns(codeName);

            return new Project
            {
                System = contentTypeSystemAttributesMock.Object,
                Author = Author,
                Content = Content,
                OnlineSince = onlineSince,
                ShortDescription = ShortDescription,
                SiteUrl = SiteUrl,
                Technologies = technologies
            };
        }
    }
}