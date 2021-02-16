using System;
using System.Linq;
using Har.Application.Factories.Components;
using Har.Domain.Components;

namespace Har.Domain.Factories
{
    public static class ReflectionFactory
    {
        public static IHtmlComponent[] GetHtmlComponentInstances()
        {
            return GetAllInterfaceInstances<IHtmlComponent>();
        }
        
        public static IHtmlComponentFactory[] GetComponentInstances()
        {
            return GetAllInterfaceInstances<IHtmlComponentFactory>();
        }

        private static T[] GetAllInterfaceInstances<T>() where T : class
        {
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(T));
            if (assembly == null)
                throw new Exception($"Assembly not found for type {typeof(T)}");

            return assembly.DefinedTypes
                .Where(type => type.ImplementedInterfaces.Contains(typeof(T)))
                .Select(type => assembly.CreateInstance(type.FullName!) as T)
                .ToArray();
        }
    }
}