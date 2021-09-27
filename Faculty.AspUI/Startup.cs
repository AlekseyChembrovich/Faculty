using System.Globalization;
using Faculty.DataAccessLayer;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Faculty.AspUI.Middleware.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

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

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            var connectionString = Configuration.GetSection("ConnectionString").GetValue(typeof(string), "ConStr").ToString();
            services.AddDbContext<DatabaseContextEntityFramework>(option => option.UseSqlServer(connectionString));
            
            services.AddScoped<IRepository<Student>, BaseRepositoryEntityFramework<Student>>();
            services.AddScoped<IRepository<Curator>, BaseRepositoryEntityFramework<Curator>>();
            services.AddScoped<IRepository<Specialization>, BaseRepositoryEntityFramework<Specialization>>();
            services.AddScoped<IRepositoryGroup, RepositoryEntityFrameworkGroup>();
            services.AddScoped<IRepositoryFaculty, RepositoryEntityFrameworkFaculty>();

            services.AddScoped<IStudentOperations, StudentService>();
            services.AddScoped<ICuratorOperations, CuratorService>();
            services.AddScoped<ISpecializationOperations, SpecializationService>();
            services.AddScoped<IGroupOperations, GroupService>();
            services.AddScoped<IFacultyOperations, FacultyService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<LocalizerMiddleware>();

            app.UseRequestLocalization(localizationOptions.Value);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(null, "{controller=Faculty}/{action=Index}");
            });
        }
    }
}
