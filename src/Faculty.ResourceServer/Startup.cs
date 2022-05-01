using System;
using AutoMapper;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Faculty.ResourceServer.Tools;
using Microsoft.EntityFrameworkCore;
using Faculty.BusinessLayer.Services;
using Faculty.DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;
using Faculty.BusinessLayer.Interfaces;
using Faculty.DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Faculty.DataAccessLayer.Repository.EntityFramework;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.ResourceServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseContext(Configuration, HostEnvironment.IsProduction());
            services.AddRepositories();
            services.AddControllerServices();
            services.AddMapper();
            services.AddSwaggerConfiguration();
            services.AddAuthenticationWithJwtToken(Configuration);
            services.AddAuthorizationWithRole();
            services.AddControllers();
            services.AddCors(opt =>
            {
                opt.AddPolicy("AnyAccess", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation($"--> Root - {Environment.CurrentDirectory}");
            logger.LogInformation($"--> Environment - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            logger.LogInformation($"--> Connection string - {Configuration.GetConnectionString("Faculty")}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resource server v1"));
            }
            else
            {
            }

            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseCors("AnyAccess");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PreparingDatabase.PrepSeedData(app, env.IsProduction());
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration, bool isProd)
        {
            var connectionString = configuration.GetConnectionString("Faculty");
            services.AddDbContext<DatabaseContextEntityFramework>(option => option.UseSqlServer(connectionString, b => b.MigrationsAssembly("Faculty.ResourceServer")));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Student>, BaseRepositoryEntityFramework<Student>>();
            services.AddScoped<IRepository<Curator>, BaseRepositoryEntityFramework<Curator>>();
            services.AddScoped<IRepository<Specialization>, BaseRepositoryEntityFramework<Specialization>>();
            services.AddScoped<IRepositoryGroup, RepositoryEntityFrameworkGroup>();
            services.AddScoped<IRepositoryFaculty, RepositoryEntityFrameworkFaculty>();
        }

        public static void AddControllerServices(this IServiceCollection services)
        {
            services.AddScoped<ICuratorService, CuratorService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<IStudentService, StudentService>();
        }

        public static void AddMapper(this IServiceCollection services)
        {
            services.AddSingleton<AutoMapper.IConfigurationProvider>(x => new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile())));
            services.AddSingleton<IMapper, Mapper>();
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Resource server",
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
    }
}
