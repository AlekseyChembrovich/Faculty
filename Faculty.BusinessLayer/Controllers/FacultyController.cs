using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    [Controller]
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            var context = new DatabaseContextEntityFramework();
            IRepository<DataAccessLayer.Models.Faculty> repository = new BaseRepositoryEntityFramework<DataAccessLayer.Models.Faculty>(context);
            return View(repository.GetAll());
        }
    }
}
