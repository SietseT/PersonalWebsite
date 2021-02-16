using GraphQL.Types;
using Har.Domain.Components;

namespace Har.GraphQL.Interfaces
{
    public sealed class ComponentInterface : InterfaceGraphType<IComponent>
    {
        public ComponentInterface()
        {
            Field(c => c.Type);
        }
    }
}