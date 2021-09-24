using Microsoft.AspNetCore.Mvc;
using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    public class CuratorController : Controller
    {
        private readonly ICuratorService _curatorService;

        public CuratorController(ICuratorService curatorService)
        {
            _curatorService = curatorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_curatorService.GetList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CuratorDTO model)
        {
            _curatorService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _curatorService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _curatorService.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CuratorDTO model)
        {
            _curatorService.Edit(model);
            return RedirectToAction("Index");
        }
    }
}
