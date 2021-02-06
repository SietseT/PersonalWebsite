using System.Runtime.CompilerServices;
using GraphQL.Types;
using Har.Application.GraphQL.Interfaces;
using Har.Domain.Components;

namespace Har.Application.GraphQL.Types
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