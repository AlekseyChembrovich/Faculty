using System;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.BusinessLayer.Dto.Faculty;
using Microsoft.AspNetCore.Localization;

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
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<FacultyAdd, FacultyDto>(model);
            _facultyService.Create(modelDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var modelDto = _facultyService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<FacultyDto, FacultyModify>(modelDto);
            FillViewBag();
            return View(model);
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
            var model = _mapper.Map<FacultyDto, FacultyModify>(modelDto);
            FillViewBag();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(FacultyModify model)
        {
            FillViewBag();
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<FacultyModify, FacultyDto>(model);
            _facultyService.Edit(modelDto);
            return RedirectToAction("Index");
        }

        public void FillViewBag()
        {
            ViewBag.Groups = _groupService.GetAll();
            ViewBag.Students = _studentService.GetAll();
            ViewBag.Curators = _curatorService.GetAll();
        }

        [HttpPost]
        public IActionResult Localize(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) } );
            return LocalRedirect(returnUrl);
        }
    }
}
