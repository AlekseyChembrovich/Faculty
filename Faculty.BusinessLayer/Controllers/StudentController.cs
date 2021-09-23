using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;

namespace Faculty.BusinessLayer.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _repositoryStudent;

        public StudentController(IRepository<Student> repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repositoryStudent.GetAll().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _repositoryStudent.Insert(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var studentToDelete = _repositoryStudent.GetById(id);
            _repositoryStudent.Delete(studentToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var studentToEdit = _repositoryStudent.GetById(id);
            return View(studentToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _repositoryStudent.Update(student);
            return RedirectToAction("Index");
        }
    }
}
