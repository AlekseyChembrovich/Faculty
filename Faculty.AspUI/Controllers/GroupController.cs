using Microsoft.AspNetCore.Mvc;
using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
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
        public IActionResult Create(GroupDTO model)
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
        public IActionResult Edit(GroupDTO group)
        {
            _groupService.Edit(group);
            return RedirectToAction("Index");
        }
    }
}
