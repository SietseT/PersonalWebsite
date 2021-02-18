using System.Text.Encodings.Web;
using System.Text.Json;
using GraphQL.Language.AST;
using GraphQL.Types;
using Har.Application.JsonConverters;

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
            return null;
        }

        public override object ParseValue(object value)
        {
            return null;
        }

        public override object Serialize(object value)
        {
            return JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                Converters = { new ComponentJsonConverter()},
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
        }
    }
}