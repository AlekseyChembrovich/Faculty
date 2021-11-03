using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Group;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ISpecializationService _specializationService;

        public GroupController(IGroupService groupService, ISpecializationService specializationService)
        {
            _groupService = groupService;
            _specializationService = specializationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<GroupDisplay> groupDisplays = default;
            try
            {
                groupDisplays = await _groupService.GetGroups();
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
                await _groupService.CreateGroup(groupAdd);
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
                groupModify = await _groupService.GetGroup(id);
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
                groupModify = await _groupService.GetGroup(id);
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
                if (ModelState.IsValid == false) return View(groupModify);
                await _groupService.EditGroup(groupModify);
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
            ViewBag.Specializations = await _specializationService.GetSpecializations();
        }
    }
}
