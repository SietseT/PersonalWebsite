using System;
using System.Threading.Tasks;
using Har.Infrastructure.Data.Kontent.Mappers;
using Xunit;

namespace Har.Infrastructure.Tests.Kontent.Mappers
{
    public class KontentProjectMapperTests
    {
        private const string Codename = "elunduscore";
        
        [Fact]
        public async Task KontentProject_ReturnsMappedProject()
        {
            var onlineSince = DateTime.Now;
            var kontentProject = KontentSetupHelpers.GetKontentProject(Codename, onlineSince);

            var mappedProject = await KontentMappers.MapProject(kontentProject);
            
            Assert.NotNull(mappedProject);
            Assert.Equal(KontentSetupHelpers.Name, mappedProject.Name);
            Assert.Equal(KontentSetupHelpers.Author, mappedProject.Author);
            Assert.Equal(KontentSetupHelpers.Content, mappedProject.Content);
            Assert.Equal(KontentSetupHelpers.ShortDescription, mappedProject.ShortDescription);
            Assert.Equal(KontentSetupHelpers.SiteUrl, mappedProject.Url);
            Assert.Equal(onlineSince, mappedProject.OnlineSince);
        }
        
        [Fact]
        public async Task KontentProject_ReturnsMappedProjectWithoutDate()
        {
            var kontentProject = KontentSetupHelpers.GetKontentProject(Codename);

            var mappedProject = await KontentMappers.MapProject(kontentProject);
            
            Assert.Null(mappedProject.OnlineSince);
        }
        
        [Fact]
        public async Task KontentProject_ReturnsMappedProjectWithTechnologies()
        {
            const string technologies = "<ul><li>.NET Core</li><li>GraphQL</li>";
            var kontentProject = KontentSetupHelpers.GetKontentProject(Codename, technologies: technologies);

            var mappedProject = await KontentMappers.MapProject(kontentProject);
            
            Assert.Collection(mappedProject.Technologies, technology =>
            {
                Assert.Equal(".NET Core", technology);
            }, technology =>
            {
                Assert.Equal("GraphQL", technology);
            });
        }
    }
}