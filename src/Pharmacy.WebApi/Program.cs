using Microsoft.EntityFrameworkCore;
using Pharmacy.WebApi.Common.Extensions;
using Pharmacy.WebApi.Common.Middlewares;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Helpers;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Mappers;
using Pharmacy.WebApi.Repositories;
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


// ----> Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// ----> Services
builder.Services.AddServices();
builder.Services.AddSwaggerAuthorization();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddHttpContextAccessor();


builder.Services.AddAutoMapper(expression => expression.AddProfile<MapperProfile>());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// ---> Middleware
var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddlewar>(); // nomini nomlashda AspNetning ichidagi middleware bilan bir xil bo'lmasligi kerak
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
