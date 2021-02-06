using System.Runtime.CompilerServices;
using GraphQL.Types;
using Har.Domain.Models;

namespace Har.Application.GraphQL.Types
{
    public sealed class ProjectType : ObjectGraphType<Project>
    {
        public ProjectType()
        {
            Field(p => p.Name);
            Field(p => p.Technologies);
            Field(p => p.Url);
            Field(p => p.OnlineSince);
            Field(p => p.ShortDescription);
        }
    }
}