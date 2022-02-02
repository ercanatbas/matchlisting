using System;
using Autofac;

namespace MatchList.Infrastructure.Services.Ioc.Autofac
{
    public class Resolver : IResolver
    {
        private readonly IComponentContext _context;

        public Resolver(IComponentContext context)
        {
            _context = context;
        }

        public T Resolve<T>() => _context.Resolve<T>();

        public T Resolve<T>(Type type) => (T)_context.Resolve(type);

        public object Resolve(Type type) => _context.Resolve(type);

        public T[] ResolveAll<T>()
        {
            throw new NotImplementedException();
        }

        public object[] ResolveAll(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
