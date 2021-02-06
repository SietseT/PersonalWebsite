using System;
using System.Collections.Generic;
using System.Numerics;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Har.Application.Components;
using Har.Application.GraphQL.Queries;
using Har.Application.GraphQL.Types;
using Har.Application.GraphQL.ValueNodes;
using Har.Domain.Components;

namespace Har.Application.GraphQL.Schemas
{
    public class ProjectsSchema : Schema
    {
        public ProjectsSchema(IServiceProvider provider) : base(provider)
        {
            var htmlComponentParser = provider.GetRequiredService<IComponentParser<string>>();
            ValueConverter.Register<string, IEnumerable<IComponent>>(htmlComponentParser.Parse);
            RegisterValueConverter(new ComponentsAstValueConverter());
            
            Query = provider.GetRequiredService<ProjectsQuery>();
            
            RegisterType<TextComponentType>();
        }
    }
}