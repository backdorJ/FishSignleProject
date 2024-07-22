using System.Reflection;
using System.Text;
using FishShop.API.Middlewares;
using FishShop.Core.Models;
using FishShop.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Filters;
using Path = System.IO.Path;

namespace FishShop.API;

public static class Entry
{
    /// <summary>
    /// Добавить аутентификацию
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    public static void AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        => services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JwtOptions:Audience"],
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]!))
                };
            });


    /// <summary>
    /// Добавить привязки настроек к классам
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    public static void AddBindOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

    /// <summary>
    /// Конфигурация сваггера
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static void AddSwaggerCustom(this IServiceCollection services)
        => services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

    /// <summary>
    /// Добавить кастомный Logger
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    public static void AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
        => services.AddLogging(b => b.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithProperty("Environment", "Developer")
            .WriteTo.Logger(lc =>
                lc.Filter.ByExcluding(Matching.FromSource("Microsoft"))
                    .MinimumLevel.Error()
                    .WriteTo.OpenSearch(
                        nodeUris: configuration["OpenSearch:Connection"],
                        indexFormat: "fish-logs-{0:yyyy.MM.dd}"))
            .WriteTo.Logger(lc =>
                lc.Filter.ByIncludingOnly(Matching.FromSource("Microsoft"))
                    .WriteTo.OpenSearch(
                        nodeUris: configuration["OpenSearch:Connection"],
                        indexFormat: "fish-query-logs-{0:yyyy.MM.dd}"))
            .WriteTo.Logger(lc => lc
                .MinimumLevel.Information()
                .WriteTo
                .Console())
            .CreateLogger()));

    /// <summary>
    /// Добавить подключение к бд
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration["AppContext:DatabaseConnection"]));

    /// <summary>
    /// Добавить exception middleware
    /// </summary>
    /// <param name="app">Приложение</param>
    public static void AddExceptionMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionMiddleware>();
}