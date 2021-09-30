using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.AspUI.ViewModels.Specialization;
using Faculty.BusinessLayer.Dto.Specialization;

namespace Faculty.AspUI.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;
        private readonly IMapper _mapper;

        public SpecializationController(ISpecializationService specializationService, IMapper mapper)
        {
            _specializationService = specializationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var modelsDto = _specializationService.GetAll();
            var models = _mapper.Map<IEnumerable<SpecializationDisplayModifyDto>, IEnumerable<SpecializationDisplayModify>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SpecializationAdd model)
        {
            if (ModelState.IsValid == false) return View(model);
            var createCurator = _mapper.Map<SpecializationAdd, SpecializationAddDto>(model);
            _specializationService.Create(createCurator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _specializationService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(SpecializationDisplayModify model)
        {
            _specializationService.Delete(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var modelDto = _specializationService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<SpecializationDisplayModifyDto, SpecializationDisplayModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SpecializationDisplayModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<SpecializationDisplayModify, SpecializationDisplayModifyDto>(model);
            _specializationService.Edit(modelDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Confirm(SpecializationDisplayModify model)
        {
            var referer = Request.Headers["referer"].ToString();
            ViewBag.RefererActionName = GetNameActionRefererUrl(referer);
            return View(model);
        }

        [HttpGet]
        public IActionResult Confirm(int id)
        {
            var referer = Request.Headers["referer"].ToString();
            ViewBag.RefererActionName = "Delete";
            var modelDto = _specializationService.GetById(id);
            var model = _mapper.Map<SpecializationDisplayModifyDto, SpecializationDisplayModify>(modelDto);
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
    }
}
