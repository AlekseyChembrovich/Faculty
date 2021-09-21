using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class SpecializationController : Controller
    {
        public IActionResult Index()
        {
            var context = new DatabaseContextEntityFramework();
            IRepository<Specialization> repository = new BaseRepositoryEntityFramework<Specialization>(context);
            return View(repository.GetAll());
        }
    }
}
