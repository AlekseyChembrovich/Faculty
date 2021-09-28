using AutoMapper;
using System.Linq;
using Faculty.AspUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.CuratorDto;

namespace Faculty.AspUI.Controllers
{
    public class CuratorController : Controller
    {
        private readonly ICuratorOperations _curatorService;

        public CuratorController(ICuratorOperations curatorService)
        {
            _curatorService = curatorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var modelsDto = _curatorService.GetList();
            Mapper.Initialize(cfg => cfg.CreateMap<DisplayCuratorDto, CuratorDisplay>());
            var models = Mapper.Map<IEnumerable<DisplayCuratorDto>, IEnumerable<CuratorDisplay>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CuratorModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorModify, CreateCuratorDto>());
            var createCurator = Mapper.Map<CuratorModify, CreateCuratorDto>(model);
            _curatorService.Create(createCurator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null) _curatorService.Delete(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            EditCuratorDto modelDto = default;
            if (id is not null) modelDto = _curatorService.GetModel(id.Value);
            if (modelDto is null) return RedirectToAction("Index");
            Mapper.Initialize(cfg => cfg.CreateMap<EditCuratorDto, CuratorModify>());
            var model = Mapper.Map<EditCuratorDto, CuratorModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CuratorModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorModify, EditCuratorDto>());
            var modelDto = Mapper.Map<CuratorModify, EditCuratorDto>(model);
            _curatorService.Edit(modelDto);
            return RedirectToAction("Index");
        }
    }
}
