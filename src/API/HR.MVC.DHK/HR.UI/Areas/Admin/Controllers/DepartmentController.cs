using HR.MVC.DHK.Models;
using HR.UI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.MVC.DHK.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IHttpClientFactory httpClientFactory, ILogger<DepartmentController> logger):base(httpClientFactory)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var response = _client.GetAsync("Department/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var companies = JsonConvert.DeserializeObject<List<Department>>(data);
                return View(companies);
            }

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCompanies()
        //{
        //    return Json(await _unitOfWork.Department.GetAllAsync());
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            try
            {

                var response = await _client.PostAsJsonAsync("Department/Create", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Department");
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

            return RedirectToAction("Index", "Department");
        }

        public IActionResult Edit(Guid id)
        {
            var response = _client.GetAsync("Department/GetById/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var Department = JsonConvert.DeserializeObject<Department>(data);
                if (Department != null)
                {
                    return View(Department);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department model)
        {

            try
            {

                var response = await _client.PostAsJsonAsync("Department/Edit", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Department");
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

            return RedirectToAction("Index", "Department");
        }

    }
}
