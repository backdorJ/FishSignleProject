using FishShop.Contracts.Requests.AuthRequests.PostLogin;
using FishShop.Core.Abstractions;
using FishShop.Core.Services.JWTService;
using FishShop.Core.Services.PasswordService;
using FishShop.Core.Services.UserClaimsManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishShop.Core.Requests.AuthRequests.PostLogin;

/// <summary>
/// Обработчик для <see cref="PostLoginCommand"/>
/// </summary>
public class PostLoginCommandHandler
    : IRequestHandler<PostLoginCommand, PostLoginResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserMangerService _mangerService;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordService _passwordService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="mangerService">Сервис для работы с claims</param>
    /// <param name="jwtGenerator">Jwt сервис</param>
    /// <param name="passwordService">Сервис по паролям</param>
    public PostLoginCommandHandler(
        IDbContext dbContext,
        IUserMangerService mangerService,
        IJwtGenerator jwtGenerator,
        IPasswordService passwordService)
    {
        _dbContext = dbContext;
        _mangerService = mangerService;
        _jwtGenerator = jwtGenerator;
        _passwordService = passwordService;
    }

    /// <inheritdoc />
    public async Task<PostLoginResponse> Handle(PostLoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var currentUser = await _dbContext.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
            ?? throw new ApplicationException($"Пользователь с почтой {request.Email} не найден");

        if (!_passwordService.VerifyPassword(request.Password, currentUser.HashPassword))
            throw new ApplicationException("Пароль или почта неверная");
            
        var claims = _mangerService.GetUserClaims(currentUser);
        var token = _jwtGenerator.GenerateToken(claims);

        return new PostLoginResponse
        {
            AccessToken = token
        };
    }
}