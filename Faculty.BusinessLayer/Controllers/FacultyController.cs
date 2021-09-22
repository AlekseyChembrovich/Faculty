using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    [Controller]
    public class FacultyController : Controller
    {
        private readonly IRepository<DataAccessLayer.Models.Faculty> _repository;
        private readonly DatabaseContextEntityFramework _context;

        public FacultyController(DatabaseContextEntityFramework context)
        {
            _repository = new BaseRepositoryEntityFramework<DataAccessLayer.Models.Faculty>(context);
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var faculties = _repository.GetAll().ToList();
            IRepository<Student> repositoryStudent = new BaseRepositoryEntityFramework<Student>(_context);
            IRepository<Curator> repositoryCurator = new BaseRepositoryEntityFramework<Curator>(_context);
            IRepository<Group> repositoryGroup = new BaseRepositoryEntityFramework<Group>(_context);
            foreach (var faculty in faculties)
            {
                faculty.Student = repositoryStudent.GetById(faculty.StudentId);
                faculty.Curator = repositoryCurator.GetById(faculty.CuratorId);
                faculty.Group = repositoryGroup.GetById(faculty.GroupId);
            }
            return View(_repository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            IRepository<Student> repositoryStudent = new BaseRepositoryEntityFramework<Student>(_context);
            IRepository<Curator> repositoryCurator = new BaseRepositoryEntityFramework<Curator>(_context);
            IRepository<Group> repositoryGroup = new BaseRepositoryEntityFramework<Group>(_context);
            ViewBag.Students = new SelectList(repositoryStudent.GetAll(), "Id", "Surname");
            ViewBag.Curators = new SelectList(repositoryCurator.GetAll(), "Id", "Surname");
            ViewBag.Groups = new SelectList(repositoryGroup.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(DataAccessLayer.Models.Faculty faculty)
        {
            _repository.Insert(faculty);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var facultyToDelete = _repository.GetById(id);
            _repository.Delete(facultyToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IRepository<Student> repositoryStudent = new BaseRepositoryEntityFramework<Student>(_context);
            IRepository<Curator> repositoryCurator = new BaseRepositoryEntityFramework<Curator>(_context);
            IRepository<Group> repositoryGroup = new BaseRepositoryEntityFramework<Group>(_context);
            ViewBag.Students = new SelectList(repositoryStudent.GetAll(), "Id", "Surname");
            ViewBag.Curators = new SelectList(repositoryCurator.GetAll(), "Id", "Surname");
            ViewBag.Groups = new SelectList(repositoryGroup.GetAll(), "Id", "Name");
            var facultyToEdit = _repository.GetById(id);
            return View(facultyToEdit);
        }

        [HttpPost]
        public IActionResult Edit(DataAccessLayer.Models.Faculty faculty)
        {
            _repository.Update(faculty);
            return RedirectToAction("Index");
        }
    }
}
