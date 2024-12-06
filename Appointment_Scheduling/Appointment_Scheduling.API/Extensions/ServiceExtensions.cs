using Appointment_Scheduling.API.LoggerService;
using Appointment_Scheduling.Application.Services.Implementations;
using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.Models;
using Appointment_Scheduling.Core.Settings;
using Appointment_Scheduling.Infrastructure.Data;
using Appointment_Scheduling.Infrastructure.Repository.Implementations;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FluentValidation.AspNetCore;
using Appointment_Scheduling.Core.Validators;
using System.Text.Json.Serialization;

namespace Appointment_Scheduling.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString("AppointmentSchedulingConnection")));

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Appointment_Scheduling API",
                    Version = "v1",
                    Description = "Appointment_Scheduling API by Ty_Graced",
                    Contact = new OpenApiContact
                    {
                        Name = "Temitayo Adesugba",
                        Email = "temitayoadesugba@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/temitayo-adesugba")
                    }
                });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: `Bearer eyJhbGciOiJIUzI1NiIsInR..."
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
                            },
                                        Scheme = "oauth2",
                                        Name = "Bearer",
                                        In = ParameterLocation.Header
                        },
                        new string[] { }
                    }
                });
            });

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(opt =>
               {
                   var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
                   opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = jwtSettings!.Issuer,
                       ValidAudience = jwtSettings.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key!)),
                   };
               });

            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ITokenService, TokenService>(); 
            services.AddScoped<IEmailService, GmailService>();

            services.Configure<GmailSettings>(configuration.GetSection("GmailSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = false;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers()
                       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(RegisterRequestValidator).Assembly))
                       .AddJsonOptions(options =>
                       {
                           options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                           options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                       });

            return services;
        }



    }
}
