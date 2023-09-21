using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectName.ServiceName.WebApi.Filters;

namespace ProjectName.ServiceName.WebApi.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
