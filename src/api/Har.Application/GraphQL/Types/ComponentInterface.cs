using GraphQL.Language.AST;
using GraphQL.Types;
using Har.Domain.Components;
using Har.Domain.Models;

namespace Har.Application.GraphQL.Types
{
    public sealed class ComponentInterface : InterfaceGraphType<IComponent>
    {
        public ComponentInterface()
        {
            Field(c => c.Name);
        }
    }
}