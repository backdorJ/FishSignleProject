using FishShop.Contracts.Requests.AuthRequests.PostLogin;
using MediatR;

namespace FishShop.Core.Requests.AuthRequests.PostLogin;

/// <summary>
/// Команда на логин
/// </summary>
public class PostLoginCommand : PostLoginRequest, IRequest<PostLoginResponse>
{
    
}