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
        private readonly IRepository<Group> _repository;
        private readonly DatabaseContextEntityFramework _context;

        public GroupController(DatabaseContextEntityFramework context)
        {
            _repository = new BaseRepositoryEntityFramework<Group>(context);
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var groups = _repository.GetAll().ToList();
            IRepository<Specialization> repositorySpecialization = new BaseRepositoryEntityFramework<Specialization>(_context);
            foreach (var group in groups)
            {
                group.Specialization = repositorySpecialization.GetById(group.SpecializationId);
            }
            return View(groups);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IRepository<Specialization> repositorySpecialization = new BaseRepositoryEntityFramework<Specialization>(_context);
            ViewBag.Groups = new SelectList(repositorySpecialization.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group group)
        {
            _repository.Insert(group);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var groupToDelete = _repository.GetById(id);
            _repository.Delete(groupToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IRepository<Specialization> repositorySpecialization = new BaseRepositoryEntityFramework<Specialization>(_context);
            ViewBag.Groups = new SelectList(repositorySpecialization.GetAll(), "Id", "Name");
            var groupToEdit = _repository.GetById(id);
            return View(groupToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Group group)
        {
            _repository.Update(group);
            return RedirectToAction("Index");
        }
    }
}
