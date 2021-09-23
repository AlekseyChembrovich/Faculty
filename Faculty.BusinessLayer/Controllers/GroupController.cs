using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class GroupController : Controller
    {
        private readonly IRepository<Group> _repositoryGroup;
        private readonly IRepository<Specialization> _repositorySpecialization;

        public GroupController(DbContext context)
        {
            _repositoryGroup = new BaseRepositoryEntityFramework<Group>(context);
            _repositorySpecialization = new BaseRepositoryEntityFramework<Specialization>(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var groups = _repositoryGroup.GetAll().ToList();
            foreach (var group in groups)
            {
                group.Specialization = _repositorySpecialization.GetById(group.SpecializationId);
            }

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
