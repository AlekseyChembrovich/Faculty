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
            if (ModelState.IsValid == false) return View(model);
            var createCurator = _mapper.Map<GroupAdd, GroupAddDto>(model);
            _groupService.Create(createCurator);
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
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<GroupModify, GroupModifyDto>(model);
            _groupService.Edit(modelDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Confirm(GroupModify model)
        {
            var referer = Request.Headers["referer"].ToString();
            ViewBag.RefererActionName = GetNameActionRefererUrl(referer);
            FillViewBag();
            return View(model);
        }

        [HttpGet]
        public IActionResult Confirm(int id)
        {
            var referer = Request.Headers["referer"].ToString();
            ViewBag.RefererActionName = "Delete";
            var modelDto = _groupService.GetById(id);
            var model = _mapper.Map<GroupModifyDto, GroupModify>(modelDto);
            FillViewBag();
            return View(model);
        }

        public string GetNameActionRefererUrl(string referer)
        {
            var valuesUrlReferer = referer.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var actionNameReferer = valuesUrlReferer[^1];
            if (valuesUrlReferer[^1].Contains("?"))
            {
                actionNameReferer = actionNameReferer[..actionNameReferer.IndexOf('?')];
            }

            return actionNameReferer;
        }

        public void FillViewBag()
        {
            ViewBag.Specializations = _specializationService.GetAll();
        }
    }
}
