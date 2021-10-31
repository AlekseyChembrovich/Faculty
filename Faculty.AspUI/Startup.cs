using System;
using AutoMapper;
using Faculty.AspUI.Tools;
using System.Globalization;
using System.Security.Claims;
using Faculty.AspUI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Faculty.BusinessLayer.Interfaces;
using Faculty.AspUI.HttpMessageHandlers;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Faculty.DataAccessLayer.Repository.EntityFramework;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.AspUI
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
            services.AddMvc().AddDataAnnotationsLocalization().AddViewLocalization();
            services.AddDatabaseContext(Configuration);
            services.AddAuthenticationWithCookies(Configuration);
            services.AddAuthorizationWithRole();
            services.AddHttpContextAccessor();
            services.AddRequestLocalization();
            services.AddControllerServices();
            services.AddUsersHttpClients(Configuration);
            services.AddRepositories();
            services.AddMapper();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization(localizationOptions.Value);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void AddRequestLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            var cultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
        }

        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").GetValue(typeof(string), "ConStr")?.ToString() ?? string.Empty;
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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddMapper(this IServiceCollection services)
        {
            services.AddSingleton<AutoMapper.IConfigurationProvider>(x => new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile())));
            services.AddSingleton<IMapper, Mapper>();
        }

        public static void AddAuthenticationWithCookies(this IServiceCollection services, IConfiguration configuration)
        {
            var authOption = new AuthOptions(configuration);
            services.AddSingleton(options => new AuthOptions(configuration));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Home/Login");
                    options.AccessDeniedPath = new PathString("/Home/Login");
                    options.ExpireTimeSpan = TimeSpan.FromDays(authOption.Lifetime);
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

                options.AddPolicy("Common", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "administrator") || x.User.HasClaim(ClaimTypes.Role, "employee"));
                });
            });
        }

        public static void AddUsersHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AuthMessageHandler>();
            services.AddHttpClient<IAuthService, AuthService>( client =>
            {
                client.BaseAddress = new Uri(configuration["Url:AuthenticationServer"]);
            }).AddHttpMessageHandler<AuthMessageHandler>();
            services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(configuration["Url:AuthenticationServer"]);
            }).AddHttpMessageHandler<AuthMessageHandler>();
        }
    }
}
