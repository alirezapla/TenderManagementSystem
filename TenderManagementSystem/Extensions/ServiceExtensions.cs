using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TenderManagementSystem.Application.Behaviors;
using TenderManagementSystem.Application.Commands.Bids;
using TenderManagementSystem.Application.Mappings;
using TenderManagementSystem.Domain.Models.Entities.RBAC;
using TenderManagementSystem.Domain.Services.Abstractions;
using TenderManagementSystem.Infrastructure.Abstractions;
using TenderManagementSystem.Infrastructure.Data;
using TenderManagementSystem.Infrastructure.Data.Dapper;
using TenderManagementSystem.Infrastructure.Filters;
using TenderManagementSystem.Infrastructure.Repositories;
using TenderManagementSystem.Security;
using TenderManagementSystem.Security.Models;
using LoggerManager = TenderManagementSystem.Core.Services.LoggerManager;

namespace TenderManagementSystem.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddControllers(o => { o.UseRoutePrefix("api"); });

            services.AddHttpContextAccessor();


            services.AddScoped<LogActionFilter>();
            services.AddScoped<CustomAuthorizationFilter>();
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddScoped<IVendorQueryRepository, VendorQueryRepository>();

            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                    b => b.MigrationsAssembly("TenderManagementSystem"));
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddMemoryCache();
            services.AddSingleton<MemoryCacheEntryOptions>(provider =>
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Cache for 10 minutes
                });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining(typeof(CreateBidCommand)));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.ConfigureCors(configuration);

            services.ConfigureIdentity();
            services.AddSystemAuthorization();

            services.ConfigureJwt(configuration);

            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument();

            services.ConfigureSwagger();
        }

        private static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() {Title = "Tender Management System API", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }


        private static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(
                options => options.AddPolicy(
                    "CorsPolicy",
                    policy => policy.WithOrigins(
                            configuration["BackendUrl"] ?? "http://localhost:5050")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                )
            );
        }

        private static void AddSystemAuthorization(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddAuthorization(options =>
            {
                options.AddPolicy("CanRead",
                    policy => { policy.Requirements.Add(new AuthorizationRequirement("Read")); });
                options.AddPolicy("CanWrite",
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new AuthorizationRequirement("Write"));
                    });
            });
            serviceProvider.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();
            serviceProvider.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
            serviceProvider.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        }

        private static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(o =>
                {
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = true;
                    o.Password.RequireUppercase = false;
                    o.Password.RequiredLength = 8;
                    o.SignIn.RequireConfirmedEmail = false;
                }).AddEntityFrameworkStores<AppDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>() 
                .AddDefaultTokenProviders();
        }

        private static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["key"];

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
        }
    }
}