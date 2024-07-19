using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FishShop.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ProblemDetails))]
public class BaseApiController : ControllerBase
{
}