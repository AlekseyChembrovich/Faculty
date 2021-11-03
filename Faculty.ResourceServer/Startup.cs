using AutoMapper;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseContext(Configuration);
            services.AddRepositories();
            services.AddAuthenticationWithJwtToken(Configuration);
            services.AddAuthorizationWithRole();
            services.AddControllerServices();
            services.AddMapper();
            services.AddSwaggerConfiguration();
            services.AddControllers();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resource server v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseRouting();
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
            var connectionString = configuration["ConnectionString:Faculty"];
            services.AddDbContext<DatabaseContextEntityFramework>(option => option.UseSqlServer(connectionString));
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
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICuratorService, CuratorService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IFacultyService, FacultyService>();
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
