using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.AspUI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
    public class UserController : Controller
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly HttpClient _userClient;

        public UserController(IHttpClientFactory clientFactory, IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userClient = clientFactory.CreateClient("usersHttpClient");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "User/GetAll");
            var response = await _userClient.SendAsync(message);
            var usersJson = await response.Content.ReadAsStringAsync();
            var usersDisplay = JsonConvert.DeserializeObject<IEnumerable<UserDisplay>>(usersJson);
            return View(usersDisplay?.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FillViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAdd user)
        {
            await FillViewBag();
            if (ModelState.IsValid == false) return View(user);
            var response = await _userClient.PostAsync("User/Create", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"User/Delete?id={id}");
            _ = await _userClient.SendAsync(message);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"User/GetById?id={id}");
            var response = await _userClient.SendAsync(message);
            var modelJson = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<UserModify>(modelJson);
            await FillViewBag();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModify user)
        {
            await FillViewBag();
            if (ModelState.IsValid == false) return View(user);
            var response = await _userClient.PostAsync("User/Edit", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        [HttpGet]
        public IActionResult EditPassword(string id)
        {
            var model = new EditPassUser { Id = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPassUser user)
        {
            await FillViewBag();
            if (ModelState.IsValid == false) return View(user);
            var response = await _userClient.PostAsync("User/EditPassword", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        public async Task FillViewBag()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "User/GetRoles");
            var response = await _userClient.SendAsync(message);
            var modelJson = await response.Content.ReadAsStringAsync();
            var namesRole = JsonConvert.DeserializeObject<IEnumerable<string>>(modelJson);
            ViewBag.Roles = namesRole;
        }
    }
}
