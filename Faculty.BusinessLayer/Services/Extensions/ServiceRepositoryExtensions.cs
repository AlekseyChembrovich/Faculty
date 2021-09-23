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
            services.AddSingleton<IRepository<Student>>(x => new BaseRepositoryEntityFramework<Student>(context));
            services.AddSingleton<IRepository<Curator>>(x => new BaseRepositoryEntityFramework<Curator>(context));
            services.AddSingleton<IRepository<Specialization>>(x => new BaseRepositoryEntityFramework<Specialization>(context));
            services.AddSingleton<IRepository<Group>>(x => new BaseRepositoryEntityFramework<Group>(context));
            services.AddSingleton<IRepository<DataAccessLayer.Models.Faculty>>(x => new BaseRepositoryEntityFramework<DataAccessLayer.Models.Faculty>(context));
        }
    }
}
