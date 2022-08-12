using Confab.Shared.Abtractions;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClock, UtcClock>();

            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Confab API!");
                });
            });

            return app;
        }
    }
}
