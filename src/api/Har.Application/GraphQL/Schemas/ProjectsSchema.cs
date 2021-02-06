using System;
using GraphQL.Types;
using GraphQL.Utilities;
using Har.Application.GraphQL.Queries;

namespace Har.Application.GraphQL.Schemas
{
    public class ProjectsSchema : Schema
    {
        public ProjectsSchema(IServiceProvider provider)
        {
            Query = provider.GetRequiredService<ProjectsQuery>();
        }
    }
}