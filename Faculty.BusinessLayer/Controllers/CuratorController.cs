using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;

namespace Faculty.BusinessLayer.Controllers
{
    public class CuratorController : Controller
    {
        private readonly IRepository<Curator> _repositoryCurator;

        public CuratorController(IRepository<Curator> repositoryCurator)
        {
            _repositoryCurator = repositoryCurator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repositoryCurator.GetAll().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Curator curator)
        {
            _repositoryCurator.Insert(curator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var curatorToDelete = _repositoryCurator.GetById(id);
            _repositoryCurator.Delete(curatorToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var curatorToEdit = _repositoryCurator.GetById(id);
            return View(curatorToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Curator curator)
        {
            _repositoryCurator.Update(curator);
            return RedirectToAction("Index");
        }
    }
}
