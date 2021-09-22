using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class CuratorController : Controller
    {
        private readonly IRepository<Curator> _repository;

        public CuratorController(DatabaseContextEntityFramework context)
        {
            _repository = new BaseRepositoryEntityFramework<Curator>(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Curator curator)
        {
            _repository.Insert(curator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var curatorToDelete = _repository.GetById(id);
            _repository.Delete(curatorToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var curatorToEdit = _repository.GetById(id);
            return View(curatorToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Curator curator)
        {
            _repository.Update(curator);
            return RedirectToAction("Index");
        }
    }
}
