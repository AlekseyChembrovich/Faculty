using Microsoft.AspNetCore.Mvc;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly ISpecializationOperations _specializationService;

        public SpecializationController(ISpecializationOperations specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_specializationService.GetList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateSpecializationDto model)
        {
            _specializationService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _specializationService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _specializationService.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditSpecializationDto model)
        {
            _specializationService.Edit(model);
            return RedirectToAction("Index");
        }
    }
}
