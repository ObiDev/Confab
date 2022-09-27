﻿using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Time;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Auth;
using Confab.Shared.Infrastructure.Commands;
using Confab.Shared.Infrastructure.Contexts;
using Confab.Shared.Infrastructure.Events;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Messaging;
using Confab.Shared.Infrastructure.Modules;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Queries;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        private const string CorsPolisy = "cors";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IList<Assembly> assemblies, IList<IModule> modules)
        {
            var disabledModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key, value) in configuration.AsEnumerable())
                {
                    if (!key.Contains(":module:enabled"))
                        continue;

                    if (!bool.Parse(value))
                        disabledModules.Add(key.Split(":")[0]);
                }
            }

            services.AddCors(cors =>
            {
                //TODO: Check origins
                cors.AddPolicy(CorsPolisy, policy =>
                {
                    policy.WithOrigins("*")
                            .WithMethods("POST", "PUT", "DELETE")
                            .WithHeaders("Content-Type", "Authotization");
                });
            });

            services.AddSwaggerGen(swagger =>
            {
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Confab API",
                    Version = "v1"
                });
            });

            services.AddSingleton<IContextFactory, ContextFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
            services.AddModuleRequests(assemblies);
            services.AddModuleInfo(modules);
            services.AddMessaging();
            services.AddAuth(modules);
            services.AddErrorHandling();
            services.AddCommands(assemblies);
            services.AddQueries(assemblies);
            services.AddEvents(assemblies);
            services.AddPostgres();
            services.AddSingleton<IClock, UtcClock>();
            services.AddHostedService<AppInitializer>();

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = manager.ApplicationParts.Where(p => p.Name.Contains(disabledModule,
                            StringComparison.InvariantCultureIgnoreCase));
                        removedParts.AddRange(parts);
                    }

                    foreach (var part in removedParts)
                    {
                        manager.ApplicationParts.Remove(part);
                    }

                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            //TODO: Check the order
            app.UseCors(CorsPolisy);
            app.UseErrorHandling();
            app.UseSwagger();
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Confab API";
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}
