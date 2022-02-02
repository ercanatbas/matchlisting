using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MatchList.Infrastructure.Services.Ioc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
            => RegisterAllTypes(services, typeof(T), assemblies, lifetime);
        public static void RegisterAllTypes(this IServiceCollection services, Type type, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(type)));
            foreach (var t in typesFromAssemblies)
                services.Add(new ServiceDescriptor(type, t, lifetime));
        }
    }
}
