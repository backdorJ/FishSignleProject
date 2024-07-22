using FishShop.Core.Abstractions;
using FishShop.Core.Constants;
using FishShop.Core.Exceptions;
using FishShop.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace FishShop.Core.Requests.AuthRequests.ConfirmEmailCode;

/// <summary>
/// Обработчик для <see cref="ConfirmEmailCodeCommand"/>
/// </summary>
public class ConfirmEmailCodeCommandHandler(IDbContext dbContext) : IRequestHandler<ConfirmEmailCodeCommand>
{
    /// <inheritdoc />
    public async Task Handle(ConfirmEmailCodeCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!RegularExpressions.IsValidEmail(request.Email))
            throw new ValidationException("Почта указана неверна");
        
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
            ?? throw new ApplicationBaseException($"Пользователь с почтой {request.Email} не найден.");

        if (user.TempEmailCode == null)
            throw new ApplicationBaseException("Почта данного пользователя уже подтверждена");

        if (user.TempEmailCode != request.Code)
            throw new ApplicationBaseException("Код неверный");
        
        user.TempEmailCode = null;
        user.ChangeStatus(UserRegisterStatus.RegisteredAndConfirmed);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}