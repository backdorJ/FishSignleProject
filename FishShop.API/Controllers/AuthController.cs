using FishShop.API.Versions;
using FishShop.Contracts.Requests.AuthRequests.PostLogin;
using FishShop.Contracts.Requests.AuthRequests.PostRegister;
using FishShop.Core.Requests.AuthRequests.PostLogin;
using FishShop.Core.Requests.AuthRequests.PostRegister;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FishShop.API.Controllers;

/// <summary>
/// Контроллер для авторизации
/// </summary>
[ApiVersion(ApiVersions.V1)]
public class AuthController : BaseApiController
{
    /// <summary>
    /// Зарегистрировать пользователя в системе
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Register")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task PostRegisterAsync(
        [FromBody] PostRegisterRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostRegisterCommand
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            UserDetails = new PostRegisterUserDetailRequest
            {
                FirstName = request.UserDetails.FirstName,
                LastName = request.UserDetails.LastName,
                Patronymic = request.UserDetails.Patronymic,
                BirthDate = request.UserDetails.BirthDate
            }
        }, cancellationToken);

    /// <summary>
    /// Войти в систему
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Токен</returns>
    [HttpPost("Login")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostLoginResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<PostLoginResponse> PostLoginAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostLoginRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostLoginCommand
        {
            Email = request.Email,
            Password = request.Password
        }, cancellationToken);
}