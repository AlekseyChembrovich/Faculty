using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class FacultyController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;
        private readonly ICuratorService _curatorService;

        public FacultyController(IFacultyService facultyService, IGroupService groupService, IStudentService studentService, ICuratorService curatorService)
        {
            _facultyService = facultyService;
            _groupService = groupService;
            _studentService = studentService;
            _curatorService = curatorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index(int? valueFilter = null)
        {
            IEnumerable<FacultyDisplay> facultiesDisplay = default;
            try
            {
                facultiesDisplay = await _facultyService.GetFaculties();
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            var listFacultiesDisplay = facultiesDisplay.ToList();
            var facultiesFilter = valueFilter != null
                ? listFacultiesDisplay.Where(x => x.CountYearEducation == valueFilter.Value).ToList()
                : listFacultiesDisplay.ToList();
            return View(facultiesFilter.ToList());
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                await FillViewBag();
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FacultyAdd facultyAdd)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(facultyAdd);
                await _facultyService.CreateFaculty(facultyAdd);
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
            FacultyModify facultyModify = default;
            try
            {
                facultyModify = await _facultyService.GetFaculty(id);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(facultyModify);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(FacultyModify facultyModify)
        {
            try
            {
                await _curatorService.DeleteCurator(facultyModify.Id);
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
            FacultyModify facultyModify = default;
            try
            {
                facultyModify = await _facultyService.GetFaculty(id);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(facultyModify);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FacultyModify facultyModify)
        {
            try
            {
                if (ModelState.IsValid == false) return View(facultyModify);
                await _facultyService.EditFaculty(facultyModify);
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

        public async Task FillViewBag()
        {
            ViewBag.Groups = await _groupService.GetGroups();
            ViewBag.Students = await _studentService.GetStudents();
            ViewBag.Curators = await _curatorService.GetCurators();
        }
    }
}
