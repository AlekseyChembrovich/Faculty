using System.Net;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Faculty.Common.Dto.Group;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Group;
using Faculty.Common.Dto.Specialization;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Faculty.AspUI.ViewModels.Specialization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ISpecializationService _specializationService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, ISpecializationService specializationService, IMapper mapper)
        {
            _groupService = groupService;
            _specializationService = specializationService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<GroupDisplay> groupDisplays = default;
            try
            {
                groupDisplays = _mapper.Map<IEnumerable<GroupDisplayDto>, IEnumerable<GroupDisplay>>(await _groupService.GetGroups());
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(groupDisplays.ToList());
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
        public async Task<ActionResult> Create(GroupAdd groupAdd)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(groupAdd);
                var groupDto = _mapper.Map<GroupAdd, GroupDto>(groupAdd);
                await _groupService.CreateGroup(groupDto);
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
            GroupModify groupModify = default;
            try
            {
                await FillViewBag();
                groupModify = _mapper.Map<GroupDto, GroupModify>(await _groupService.GetGroup(id));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(groupModify);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(GroupModify groupModify)
        {
            try
            {
                await _groupService.DeleteGroup(groupModify.Id);
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
            GroupModify groupModify = default;
            try
            {
                await FillViewBag();
                groupModify = _mapper.Map<GroupDto, GroupModify>(await _groupService.GetGroup(id));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(groupModify);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(GroupModify groupModify)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(groupModify);
                var groupDto = _mapper.Map<GroupModify, GroupDto>(groupModify);
                await _groupService.EditGroup(groupDto);
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
            ViewBag.Specializations = _mapper.Map<IEnumerable<SpecializationDto>, IEnumerable<SpecializationDisplayModify>>(await _specializationService.GetSpecializations());
        }
    }
}
