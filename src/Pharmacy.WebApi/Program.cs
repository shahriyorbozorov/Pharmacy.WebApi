using Microsoft.EntityFrameworkCore;
using Pharmacy.WebApi.Common.Middlewares;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Helpers;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.Interfaces.Managers;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Mappers;
using Pharmacy.WebApi.Repositories;
using Pharmacy.WebApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---> Databases
var connectionString = builder.Configuration.GetConnectionString("PostgresDevelopmentDb");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// ---->  Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddMemoryCache();

// ----> Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// ----> Services
builder.Services.AddScoped<IDrugService, DrugService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddAutoMapper(expression => expression.AddProfile<MapperProfile>());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// ---> Middleware
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
