using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class CuratorController : Controller
    {
        public IActionResult Index()
        {
            var context = new DatabaseContextEntityFramework();
            IRepository<Curator> repository = new BaseRepositoryEntityFramework<Curator>(context);
            return View(repository.GetAll());
        }
    }
}
