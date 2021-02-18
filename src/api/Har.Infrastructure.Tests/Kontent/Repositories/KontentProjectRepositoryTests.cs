using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Har.Infrastructure.Data.Kontent.Repositories;
using Har.Infrastructure.Data.Kontent.Types;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Har.Infrastructure.Tests.Kontent.Repositories
{
    public class KontentProjectRepositoryTests
    {
        private readonly NullLogger<KontentProjectRepository> _mockLogger;
        
        private const string Codename1 = "elunduscore";
        private const string Codename2 = "elunduscore2";

        public KontentProjectRepositoryTests()
        {
            _mockLogger = new NullLogger<KontentProjectRepository>();
        }
        
        [Fact]
        public async Task KontentProjectRepository_ReturnsProjects()
        {
            // Arrange
            var deliveryClient = new FakeProjectsDeliveryClient(GetProjects());
            var kontentProjectsRepository = new KontentProjectRepository(deliveryClient, _mockLogger);

            // Act
            var projects = await kontentProjectsRepository.GetProjectsAsync();
            var projectsArray = projects.ToArray();

            // Assert
            Assert.NotEmpty(projectsArray);
            Assert.Equal(2, projectsArray.Length);
        }
        
        [Fact]
        public async Task KontentProjectRepositoryGetProjectsList_ThrowsException()
        {
            // Arrange
            var deliveryClient = new FakeProjectsDeliveryClient(null);
            var kontentProjectsRepository = new KontentProjectRepository(deliveryClient, _mockLogger);
            
            //Act
            var projects = await kontentProjectsRepository.GetProjectsAsync();

            // Assert
            Assert.Empty(projects);
        }
        
        [Fact]
        public async Task KontentProjectRepository_ReturnsSingleProject()
        {
            // Arrange
            var deliveryClient = new FakeProjectsDeliveryClient(GetProjects());
            var kontentProjectsRepository = new KontentProjectRepository(deliveryClient, _mockLogger);

            // Act
            var project = await kontentProjectsRepository.GetProjectByIdAsync(Codename2);

            // Assert
            Assert.NotNull(project);
            Assert.Equal(Codename2, project.Id);
        }
        
        [Fact]
        public async Task KontentProjectRepositoryGetSingleProject_ReturnsSingleProject()
        {
            // Arrange
            var deliveryClient = new FakeProjectsDeliveryClient(null);
            var kontentProjectsRepository = new KontentProjectRepository(deliveryClient, _mockLogger);
            
            // Act
            var project = await kontentProjectsRepository.GetProjectByIdAsync(Codename1);

            // Assert
            Assert.Null(project);
        }
        
        private IEnumerable<Project> GetProjects()
        {
            return new[]
            {
                KontentSetupHelpers.GetKontentProject(Codename1),
                KontentSetupHelpers.GetKontentProject(Codename2)
            };
        }
    }
}