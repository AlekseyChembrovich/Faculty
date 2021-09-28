using AutoMapper;
using System.Linq;
using Faculty.AspUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.GroupDto;

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
            var modelsDto = _groupService.GetList();
            Mapper.Initialize(cfg => cfg.CreateMap<DisplayGroupDto, GroupDisplay>());
            var models = Mapper.Map<IEnumerable<DisplayGroupDto>, IEnumerable<GroupDisplay>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ViewModelGroup = _groupService.CreateViewModelGroup();
            return View();
        }

        [HttpPost]
        public IActionResult Create(GroupModify model)
        {
            ViewBag.ViewModelGroup = _groupService.CreateViewModelGroup();
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<GroupModify, CreateGroupDto>());
            var createCurator = Mapper.Map<GroupModify, CreateGroupDto>(model);
            _groupService.Create(createCurator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null) _groupService.Delete(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            EditGroupDto modelDto = default;
            if (id is not null) modelDto = _groupService.GetModel(id.Value);
            if (modelDto is null) return RedirectToAction("Index");
            Mapper.Initialize(cfg => cfg.CreateMap<EditGroupDto, GroupModify>());
            var model = Mapper.Map<EditGroupDto, GroupModify>(modelDto);
            ViewBag.ViewModelGroup = _groupService.CreateViewModelGroup();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(GroupModify model)
        {
            ViewBag.ViewModelGroup = _groupService.CreateViewModelGroup();
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<GroupModify, EditGroupDto>());
            var modelDto = Mapper.Map<GroupModify, EditGroupDto>(model);
            _groupService.Edit(modelDto);
            return RedirectToAction("Index");
        }
    }
}
