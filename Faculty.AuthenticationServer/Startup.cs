using System;
using AutoMapper;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Faculty.AuthenticationServer.Tools;
using Microsoft.Extensions.Configuration;
using Faculty.AuthenticationServer.Models;
using Faculty.AuthenticationServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Faculty.AuthenticationServer.Services.Interfaces;

namespace Faculty.AuthenticationServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IServiceProvider _serviceProvider;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseContext(Configuration);
            services.AddIdentityConfiguration();
            services.AddAuthenticationWithJwtToken(Configuration);
            services.AddAuthorizationWithRole();
            services.AddSwaggerConfiguration();
            services.AddControllers();
            services.AddCors();
            services.AddControllerServices();
            _serviceProvider = services.BuildServiceProvider();
            services.AddMapper(_serviceProvider);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication server v1"));
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConStr");
            services.AddDbContext<CustomIdentityContext>(option => option.UseSqlServer(connectionString));
        }

        public static void AddAuthenticationWithJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            var authOption = new AuthOptions(configuration);
            services.AddSingleton(options => new AuthOptions(configuration));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authOption?.Issuer,
                            ValidateAudience = true,
                            ValidAudience = authOption?.Audience,
                            IssuerSigningKey = authOption?.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true
                        };
                    });
        }

        public static void AddAuthorizationWithRole(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "administrator");
                });

                options.AddPolicy("Employee", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "employee");
                });
            });
        }

        public static void AddControllerServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Authentication server", 
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Value template: \"Bearer ****\")",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[]{}
                    }
                });
            });
        }

        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<CustomUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<CustomIdentityContext>();
        }

        public static void AddMapper(this IServiceCollection services, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<CustomUser>>();
            services.AddSingleton<AutoMapper.IConfigurationProvider>(x =>
                new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile(userManager))));
            services.AddSingleton<IMapper, Mapper>();
        }
    }
}
