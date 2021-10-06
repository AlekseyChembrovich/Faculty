using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Curator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.AspUI.Controllers
{
    public class CuratorController : Controller
    {
        private readonly ICuratorService _curatorService;
        private readonly IMapper _mapper;

        public CuratorController(ICuratorService curatorService, IMapper mapper)
        {
            _curatorService = curatorService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var modelsDto = _curatorService.GetAll();
            var models = _mapper.Map<IEnumerable<CuratorDisplayModifyDto>, IEnumerable<CuratorDisplayModify>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public IActionResult Create(CuratorAdd model)
        {
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<CuratorAdd, CuratorAddDto>(model);
            _curatorService.Create(modelDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public IActionResult Delete(int id)
        {
            var modelDto = _curatorService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<CuratorDisplayModifyDto, CuratorDisplayModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public IActionResult Delete(CuratorDisplayModify model)
        {
            _curatorService.Delete(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public IActionResult Edit(int id)
        {
            var modelDto = _curatorService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<CuratorDisplayModifyDto, CuratorDisplayModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
        public IActionResult Edit(CuratorDisplayModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<CuratorDisplayModify, CuratorDisplayModifyDto>(model);
            _curatorService.Edit(modelDto);
            return RedirectToAction("Index");
        }
    }
}
