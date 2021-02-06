using System.Collections.Generic;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using Har.Application.GraphQL.ValueNodes;
using Har.Domain.Components;

namespace Har.Application.GraphQL.Types
{
    public class ComponentsType : ScalarGraphType
    {
        public ComponentsType()
        {
            Name = "Components";
        }

        public override object ParseLiteral(IValue value)
        {
            // new test
            if (value is ComponentsValue componentsValue)
                return ParseValue(componentsValue.Value);
            
            return value is StringValue stringValue
                ? ParseValue(stringValue.Value)
                : null;
        }

        public override object ParseValue(object value)
        {
            return ValueConverter.ConvertTo(value, typeof(IEnumerable<IComponent>));
        }

        public override object Serialize(object value)
        {
            return ValueConverter.ConvertTo(value, typeof(IEnumerable<IComponent>));
        }
    }
}