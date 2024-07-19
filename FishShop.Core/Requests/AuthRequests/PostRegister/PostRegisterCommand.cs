using FishShop.Contracts.Requests.AuthRequests.PostRegister;
using MediatR;

namespace FishShop.Core.Requests.AuthRequests.PostRegister;

/// <summary>
/// Команда на регистрацию в системе
/// </summary>
public class PostRegisterCommand : PostRegisterRequest, IRequest
{
}