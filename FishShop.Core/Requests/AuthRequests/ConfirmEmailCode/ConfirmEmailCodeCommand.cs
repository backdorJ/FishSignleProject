using FishShop.Contracts.Requests.AuthRequests.ConfirmEmailCode;
using MediatR;

namespace FishShop.Core.Requests.AuthRequests.ConfirmEmailCode;

/// <summary>
/// Команда на подтверждение почты
/// </summary>
public class ConfirmEmailCodeCommand : ConfirmEmailCodeRequest, IRequest
{
}