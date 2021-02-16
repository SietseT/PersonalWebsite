using System.Collections.Generic;
using GraphQL.Language.AST;
using Har.Domain.Components;

namespace Har.GraphQL.ValueNodes
{
    public class ComponentsValue : ValueNode<IEnumerable<IComponent>>
    {
        public ComponentsValue(IEnumerable<IComponent> value)
        {
            Value = value;
        }
        
        protected override bool Equals(ValueNode<IEnumerable<IComponent>> node)
        {
            return Value.Equals(node.Value);
        }
    }
}