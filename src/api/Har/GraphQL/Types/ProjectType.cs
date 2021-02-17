using System;
using GraphQL.Types;
using GraphQL.Utilities;
using Har.Application.Components;
using Har.Domain.Models;

namespace Har.GraphQL.Types
{
    public sealed class ProjectType : ObjectGraphType<Project>
    {
        public ProjectType(IServiceProvider provider)
        {
            var componentParser = provider.GetRequiredService<IComponentParser<string>>();
            
            Field(p => p.Id);
            Field(p => p.Name);
            Field(p => p.Author);
            Field(p => p.Technologies);
            Field(p => p.Url);
            Field(p => p.OnlineSince, true);
            Field(p => p.ShortDescription);
            Field(p => p.Content);
            
            Field<ComponentsType>("components", 
                resolve: context => componentParser.Parse(context.Source.Content));
        }
    }
}