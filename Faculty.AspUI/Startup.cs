using System;
using System.Net;
using AutoMapper;
using System.Globalization;
using System.Threading.Tasks;
using Faculty.AspUI.Services;
using Faculty.AspUI.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Microsoft.IdentityModel.Tokens;
using Faculty.AspUI.HttpMessageHandler;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
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
            services.AddUsersHttpClients();
            services.AddRepositories();
            services.AddMapper();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseCors(x => x.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies["access_token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode is (int)HttpStatusCode.Unauthorized or (int)HttpStatusCode.Forbidden)
                {
                    response.Redirect("/Home/Login");
                }
                else if (response.StatusCode >= 400)
                {
                    response.Redirect("/Home/Error");
                }
                return Task.CompletedTask;
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization(localizationOptions.Value);
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
            var connectionString = configuration.GetSection("ConnectionString").GetValue(typeof(string), "ConStr").ToString();
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

        public static void AddUsersHttpClients(this IServiceCollection services)
        {
            services.AddTransient<AuthMessageHandler>();
            services.AddHttpClient("usersHttpClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44342/");
            }).AddHttpMessageHandler<AuthMessageHandler>();

            services.AddScoped<UserService>();
        }
    }
}
