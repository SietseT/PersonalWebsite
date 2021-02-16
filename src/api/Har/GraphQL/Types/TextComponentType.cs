using GraphQL.Types;
using Har.Domain.Components;
using Har.GraphQL.Interfaces;

namespace Har.GraphQL.Types
{
    public class TextComponentType : ObjectGraphType<TextComponent>
    {
        public TextComponentType()
        {
            Field(d => d.Type);
            Field(d => d.Content);

            Interface<ComponentInterface>();
        }
    }
}