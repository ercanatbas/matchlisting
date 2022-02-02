using System;
using AutoMapper;

namespace MatchList.Infrastructure.Services.Map
{
    public interface IMapperService 
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts);
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source,TDestination destination);
        TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts);
    }
}
