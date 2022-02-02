using System;

namespace MatchList.Infrastructure.Services.Ioc
{
    public interface IResolver
    {
        T Resolve<T>();
        T Resolve<T>(Type type);
        
        object Resolve(Type type);
        T[] ResolveAll<T>();
        object[] ResolveAll(Type type);
    }
}
