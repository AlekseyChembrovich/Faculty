using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;

namespace Faculty.BusinessLayer.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly IRepository<Specialization> _repositorySpecialization;

        public SpecializationController(IRepository<Specialization> repositorySpecialization)
        {
            _repositorySpecialization = repositorySpecialization;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repositorySpecialization.GetAll().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Specialization specialization)
        {
            _repositorySpecialization.Insert(specialization);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var specializationToDelete = _repositorySpecialization.GetById(id);
            _repositorySpecialization.Delete(specializationToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var specializationToEdit = _repositorySpecialization.GetById(id);
            return View(specializationToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Specialization specialization)
        {
            _repositorySpecialization.Update(specialization);
            return RedirectToAction("Index");
        }
    }
}
