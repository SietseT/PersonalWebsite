using System;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Har.Application.Components;
using Har.Domain.Components;
using Har.GraphQL.Queries;

namespace Har.GraphQL.Schemas
{
    public class ProjectsSchema : Schema
    {
        public ProjectsSchema(IServiceProvider provider) : base(provider)
        {
            var htmlComponentParser = provider.GetRequiredService<IComponentParser<string>>();
            ValueConverter.Register<string, IEnumerable<IComponent>>(htmlComponentParser.Parse);

            Query = provider.GetRequiredService<ProjectsQuery>();
        }
    }
}