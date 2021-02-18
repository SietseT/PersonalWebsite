using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GraphQL.NewtonsoftJson;
using GraphQL.Server.Common;
using Har.Domain.Components;
using Har.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Har.Tests.GraphQL
{
    public class GraphQLProjectsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        private const string ProjectsQuery = @"query Projects {
                            projects {
                                content
                                author  
                                id  
                                name
                                onlineSince 
                                shortDescription
                                technologies
                                url
                                components
                            }
                        }";
        
        private const string ProjectQuery = @"query Projects {
                            project(id:""elunduscore"") {
                                content
                                author  
                                id  
                                name
                                onlineSince 
                                shortDescription
                                technologies
                                url
                                components
                            }
                        }";

        public GraphQLProjectsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task ProjectsQuery_ReturnsSuccessfulResponse()
        {
            // Arrange
            var client = _factory.CreateClient();
            var query = new GraphQLRequest
            {
                Query = ProjectsQuery
            };
            
            // Act
            var response = await client.PostAsync("/graphql", JsonContent.Create(query.GetValue()));
            var projects = await GetProjectsFromResponse(response);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(2, projects.Count());
        }
        
        [Fact]
        public async Task ProjectsQuery_ReturnsProjectsWithParsedComponents()
        {
            // Arrange
            var client = _factory.CreateClient();
            var query = new GraphQLRequest
            {
                Query = ProjectsQuery
            };
            
            // Act
            var response = await client.PostAsync("/graphql", JsonContent.Create(query.GetValue()));
            var projects = await GetProjectsFromResponse(response);
            var projectsArray = projects as Project[] ?? projects.ToArray();
            
            var firstProject = projectsArray.FirstOrDefault();
            dynamic components = JsonConvert.DeserializeObject<IEnumerable<ExpandoObject>>(firstProject?.Components!,
                new ExpandoObjectConverter());

            var componentsCollection = ((IEnumerable) components)!
                .Cast<dynamic>();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Collection(componentsCollection, component =>
            {
                Assert.Equal(new TextComponent().Type, component.type);
            }, component =>
            {
                Assert.Equal(new ImagesComponent().Type, component.type);
            }, component =>
            {
                Assert.Equal(new TextComponent().Type, component.type);
            });
        }
        
        [Fact]
        public async Task ProjectQuery_ReturnsSuccessfulResponse()
        {
            // Arrange
            var client = _factory.CreateClient();
            var query = new GraphQLRequest
            {
                Query = ProjectQuery
            };
            
            // Act
            var response = await client.PostAsync("/graphql", JsonContent.Create(query.GetValue()));
            var project = await GetProjectFromResponse(response);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(project);
        }

        private static async Task<IEnumerable<Project>> GetProjectsFromResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var contentJobject = JObject.Parse(content);

            var results = contentJobject["data"]?["projects"]?.Children().ToArray();

            return results!.Select(result => result.ToObject<Project>()).ToList();
        }
        
        private static async Task<Project> GetProjectFromResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var contentJobject = JObject.Parse(content);

            var result = contentJobject["data"]?["project"];

            return result?.ToObject<Project>();
        }
    }
}