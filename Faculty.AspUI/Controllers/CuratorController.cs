using System.Net;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Curator;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class CuratorController : Controller
    {
        private readonly ICuratorService _curatorService;
        private readonly IMapper _mapper;

        public CuratorController(ICuratorService curatorService, IMapper mapper)
        {
            _curatorService = curatorService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            IEnumerable<CuratorDisplayModify> curatorsDisplay = default;
            try
            {
                curatorsDisplay = _mapper.Map<IEnumerable<CuratorDto>, IEnumerable<CuratorDisplayModify>>(await _curatorService.GetCurators());
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(curatorsDisplay.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CuratorAdd curatorAdd)
        {
            try
            {
                if (ModelState.IsValid == false) return View(curatorAdd);
                var curatorDto = _mapper.Map<CuratorAdd, CuratorDto>(curatorAdd);
                await _curatorService.CreateCurator(curatorDto);
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
            CuratorDisplayModify curatorModify = default;
            try
            {
                curatorModify = _mapper.Map<CuratorDto, CuratorDisplayModify>(await _curatorService.GetCurator(id));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(curatorModify);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(CuratorDisplayModify curatorModify)
        {
            try
            {
                await _curatorService.DeleteCurator(curatorModify.Id);
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
            CuratorDisplayModify curatorModify = default;
            try
            {
                curatorModify = _mapper.Map<CuratorDto, CuratorDisplayModify>(await _curatorService.GetCurator(id));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(curatorModify);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CuratorDisplayModify curatorModify)
        {
            try
            {
                if (ModelState.IsValid == false) return View(curatorModify);
                var curatorDto = _mapper.Map<CuratorDisplayModify, CuratorDto>(curatorModify);
                await _curatorService.EditCurator(curatorDto);
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
