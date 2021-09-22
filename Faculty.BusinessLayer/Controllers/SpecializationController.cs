using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly IRepository<Specialization> _repository;

        public SpecializationController(DatabaseContextEntityFramework context)
        {
            _repository = new BaseRepositoryEntityFramework<Specialization>(context);
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
        public IActionResult Create(Specialization specialization)
        {
            _repository.Insert(specialization);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var specializationToDelete = _repository.GetById(id);
            _repository.Delete(specializationToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var specializationToEdit = _repository.GetById(id);
            return View(specializationToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Specialization specialization)
        {
            _repository.Update(specialization);
            return RedirectToAction("Index");
        }
    }
}
