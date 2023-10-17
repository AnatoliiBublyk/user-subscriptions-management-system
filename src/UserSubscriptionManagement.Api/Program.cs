using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionManagement.Infrastructure.Database;
using UserSubscriptionManagement.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddDbContext<UserSubscriptionsManagementContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
