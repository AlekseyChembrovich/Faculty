using System.Net;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Faculty.Common.Dto.Group;
using System.Collections.Generic;
using Faculty.Common.Dto.Faculty;
using Faculty.Common.Dto.Curator;
using Faculty.Common.Dto.Student;
using Faculty.AspUI.ViewModels.Group;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.AspUI.ViewModels.Student;
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
        [AllowAnonymous]
        public async Task<ActionResult> Index(int? valueFilter = null)
        {
            IEnumerable<FacultyDisplay> facultiesDisplay = default;
            try
            {
                facultiesDisplay = _mapper.Map<IEnumerable<FacultyDisplayDto>, IEnumerable<FacultyDisplay>>(await _facultyService.GetFaculties());
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
                var facultyDto = _mapper.Map<FacultyAdd, FacultyDto>(facultyAdd);
                await _facultyService.CreateFaculty(facultyDto);
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
                await FillViewBag();
                facultyModify = _mapper.Map<FacultyDto, FacultyModify>(await _facultyService.GetFaculty(id));
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
                await _facultyService.DeleteFaculty(facultyModify.Id);
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
                await FillViewBag();
                facultyModify = _mapper.Map<FacultyDto, FacultyModify>(await _facultyService.GetFaculty(id));
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
                await FillViewBag();
                if (ModelState.IsValid == false) return View(facultyModify);
                var facultyDto = _mapper.Map<FacultyModify, FacultyDto>(facultyModify);
                await _facultyService.EditFaculty(facultyDto);
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
            ViewBag.Groups = _mapper.Map<IEnumerable<GroupDisplayDto>, IEnumerable<GroupDisplay>>(await _groupService.GetGroups());
            ViewBag.Students = _mapper.Map<IEnumerable<StudentDto>, IEnumerable<StudentDisplayModify>>(await _studentService.GetStudents());
            ViewBag.Curators = _mapper.Map<IEnumerable<CuratorDto>, IEnumerable<CuratorDisplayModify>>(await _curatorService.GetCurators());
        }
    }
}
