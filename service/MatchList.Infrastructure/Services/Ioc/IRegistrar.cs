using System;
using System.Reflection;

namespace MatchList.Infrastructure.Services.Ioc
{
    public interface IRegistrar
    {
        void Register<T>(LifeStyle lifeStyle = LifeStyle.Singleton)
            where T : class;
        void Register(Type type, LifeStyle lifeStyle = LifeStyle.Singleton);

        void Register<TType, TImpl>(LifeStyle lifeStyle = LifeStyle.Singleton, params Assembly[] assemblyLimitation)
            where TType : class
            where TImpl : class, TType;
        void Register(Type type, Type impl, LifeStyle lifeStyle = LifeStyle.Singleton, params Assembly[] assemblyLimitation);
        bool IsRegistered(Type type);
        bool IsRegistered<TType>();
    }
}
