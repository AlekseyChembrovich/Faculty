using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    [Controller]
    public class FacultyController : Controller
    {
        private readonly IFacultyOperations _facultyService;

        public FacultyController(IFacultyOperations facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public IActionResult Index(string valueFilter = null)
        {
            var faculties = _facultyService.GetList();
            var facultiesFilter = valueFilter != null ? faculties.Where(x => x.CountYearEducation.ToString().Contains(valueFilter)).ToList() : faculties;
            return View(facultiesFilter);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ViewModelFaculty = _facultyService.CreateViewModelFaculty();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateFacultyDto model)
        {
            _facultyService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _facultyService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ViewModelFaculty = _facultyService.CreateViewModelFaculty();
            var model = _facultyService.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditFacultyDto model)
        {
            _facultyService.Edit(model);
            return RedirectToAction("Index");
        }
    }
}
