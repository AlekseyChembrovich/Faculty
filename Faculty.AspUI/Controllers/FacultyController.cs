using AutoMapper;
using System.Linq;
using Faculty.AspUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.FacultyDto;

namespace Faculty.AspUI.Controllers
{
    [Controller]
    public class FacultyController : Controller
    {
        private readonly IFacultyOperations _facultyService;

        public FacultyController(IFacultyOperations facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public IActionResult Index(int? valueFilter = null)
        {
            var modelsDto = _facultyService.GetList();
            Mapper.Initialize(cfg => cfg.CreateMap<DisplayFacultyDto, FacultyDisplay>());
            var models = Mapper.Map<IEnumerable<DisplayFacultyDto>, IEnumerable<FacultyDisplay>>(modelsDto);
            var modelsFilter = valueFilter != null ? models.ToList().Where(x => x.CountYearEducation == valueFilter.Value).ToList() : models.ToList();
            return View(modelsFilter);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ViewModelFaculty = _facultyService.CreateViewModelFaculty();
            return View();
        }

        [HttpPost]
        public IActionResult Create(FacultyModify model)
        {
            ViewBag.ViewModelFaculty = _facultyService.CreateViewModelFaculty();
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyModify, CreateFacultyDto>());
            var createCurator = Mapper.Map<FacultyModify, CreateFacultyDto>(model);
            _facultyService.Create(createCurator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null) _facultyService.Delete(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            EditFacultyDto modelDto = default;
            if (id is not null) modelDto = _facultyService.GetModel(id.Value);
            if (modelDto is null) return RedirectToAction("Index");
            Mapper.Initialize(cfg => cfg.CreateMap<EditFacultyDto, FacultyModify>());
            var model = Mapper.Map<EditFacultyDto, FacultyModify>(modelDto);
            ViewBag.ViewModelFaculty = _facultyService.CreateViewModelFaculty();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(FacultyModify model)
        {
            ViewBag.ViewModelFaculty = _facultyService.CreateViewModelFaculty();
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyModify, EditFacultyDto>());
            var modelDto = Mapper.Map<FacultyModify, EditFacultyDto>(model);
            _facultyService.Edit(modelDto);
            return RedirectToAction("Index");
        }
    }
}
