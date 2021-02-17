using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using Har.Application.JsonConverters;
using Har.Domain.Components;
using Har.GraphQL.ValueNodes;

namespace Har.GraphQL.Types
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
            return JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                Converters = { new ComponentJsonConverter()},
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            
            // return ValueConverter.ConvertTo(value, typeof(IEnumerable<IComponent>)); 
        }
    }
}