using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    [Controller]
    public class FacultyController : Controller
    {
        private readonly IRepository<DataAccessLayer.Models.Faculty> _repositoryFaculty;
        private readonly IRepository<Student> _repositoryStudent;
        private readonly IRepository<Curator> _repositoryCurator;
        private readonly IRepository<Group> _repositoryGroup;

        public FacultyController(DbContext context)
        {
            _repositoryFaculty = new BaseRepositoryEntityFramework<DataAccessLayer.Models.Faculty>(context);
            _repositoryStudent = new BaseRepositoryEntityFramework<Student>(context);
            _repositoryCurator = new BaseRepositoryEntityFramework<Curator>(context);
            _repositoryGroup = new BaseRepositoryEntityFramework<Group>(context);
        }

        [HttpGet]
        public IActionResult Index(string valueFilter = null)
        {
            var faculties = _repositoryFaculty.GetAll().ToList();
            foreach (var faculty in faculties)
            {
                faculty.Student = _repositoryStudent.GetById(faculty.StudentId);
                faculty.Curator = _repositoryCurator.GetById(faculty.CuratorId);
                faculty.Group = _repositoryGroup.GetById(faculty.GroupId);
            }

            var facultiesFilter = valueFilter != null ? faculties.Where(x => x.CountYearEducation.ToString().Contains(valueFilter)).ToList() : faculties;
            return View(facultiesFilter);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(_repositoryStudent.GetAll(), "Id", "Surname");
            ViewBag.Curators = new SelectList(_repositoryCurator.GetAll(), "Id", "Surname");
            ViewBag.Groups = new SelectList(_repositoryGroup.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(DataAccessLayer.Models.Faculty faculty)
        {
            _repositoryFaculty.Insert(faculty);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var facultyToDelete = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(facultyToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Students = new SelectList(_repositoryStudent.GetAll(), "Id", "Surname");
            ViewBag.Curators = new SelectList(_repositoryCurator.GetAll(), "Id", "Surname");
            ViewBag.Groups = new SelectList(_repositoryGroup.GetAll(), "Id", "Name");
            var facultyToEdit = _repositoryFaculty.GetById(id);
            return View(facultyToEdit);
        }

        [HttpPost]
        public IActionResult Edit(DataAccessLayer.Models.Faculty faculty)
        {
            _repositoryFaculty.Update(faculty);
            return RedirectToAction("Index");
        }
    }
}
