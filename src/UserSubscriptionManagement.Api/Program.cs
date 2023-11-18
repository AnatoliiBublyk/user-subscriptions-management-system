using System.Text;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using UserSubscriptionManagement.Application.Repositories;
using UserSubscriptionManagement.Application.Services;
using UserSubscriptionManagement.Application.Services.Interfaces;
using UserSubscriptionManagement.Infrastructure.Database;
using UserSubscriptionManagement.Infrastructure.Mapping;
using UserSubscriptionManagement.Infrastructure.Middleware;
using UserSubscriptionManagement.Infrastructure.Repository;
using UserSubscriptionManagement.Infrastructure.Services;
using UserSubscriptionManagement.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MapsterConfig>();
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddDbContext<UserSubscriptionsManagementContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IHashService, Sha256Service>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

var timeOffset = builder.Configuration.GetSection("SchedulerOptions").GetValue<TimeSpan>("UnsubscribeTimeDiff");
builder.Services.AddSingleton(p => new ScheduledWithdrawalService(p, timeOffset));

builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();


builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Auth header  using Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    opt.OperationFilter<SecurityRequirementsOperationFilter>();

});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("TokenInfo:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.MapControllers();

await app.Services.GetRequiredService<ScheduledWithdrawalService>().StartAsync();
app.Run();
