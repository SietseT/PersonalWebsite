using System.Collections.Generic;
using GraphQL.Language.AST;
using GraphQL.Types;
using Har.Domain.Components;

namespace Har.GraphQL.ValueNodes
{
    public class ComponentsAstValueConverter : IAstFromValueConverter
    {
        public IValue Convert(object value, IGraphType type)
        {
            return new ComponentsValue((IEnumerable<IComponent>)value);
        }

        public bool Matches(object value, IGraphType type)
        {
            return value is IEnumerable<IComponent>;
        }
    }
}