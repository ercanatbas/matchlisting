using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using FluentValidation;
using MatchList.Application.Behaviors;
using MatchList.Infrastructure;
using MatchList.Infrastructure.Repositories.Base;
using MatchList.Infrastructure.Repositories.Base.EntityFramework;
using MatchList.Infrastructure.Repositories.UnitOfWork;
using MatchList.Infrastructure.Services.Ioc;
using MatchList.Infrastructure.Services.Ioc.Autofac;
using MatchList.Infrastructure.Services.Map;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MatchList.Api.Extensions
{
    public static class RegisterExtensions
    {
        private static Assembly CurrentAssembly        => Assembly.Load("MatchList.Api");
        private static Assembly ApplicationAssembly    => Assembly.Load("MatchList.Application");
        private static Assembly DomainAssembly         => Assembly.Load("MatchList.Domain");
        private static Assembly InfrastructureAssembly => Assembly.Load("MatchList.Infrastructure");


        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(CurrentAssembly, ApplicationAssembly, DomainAssembly, InfrastructureAssembly);
        }

        public static void AddMediator(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterAssemblyTypes(ApplicationAssembly, CurrentAssembly).AsClosedTypesOf(typeof(IRequestHandler<,>)).InstancePerDependency();
            builder.RegisterAssemblyTypes(CurrentAssembly).AsClosedTypesOf(typeof(INotificationHandler<>)).InstancePerDependency();
        }

        public static void AddServices(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ApplicationAssembly)
                   .Where(t => t.Name.EndsWith("Query"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }

        public static void AddRepositories(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(InfrastructureAssembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }

        public static void AddContainer(this IServiceCollection services)
        {
            services.AddSingleton<IResolver, Resolver>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseNpgsql(configuration["ConnectionString"], b =>
                   {
                       b.MigrationsAssembly("MatchList.Infrastructure");
                       b.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), null);
                   })
                   .UseLowerCaseNamingConvention()
                   .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information,
                          DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.UtcTime);
            });

            services.AddScoped<DbContext>(x => x.GetService<DatabaseContext>());
        }

        public static void AddValidators(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ApplicationAssembly)
                   .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                   .AsImplementedInterfaces();
        }

        public static void AddMaps(this ContainerBuilder builder)
        {
            builder.RegisterType<MapperService>().As<IMapperService>().SingleInstance();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                   .As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                    cfg.AddProfile(profile);
            })).AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                   .As<IMapper>()
                   .InstancePerLifetimeScope();
        }

        public static void AddCorses(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("development", x =>
                {
                    x.AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials()
                     .WithOrigins("http://localhost:5200", "http://localhost:8080", "http://localhost:4000")
                     .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });
        }

        public static void AddStart(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, MatchListUow>();
        }
    }
}