using System.Net.Http.Headers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectName.ServiceName.WebApi.Filters;

namespace ProjectName.ServiceName.WebApi.Controllers;

[ApiController]
//TODO: use [Authorize]
[ApiExceptionFilter]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    
    protected string? GetAuthToken()
    {
        return AuthenticationHeaderValue.TryParse(HttpContext.Request.Headers["Authorization"], out var headerValue) ? headerValue.Parameter : null;
    }
}
