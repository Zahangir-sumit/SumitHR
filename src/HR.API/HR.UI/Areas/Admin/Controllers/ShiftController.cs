using HR.MVC.DHK.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HR.UI.Controllers
{
    public class ShiftController : BaseController
    {

        private readonly ILogger<ShiftController> _logger;

        public ShiftController(IHttpClientFactory httpClientFactory, ILogger<ShiftController> logger) : base(httpClientFactory)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var response = _client.GetAsync("Shift/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var shifts = JsonConvert.DeserializeObject<List<Shift>>(data);
                return View(shifts);
            }

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCompanies()
        //{
        //    return Json(await _unitOfWork.Shift.GetAllAsync());
        //}

        [HttpGet]
        public IActionResult Create()
        {
            var response = _client.GetAsync("Company/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var companies = JsonConvert.DeserializeObject<List<Company>>(data);
                ViewBag.Companies = companies;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shift model)
        {
            try
            {

                var response = await _client.PostAsJsonAsync("Shift/Create", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Shift");
                }
                else
                {
                    // Handle unsuccessful API response
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Index", "Shift");
        }

        public IActionResult Edit(Guid Id)
        {
            var response = _client.GetAsync("Shift/GetById/" + Id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var Shift = JsonConvert.DeserializeObject<Shift>(data);
                if (Shift != null)
                {
                    return View(Shift);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Shift model)
        {

            try
            {

                var response = await _client.PostAsJsonAsync("Shift/Edit", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Shift");
                }
                else
                {
                    // Handle unsuccessful API response
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Index", "Shift");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Delete(Guid Id)
        {

            var response = _client.GetAsync("Shift/Delete/" + Id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "Shift");

            }
            return RedirectToAction("Index", "Shift");
        }

    }
}