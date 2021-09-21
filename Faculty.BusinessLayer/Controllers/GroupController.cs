using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            var context = new DatabaseContextEntityFramework();
            IRepository<Group> repository = new BaseRepositoryEntityFramework<Group>(context);
            return View(repository.GetAll());
        }
    }
}
