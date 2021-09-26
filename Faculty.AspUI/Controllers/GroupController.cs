using Microsoft.AspNetCore.Mvc;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupOperations _groupService;

        public GroupController(IGroupOperations groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_groupService.GetList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ViewModelGroup = _groupService.CreateViewModelGroup();
            return View();
        }

        [HttpPost]
        public IActionResult Create(GroupDto model)
        {
            _groupService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _groupService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ViewModelGroup = _groupService.CreateViewModelGroup();
            var model = _groupService.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(GroupDto group)
        {
            _groupService.Edit(group);
            return RedirectToAction("Index");
        }
    }
}
