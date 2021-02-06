using System.Collections.Generic;
using Har.Domain.Components;

namespace Har.Application.Components
{
    public interface IComponentParser<in T>
    {
        IEnumerable<IComponent> Parse(T value);
    }
}