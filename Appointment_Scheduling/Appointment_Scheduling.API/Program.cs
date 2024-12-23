using Appointment_Scheduling.API.Extensions;
using Appointment_Scheduling.API.LoggerService;
using Appointment_Scheduling.Application.Services.Implementations;
using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.Validators;
using Appointment_Scheduling.Infrastructure.Data.SeedData;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NLog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureLoggerService();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureNpgsqlContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(RegisterRequestValidator).Assembly))
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
         options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
     });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.ConfigureSwagger();

// Add CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
}

// Enable CORS
app.UseCors("AllowAll");

// Enable Athentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
