using FishShop.API.Versions;
using Microsoft.AspNetCore.Mvc;

namespace FishShop.API.Controllers;

/// <summary>
/// Контроллер для авторизации
/// </summary>
[ApiController]
[ApiVersion(ApiVersions.V1)]
public class AuthController : ControllerBase
{
}