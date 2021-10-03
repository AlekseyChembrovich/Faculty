using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Group;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.AspUI.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ISpecializationService _specializationService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, ISpecializationService specializationService, IMapper mapper)
        {
            _groupService = groupService;
            _specializationService = specializationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var modelsDto = _groupService.GetAll();
            var models = _mapper.Map<IEnumerable<GroupDisplayDto>, IEnumerable<GroupDisplay>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            FillViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Create(GroupAdd model)
        {
            FillViewBag();
            var modelDto = _mapper.Map<GroupAdd, GroupAddDto>(model);
            _groupService.Create(modelDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _groupService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(GroupModify model)
        {
            _groupService.Delete(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var modelDto = _groupService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<GroupModifyDto, GroupModify>(modelDto);
            FillViewBag();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(GroupModify model)
        {
            FillViewBag();
            var modelDto = _mapper.Map<GroupModify, GroupModifyDto>(model);
            _groupService.Edit(modelDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Confirm(int id, string actionName)
        {
            FillViewBag();
            ViewBag.RefererActionName = actionName;
            var model = _mapper.Map<GroupModifyDto, GroupModify>(_groupService.GetById(id));
            return View(model);
        }

        [HttpPost]
        public IActionResult Confirm(GroupModify model, string actionName)
        {
            FillViewBag();
            ViewBag.RefererActionName = actionName;
            return ModelState.IsValid == false ? View(actionName, model) : View(model);
        }

        public void FillViewBag()
        {
            ViewBag.Specializations = _specializationService.GetAll();
        }
    }
}
