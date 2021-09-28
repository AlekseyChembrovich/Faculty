using AutoMapper;
using System.Linq;
using Faculty.AspUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.SpecializationDto;

namespace Faculty.AspUI.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly ISpecializationOperations _specializationService;

        public SpecializationController(ISpecializationOperations specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var modelsDto = _specializationService.GetList();
            Mapper.Initialize(cfg => cfg.CreateMap<DisplaySpecializationDto, SpecializationDisplay>());
            var models = Mapper.Map<IEnumerable<DisplaySpecializationDto>, IEnumerable<SpecializationDisplay>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SpecializationModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationModify, CreateSpecializationDto>());
            var createCurator = Mapper.Map<SpecializationModify, CreateSpecializationDto>(model);
            _specializationService.Create(createCurator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null) _specializationService.Delete(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            EditSpecializationDto modelDto = default;
            if (id is not null) modelDto = _specializationService.GetModel(id.Value);
            if (modelDto is null) return RedirectToAction("Index");
            Mapper.Initialize(cfg => cfg.CreateMap<EditSpecializationDto, SpecializationModify>());
            var model = Mapper.Map<EditSpecializationDto, SpecializationModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SpecializationModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationModify, EditSpecializationDto>());
            var modelDto = Mapper.Map<SpecializationModify, EditSpecializationDto>(model);
            _specializationService.Edit(modelDto);
            return RedirectToAction("Index");
        }
    }
}
