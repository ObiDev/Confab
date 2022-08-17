using System.Net;

namespace Confab.Shared.Abtractions.Exceptions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
