using FishShop.Contracts.Models;
using FishShop.Core.Abstractions;
using FishShop.Core.Constants;
using FishShop.Core.Entities;
using FishShop.Core.Exceptions;
using FishShop.Core.Services;
using FishShop.Core.Services.GuidFactory;
using FishShop.Core.Services.PasswordService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IPublisher = FishShop.RabbitMQ.IPublisher;

namespace FishShop.Core.Requests.AuthRequests.PostRegister;

/// <summary>
/// Обработчик для <see cref="PostRegisterCommand"/>
/// </summary>
public class PostRegisterCommandHandler : IRequestHandler<PostRegisterCommand>
{
    private const string QueueRouteKey = "email-codes";
    private const string QueueName = "email-codes";
    
    private readonly IDbContext _dbContext;
    private readonly IPasswordService _passwordService;
    private readonly IGuidFactory _guidFactory;
    private readonly IPublisher _publisher;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="passwordService">Сервис для хеширования паролей</param>
    /// <param name="guidFactory">Фабрика guid</param>
    /// <param name="publisher">Отправить сообщение в очередь</param>
    public PostRegisterCommandHandler(
        IDbContext dbContext,
        IPasswordService passwordService,
        IGuidFactory guidFactory,
        IPublisher publisher)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
        _guidFactory = guidFactory;
        _publisher = publisher;
    }

    /// <inheritdoc />
    public async Task Handle(PostRegisterCommand request, CancellationToken cancellationToken)
    {
        await ValidateAsync(request, cancellationToken);
        
        var baseRoleFromDb = await _dbContext.Roles
            .FirstOrDefaultAsync(x => x.Id == DefaultRolesIds.User, cancellationToken)
            ?? throw new ApplicationBaseException($"Роль с id {DefaultRolesIds.User} не найдена");

        var generateRandomCode = _guidFactory.GetGuid();
        var hashPassword = _passwordService.HashPassword(request.Password);
        
        var user = new User(
            userName: request.Username,
            hashPassword: hashPassword,
            email: request.Email,
            tempEmailCode: generateRandomCode.ToString(),
            details: new UserDetail
            {
                FirstName = request.UserDetails.FirstName,
                LastName = request.UserDetails.LastName,
                Patronymic = request.UserDetails.Patronymic,
                BirthDate = request.UserDetails.BirthDate
            }, 
            roles: new List<Role>
            {
                baseRoleFromDb
            });
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _publisher.Send(new QueueRequest
        {
            Message = new Dictionary<string, string>
            {
                ["Code"] = generateRandomCode.ToString(),
                ["Email"] = request.Email
            },
            RoutingKey = QueueRouteKey,
            QueueName = QueueName,
        });
    }

    private async Task ValidateAsync(PostRegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var isExist = await _dbContext.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (isExist)
            throw new ApplicationException("Такой пользователь уже существует");
        
        if (string.IsNullOrEmpty(request.Username))
            throw new RequiredException("Поле 'Логин' является обязательным");

        if (string.IsNullOrEmpty(request.Password))
            throw new RequiredException("Поле 'Пароль' является обязательным");

        if (!RegularExpressions.IsValidEmail(request.Email))
            throw new ValidationException("Поле 'Почта' указана некорректно");

        if (request.UserDetails is null)
            throw new RequiredException("Заполните дополнительную информацию о себе");

        if (string.IsNullOrEmpty(request.UserDetails.FirstName))
            throw new RequiredException("Поле 'Имя' является обязательным");
        
        if (string.IsNullOrEmpty(request.UserDetails.LastName))
            throw new RequiredException("Поле 'Фамилия' является обязательным");
    }
}