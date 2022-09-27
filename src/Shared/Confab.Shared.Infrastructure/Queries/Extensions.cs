using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Infrastructure.Commands;
using Confab.Shared.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Queries
{
    public static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services,
           IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
