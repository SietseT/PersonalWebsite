using GraphQL.Types;
using Har.Domain.Components;
using Har.Domain.Models;

namespace Har.Application.GraphQL.Types
{
    public sealed class ProjectType : ObjectGraphType<Project>
    {
        public ProjectType()
        {
            Field(p => p.Id);
            Field(p => p.Name);
            Field(p => p.Author);
            Field(p => p.Technologies);
            Field(p => p.Url);
            Field(p => p.OnlineSince);
            Field(p => p.ShortDescription);
            Field(p => p.Content);
            
            Field<ListGraphType<ComponentInterface>>("components", resolve: context =>
            {
                return null;
            });
        }
    }
}