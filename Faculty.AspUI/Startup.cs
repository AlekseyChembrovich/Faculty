using AutoMapper;
using System.Globalization;
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
using Faculty.DataAccessLayer.Repository;
using Faculty.AspUI.Middleware.Implementation;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<AutoMapper.IConfigurationProvider>(x => new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile())));
            services.AddSingleton<IMapper, Mapper>();

            services.AddScoped<IRepository<Student>, BaseRepositoryEntityFramework<Student>>();
            services.AddScoped<IRepository<Curator>, BaseRepositoryEntityFramework<Curator>>();
            services.AddScoped<IRepository<Specialization>, BaseRepositoryEntityFramework<Specialization>>();
            services.AddScoped<IRepositoryGroup, RepositoryEntityFrameworkGroup>();
            services.AddScoped<IRepositoryFaculty, RepositoryEntityFrameworkFaculty>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICuratorService, CuratorService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IFacultyService, FacultyService>();
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
