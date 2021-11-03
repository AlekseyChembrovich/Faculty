using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Faculty.AspUI.ViewModels.Specialization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<SpecializationDisplayModify> specializationsDisplay = default;
            try
            {
                specializationsDisplay = await _specializationService.GetSpecializations();
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(specializationsDisplay.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(SpecializationAdd specializationAdd)
        {
            try
            {
                if (ModelState.IsValid == false) return View(specializationAdd);
                await _specializationService.CreateSpecialization(specializationAdd);
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
            SpecializationDisplayModify specializationModify = default;
            try
            {
                specializationModify = await _specializationService.GetSpecialization(id);
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
        public async Task<ActionResult> Delete(SpecializationDisplayModify specializationModify)
        {
            try
            {
                await _specializationService.DeleteSpecialization(specializationModify.Id);
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
            SpecializationDisplayModify specializationModify = default;
            try
            {
                specializationModify = await _specializationService.GetSpecialization(id);
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
        public async Task<ActionResult> Edit(SpecializationDisplayModify specializationModify)
        {
            try
            {
                if (ModelState.IsValid == false) return View(specializationModify);
                await _specializationService.EditSpecialization(specializationModify);
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
