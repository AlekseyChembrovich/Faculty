using Faculty.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Services.Extensions
{
    public static class ServiceRepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            DbContext context = new DatabaseContextEntityFramework();
            services.AddScoped<IRepository<Student>>(x => new BaseRepositoryEntityFramework<Student>(context));
            services.AddScoped<IRepository<Curator>>(x => new BaseRepositoryEntityFramework<Curator>(context));
            services.AddScoped<IRepository<Specialization>>(x => new BaseRepositoryEntityFramework<Specialization>(context));
            services.AddScoped<IRepositoryGroup>(x => new RepositoryEntityFrameworkGroup(context));
            services.AddScoped<IRepositoryFaculty>(x => new RepositoryEntityFrameworkFaculty(context));
        }
    }
}
