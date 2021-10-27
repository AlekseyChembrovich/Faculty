using System;
using System.Net;
using AutoMapper;
using System.Globalization;
using System.Threading.Tasks;
using Faculty.AspUI.Services;
using Microsoft.AspNetCore.Http;
using Faculty.AspUI.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Microsoft.IdentityModel.Tokens;
using Faculty.BusinessLayer.Interfaces;
using Faculty.AspUI.HttpMessageHandlers;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            services.AddControllersWithViews().AddDataAnnotationsLocalization().AddViewLocalization();
            services.AddAuthenticationWithJwtToken(Configuration);
            services.AddDatabaseContext(Configuration);
            services.AddHttpContextAccessor();
            services.AddRequestLocalization();
            services.AddControllerServices();
            services.AddUsersHttpClients(Configuration);
            services.AddRepositories();
            services.AddMapper();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(x => x.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseRedirectionForAuthorizationErrors(Configuration);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization(localizationOptions.Value);
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(null, "{controller=Faculty}/{action=Index}/{id?}");
            });
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void AddRequestLocalization(this IServiceCollection services)
        {
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

            services.AddScoped<IStringLocalizer, ServerErrorLocalizer>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
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
        }

        public static void AddMapper(this IServiceCollection services)
        {
            services.AddSingleton<AutoMapper.IConfigurationProvider>(x => new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile())));
            services.AddSingleton<IMapper, Mapper>();
        }

        public static void AddAuthenticationWithJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            var authOption = new AuthOptions(configuration);
            services.AddSingleton(options => new AuthOptions(configuration));
            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authOption?.Issuer,
                ValidateAudience = true,
                ValidAudience = authOption?.Audience,
                IssuerSigningKey = authOption?.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(
                    options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = validationParams;
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = (context) =>
                            {
                                var token = context.HttpContext.Request.Cookies["access_token"];
                                if (string.IsNullOrEmpty(token) == false)
                                {
                                    context.Token = token;
                                }

                                return Task.CompletedTask;
                            }
                        };
                    }
                );
        }

        public static void AddUsersHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("Url").GetValue(typeof(string), "UsersHttpClient").ToString();
            services.AddTransient<AuthMessageHandler>();
            services.AddHttpClient("UsersHttpClient", client =>
            {
                client.BaseAddress = new Uri(url ?? string.Empty);
            }).AddHttpMessageHandler<AuthMessageHandler>();

            services.AddScoped<UserService>();
        }
    }

    public static class ApplicationBuilderExtension
    {
        public static void UseRedirectionForAuthorizationErrors(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;
                switch (response.StatusCode)
                {
                    case (int)HttpStatusCode.Unauthorized or (int)HttpStatusCode.Forbidden:
                    {
                        var urlLogin = configuration.GetSection("Url").GetValue(typeof(string), "Login").ToString();
                        response.Redirect(urlLogin ?? string.Empty);
                    }
                        break;
                    case >= 400:
                    {
                        var urlRedirect = configuration.GetSection("RedirectError").GetValue(typeof(string), "RedirectError").ToString();
                        response.Redirect(urlRedirect ?? string.Empty);
                    }
                        break;
                }

                return Task.CompletedTask;
            });
        }
    }
}
