using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Student;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Student;

namespace Faculty.AspUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var modelsDto = _studentService.GetAll();
            var models = _mapper.Map<IEnumerable<StudentDto>, IEnumerable<StudentDisplayModify>>(modelsDto);
            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentAdd model)
        {
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<StudentAdd, StudentDto>(model);
            _studentService.Create(modelDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var modelDto = _studentService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<StudentDto, StudentDisplayModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(StudentDisplayModify model)
        {
            _studentService.Delete(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var modelDto = _studentService.GetById(id);
            if (modelDto is null) return RedirectToAction("Index");
            var model = _mapper.Map<StudentDto, StudentDisplayModify>(modelDto);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(StudentDisplayModify model)
        {
            if (ModelState.IsValid == false) return View(model);
            var modelDto = _mapper.Map<StudentDisplayModify, StudentDto>(model);
            _studentService.Edit(modelDto);
            return RedirectToAction("Index");
        }
    }
}
