using AutoMapper;
using System.Linq;
using Faculty.AspUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.StudentDto;

namespace Faculty.AspUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentOperations _studentService;

        public StudentController(IStudentOperations studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var modelsDto = _studentService.GetList();
            Mapper.Initialize(cfg => cfg.CreateMap<DisplayStudentDto, StudentDisplay>());
            var models = Mapper.Map<IEnumerable<DisplayStudentDto>, IEnumerable<StudentDisplay>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<StudentModify, CreateStudentDto>());
            var createCurator = Mapper.Map<StudentModify, CreateStudentDto>(model);
            _studentService.Create(createCurator);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null) _studentService.Delete(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            EditStudentDto modelDto = default;
            if (id is not null) modelDto = _studentService.GetModel(id.Value);
            if (modelDto is null) return RedirectToAction("Index");
            Mapper.Initialize(cfg => cfg.CreateMap<EditStudentDto, StudentModify>());
            var model = Mapper.Map<EditStudentDto, StudentModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            Mapper.Initialize(cfg => cfg.CreateMap<StudentModify, EditStudentDto>());
            var modelDto = Mapper.Map<StudentModify, EditStudentDto>(model);
            _studentService.Edit(modelDto);
            return RedirectToAction("Index");
        }
    }
}
