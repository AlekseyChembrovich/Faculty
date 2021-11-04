using System.Net;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Specialization;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Faculty.AspUI.ViewModels.Specialization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;
        private readonly IMapper _mapper;

        public SpecializationController(ISpecializationService specializationService, IMapper mapper)
        {
            _specializationService = specializationService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<SpecializationDisplayModify> specializationsDisplay = default;
            try
            {
                specializationsDisplay = _mapper.Map<IEnumerable<SpecializationDto>, IEnumerable<SpecializationDisplayModify>>(await _specializationService.GetSpecializations());
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
                var specializationDto = _mapper.Map<SpecializationAdd, SpecializationDto>(specializationAdd);
                await _specializationService.CreateSpecialization(specializationDto);
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
                specializationModify = _mapper.Map<SpecializationDto, SpecializationDisplayModify>(await _specializationService.GetSpecialization(id));
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
                specializationModify = _mapper.Map<SpecializationDto, SpecializationDisplayModify>(await _specializationService.GetSpecialization(id));
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
                var specializationDto = _mapper.Map<SpecializationDisplayModify, SpecializationDto>(specializationModify);
                await _specializationService.EditSpecialization(specializationDto);
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
