using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Student> _repository;

        public StudentController(DatabaseContextEntityFramework context)
        {
            _repository = new BaseRepositoryEntityFramework<Student>(context);
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
        public IActionResult Create(Student student)
        {
            _repository.Insert(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var studentToDelete = _repository.GetById(id);
            _repository.Delete(studentToDelete);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var studentToEdit = _repository.GetById(id);
            return View(studentToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _repository.Update(student);
            return RedirectToAction("Index");
        }
    }
}
