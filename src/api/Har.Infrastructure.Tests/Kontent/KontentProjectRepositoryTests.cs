using System;
using System.Threading.Tasks;
using Har.Infrastructure.Data.Kontent.Mappers;
using Har.Infrastructure.Data.Kontent.Types;
using Kentico.Kontent.Delivery.Abstractions;
using Moq;
using Xunit;

namespace Har.Infrastructure.Tests.Kontent
{
    public class KontentProjectMapperTests
    {
        private const string Name = "Elundus Core";
        private const string Author = "Sietse Trommelen";
        private const string Content = "This is some content";
        private const string ShortDescription = "This is a short description";
        private const string SiteUrl = "https://elunduscore.com";
        
        [Fact]
        public async Task KontentProject_ReturnsMappedProject()
        {
            var onlineSince = DateTime.Now;
            var kontentProject = GetKontentProject(onlineSince);

            var mappedProject = await KontentMappers.MapProject(kontentProject);
            
            Assert.NotNull(mappedProject);
            Assert.Equal(Name, mappedProject.Name);
            Assert.Equal(Author, mappedProject.Author);
            Assert.Equal(Content, mappedProject.Content);
            Assert.Equal(ShortDescription, mappedProject.ShortDescription);
            Assert.Equal(SiteUrl, mappedProject.Url);
            Assert.Equal(onlineSince, mappedProject.OnlineSince);
        }
        
        [Fact]
        public async Task KontentProject_ReturnsMappedProjectWithoutDate()
        {
            var kontentProject = GetKontentProject();

            var mappedProject = await KontentMappers.MapProject(kontentProject);
            
            Assert.Null(mappedProject.OnlineSince);
        }
        
        [Fact]
        public async Task KontentProject_ReturnsMappedProjectWithTechnologies()
        {
            const string technologies = "<ul><li>.NET Core</li><li>GraphQL</li>";
            var kontentProject = GetKontentProject(technologies: technologies);

            var mappedProject = await KontentMappers.MapProject(kontentProject);
            
            Assert.Collection(mappedProject.Technologies, technology =>
            {
                Assert.Equal(".NET Core", technology);
            }, technology =>
            {
                Assert.Equal("GraphQL", technology);
            });
        }

        private static Project GetKontentProject(DateTime? onlineSince = null, string technologies = "")
        {
            var customTypeProvider = new CustomTypeProvider();

            var contentTypeSystemAttributesMock = new Mock<IContentItemSystemAttributes>();
            contentTypeSystemAttributesMock
                .SetupGet(c => c.Codename)
                .Returns(customTypeProvider.GetCodename(typeof(Project)));

            contentTypeSystemAttributesMock
                .SetupGet(c => c.Name)
                .Returns(Name);

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