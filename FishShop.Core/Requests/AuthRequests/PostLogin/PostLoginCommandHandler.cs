using FishShop.Contracts.Models;
using FishShop.Contracts.Requests.AuthRequests.PostLogin;
using FishShop.Core.Abstractions;
using FishShop.Core.Constants;
using FishShop.Core.Exceptions;
using FishShop.Core.Services.JWTService;
using FishShop.Core.Services.NextFactory;
using FishShop.Core.Services.PasswordService;
using FishShop.Core.Services.UserClaimsManager;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Publisher = FishShop.RabbitMQ.IPublisher;

namespace FishShop.Core.Requests.AuthRequests.PostLogin;

/// <summary>
/// Обработчик для <see cref="PostLoginCommand"/>
/// </summary>
public class PostLoginCommandHandler
    : IRequestHandler<PostLoginCommand, PostLoginResponse>
{
    private const string QueueRouteKey = "email-codes";
    private const string QueueName = "email-codes";
    
    private readonly IDbContext _dbContext;
    private readonly IUserMangerService _mangerService;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IPasswordService _passwordService;
    private readonly Publisher _publisher;
    private readonly INextFactory _nextFactory;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="mangerService">Сервис для работы с claims</param>
    /// <param name="jwtGenerator">Jwt сервис</param>
    /// <param name="passwordService">Сервис по паролям</param>
    /// <param name="publisher">Паблишер в RabiitMq</param>
    /// <param name="nextFactory">Фабрика рандомных чисел</param>
    public PostLoginCommandHandler(
        IDbContext dbContext,
        IUserMangerService mangerService,
        IJwtGenerator jwtGenerator,
        IPasswordService passwordService,
        Publisher publisher,
        INextFactory nextFactory)
    {
        _dbContext = dbContext;
        _mangerService = mangerService;
        _jwtGenerator = jwtGenerator;
        _passwordService = passwordService;
        _publisher = publisher;
        _nextFactory = nextFactory;
    }

    /// <inheritdoc />
    public async Task<PostLoginResponse> Handle(PostLoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var currentUser = await _dbContext.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
            ?? throw new ApplicationBaseException($"Пользователь с почтой {request.Email} не найден");

        var generateRandomCode = _nextFactory.GetNextFactory();
        
        if (currentUser.Status != UserRegisterStatus.RegisteredAndConfirmed)
        {
            _publisher.Send(new QueueRequest
            {
                Message = new Dictionary<string, string>
                {
                    ["Code"] = generateRandomCode,
                    ["Email"] = request.Email
                },
                RoutingKey = QueueRouteKey,
                QueueName = QueueName,
            });

            throw new ApplicationBaseException("Вы не подтвердили почту, вам на почту заново отправлен код.");
        }
        
        if (!_passwordService.VerifyPassword(request.Password, currentUser.HashPassword))
            throw new ApplicationBaseException("Пароль или почта неверная");
        
        var claims = _mangerService.GetUserClaims(currentUser);
        var token = _jwtGenerator.GenerateToken(claims);

        return new PostLoginResponse
        {
            AccessToken = token
        };
    }
}