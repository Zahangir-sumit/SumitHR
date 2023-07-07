
using HR.MVC.DHK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.UI.Controllers
{
    public class DesignationController : BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger<DesignationController> _logger;

        public DesignationController(IHttpClientFactory httpClientFactory, ILogger<DesignationController> logger):base(httpClientFactory)
        {

            //_context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var response = _client.GetAsync("Designation/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

               var companies = JsonConvert.DeserializeObject<List<Designation>>(data);
                return View(companies);
            }

            return View();
        }

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
        public async Task<IActionResult> Create(Designation model)
        {
                try
                {

                var response = await _client.PostAsJsonAsync("Designation/Create", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Designation");
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

            return RedirectToAction("Index", "Designation");
        }

        public IActionResult Edit(Guid Id)
        {
            var response = _client.GetAsync("Designation/GetById/"+ Id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var Designation = JsonConvert.DeserializeObject<Designation>(data);
                if (Designation != null)
                {
                    return View(Designation);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> Edit(Designation model)
        {

            try
            {

                var response = await _client.PostAsJsonAsync("Designation/Edit", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Designation");
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

            return RedirectToAction("Index", "Designation");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Delete(Guid Id)
        {

            var response = _client.GetAsync("Designation/Delete/" + Id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "Designation");

            }
            return RedirectToAction("Index", "Designation");
        }
    }
}
