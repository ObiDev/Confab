using Confab.Shared.Abstractions.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ExceptionCompositionRoot : IExceptionCompositionRoot
    {
        private readonly IServiceProvider _serviceProvider;

        public ExceptionCompositionRoot(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ExceptionResponse Map(Exception exception)
        {
            using var scope = _serviceProvider.CreateScope();
            var mappers = scope.ServiceProvider.GetServices<IExceptionToResponseMapper>().ToArray();
            var nonDefaultMappers = mappers.Where(m => m is not ExceptionToResponseMapper);
            var result = nonDefaultMappers
                .Select(m => m.Map(exception))
                .SingleOrDefault(m => m is not null);

            if (result is not null)
                return result;

            var defaultMapper = mappers.SingleOrDefault(m => m is ExceptionToResponseMapper);

            return defaultMapper?.Map(exception);
        }
    }
}
