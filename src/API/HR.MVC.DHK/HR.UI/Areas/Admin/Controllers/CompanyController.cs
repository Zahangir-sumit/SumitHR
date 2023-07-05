
using HR.MVC.DHK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.UI.Controllers
{
    public class CompanyController : BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(IHttpClientFactory httpClientFactory, ILogger<CompanyController> logger):base(httpClientFactory)
        {

            //_context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var response = _client.GetAsync("Company/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

               var companies = JsonConvert.DeserializeObject<List<Company>>(data);
                return View(companies);
            }

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCompanies()
        //{
        //    return Json(await _unitOfWork.Company.GetAllAsync());
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company model)
        {
                try
                {

                var response = await _client.PostAsJsonAsync("Company/Create", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Company");
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

            return RedirectToAction("Index", "Company");
        }

        public IActionResult Edit(Guid id)
        {
            var response = _client.GetAsync("Company/GetById/"+id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var company = JsonConvert.DeserializeObject<Company>(data);
                if (company != null)
                {
                    return View(company);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> Edit(Company model)
        {

            try
            {

                var response = await _client.PostAsJsonAsync("Company/Edit", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Company");
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

            return RedirectToAction("Index", "Company");
        }

    }
}
