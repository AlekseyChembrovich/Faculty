using System.Net;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;
using Faculty.AspUI.ViewModels.Student;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
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
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<StudentDisplayModify> studentsDisplay = default;
            try
            {
                studentsDisplay = _mapper.Map<IEnumerable<StudentDto>, IEnumerable<StudentDisplayModify>>(await _studentService.GetStudents());
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(studentsDisplay.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentAdd studentAdd)
        {
            try
            {
                if (ModelState.IsValid == false) return View(studentAdd);
                var studentDto = _mapper.Map<StudentAdd, StudentDto>(studentAdd);
                await _studentService.CreateStudent(studentDto);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            StudentDisplayModify studentModify = default;
            try
            {
                studentModify = _mapper.Map<StudentDto, StudentDisplayModify>(await _studentService.GetStudent(id));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(studentModify);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(StudentDisplayModify studentModify)
        {
            try
            {
                await _studentService.DeleteStudent(studentModify.Id);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            StudentDisplayModify specializationModify = default;
            try
            {
                specializationModify = _mapper.Map<StudentDto, StudentDisplayModify>(await _studentService.GetStudent(id));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(specializationModify);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StudentDisplayModify studentModify)
        {
            try
            {
                if (ModelState.IsValid == false) return View(studentModify);
                var studentDto = _mapper.Map<StudentDisplayModify, StudentDto>(studentModify);
                await _studentService.EditStudent(studentDto);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }
    }
}
