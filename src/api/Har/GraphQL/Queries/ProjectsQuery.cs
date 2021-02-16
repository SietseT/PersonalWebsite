using System;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Har.Application.Services;
using Har.GraphQL.Types;

namespace Har.GraphQL.Queries
{
    public class ProjectsQuery : ObjectGraphType<object>
    {
        public ProjectsQuery(IServiceProvider provider)
        {
            var projectRepository1 = provider.GetRequiredService<IProjectRepository>();
            
            Name = "Query";
            
            FieldAsync<ListGraphType<ProjectType>>("projects",
                resolve: async _ => await projectRepository1.GetProjectsAsync());
            
            FieldAsync<ProjectType>("project",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
                        {Name = "id", Description = "Project ID or codename"}),
                resolve: async c => await projectRepository1.GetProjectByIdAsync(c.GetArgument<string>("id")));
        }
    }
}