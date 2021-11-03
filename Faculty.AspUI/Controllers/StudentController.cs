using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Student;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<StudentDisplayModify> studentsDisplay = default;
            try
            {
                studentsDisplay = await _studentService.GetStudents();
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
                await _studentService.CreateStudent(studentAdd);
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
                studentModify = await _studentService.GetStudent(id);
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
                specializationModify = await _studentService.GetStudent(id);
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
                await _studentService.EditStudent(studentModify);
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
