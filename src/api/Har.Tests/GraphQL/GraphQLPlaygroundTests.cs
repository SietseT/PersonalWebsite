using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Har.Tests.GraphQL
{
    public class GraphQLPlaygroundTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GraphQLPlaygroundTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task PlaygroundUrl_ReturnsSuccessfulResponse()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/ui/graphiql");

            response.EnsureSuccessStatusCode();
            Assert.Contains("GraphiQL", await response.Content.ReadAsStringAsync());
        }
    }
}