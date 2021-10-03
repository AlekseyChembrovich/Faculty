﻿using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.BusinessLayer.Dto.Faculty;

namespace Faculty.AspUI.Controllers
{
    [Controller]
    public class FacultyController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ICuratorService _curatorService;
        private readonly IMapper _mapper;

        public FacultyController(IFacultyService facultyService, IGroupService groupService, IStudentService studentService, ICuratorService curatorService, IMapper mapper)
        {
            _facultyService = facultyService;
            _groupService = groupService;
            _studentService = studentService;
            _curatorService = curatorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(int? valueFilter = null)
        {
            var modelsDto = _facultyService.GetAll();
            var models = _mapper.Map<IEnumerable<FacultyDisplayDto>, IEnumerable<FacultyDisplay>>(modelsDto);
            var modelsFilter = valueFilter != null ? models.ToList().Where(x => x.CountYearEducation == valueFilter.Value).ToList() : models.ToList();
            return View(modelsFilter);
        }

        [HttpGet]
        public IActionResult Create()
        {
            FillViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Create(FacultyAdd model)
        {
            FillViewBag();
            var modelDto = _mapper.Map<FacultyAdd, FacultyAddDto>(model);
            _facultyService.Create(modelDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _facultyService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(FacultyModify model)
        {
            _facultyService.Delete(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var modelDto = _facultyService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<FacultyModifyDto, FacultyModify>(modelDto);
            FillViewBag();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(FacultyModify model)
        {
            FillViewBag();
            var modelDto = _mapper.Map<FacultyModify, FacultyModifyDto>(model);
            _facultyService.Edit(modelDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Confirm(int id, string actionName)
        {
            FillViewBag();
            ViewBag.RefererActionName = actionName;
            var model = _mapper.Map<FacultyModifyDto, FacultyModify>(_facultyService.GetById(id));
            return View(model);
        }

        [HttpPost]
        public IActionResult Confirm(FacultyModify model, string actionName)
        {
            FillViewBag();
            ViewBag.RefererActionName = actionName;
            return ModelState.IsValid == false ? View(actionName, model) : View(model);
        }

        public void FillViewBag()
        {
            ViewBag.Groups = _groupService.GetAll();
            ViewBag.Students = _studentService.GetAll();
            ViewBag.Curators = _curatorService.GetAll();
        }
    }
}
