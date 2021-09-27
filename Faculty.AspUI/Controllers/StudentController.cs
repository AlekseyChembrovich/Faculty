using Microsoft.AspNetCore.Mvc;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentOperations _studentService;

        public StudentController(IStudentOperations studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_studentService.GetList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateStudentDto model)
        {
            _studentService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _studentService.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditStudentDto model)
        {
            _studentService.Edit(model);
            return RedirectToAction("Index");
        }
    }
}
