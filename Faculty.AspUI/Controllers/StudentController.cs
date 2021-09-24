using Microsoft.AspNetCore.Mvc;
using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
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
        public IActionResult Create(StudentDTO model)
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
        public IActionResult Edit(StudentDTO model)
        {
            _studentService.Edit(model);
            return RedirectToAction("Index");
        }
    }
}
