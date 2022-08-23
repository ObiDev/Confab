using Confab.Shared.Abstractions.Exceptions;
using Humanizer;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                ConfabException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(exception), ex.Message)), 
                    HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(new ErrorsResponse(new Error("error", "There was an error!")), 
                    HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);
        private record ErrorsResponse(params Error[] errors);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            var code = type.Name.Underscore().Replace("_exception", string.Empty);
            return Codes.GetOrAdd(type, code); 

        }
    }
}
