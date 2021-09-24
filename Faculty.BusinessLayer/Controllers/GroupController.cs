using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class GroupController : Controller
    {
        private readonly IRepositoryGroup _repositoryGroup;
        private readonly IRepository<Specialization> _repositorySpecialization;

        public GroupController(IRepositoryGroup repositoryGroup, IRepository<Specialization> repositorySpecialization)
        {
            _repositoryGroup = repositoryGroup;
            _repositorySpecialization = repositorySpecialization;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var groups = _repositoryGroup.GetAllIncludeForeignKey().ToList();
            return View(groups);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Groups = new SelectList(_repositorySpecialization.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group group)
        {
            _repositoryGroup.Insert(group);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var groupToDelete = _repositoryGroup.GetById(id);
            _repositoryGroup.Delete(groupToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Groups = new SelectList(_repositorySpecialization.GetAll(), "Id", "Name");
            var groupToEdit = _repositoryGroup.GetById(id);
            return View(groupToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Group group)
        {
            _repositoryGroup.Update(group);
            return RedirectToAction("Index");
        }
    }
}
