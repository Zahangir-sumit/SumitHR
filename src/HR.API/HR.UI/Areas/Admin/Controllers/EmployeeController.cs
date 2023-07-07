using HR.MVC.DHK.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HR.UI.Controllers
{
    public class EmployeeController : BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IHttpClientFactory httpClientFactory, ILogger<EmployeeController> logger):base(httpClientFactory)
        {
            //_context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var response = _client.GetAsync("Employee/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var employees = JsonConvert.DeserializeObject<List<Employee>>(data);
                return View(employees);
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
        public async Task<IActionResult> Create(Employee model)
        {

            try
            {

                var response = await _client.PostAsJsonAsync("Employee/Create", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Employee");
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

            return RedirectToAction("Index", "Employee");
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> Edit(Guid Id)
        {

            var response = _client.GetAsync("Employee/GetById/" + Id.ToString()).Result;
            var company = _client.GetAsync("Company/GetAll").Result;
            var department = _client.GetAsync("Department/GetAll").Result;
            var designation = _client.GetAsync("Designation/GetAll").Result;
            var shift = _client.GetAsync("Shift/GetAll").Result;

            if (response.IsSuccessStatusCode && company.IsSuccessStatusCode)
            {
                var employees = response.Content.ReadAsStringAsync().Result;
                var com = company.Content.ReadAsStringAsync().Result;
                var dept = department.Content.ReadAsStringAsync().Result;
                var desig = designation.Content.ReadAsStringAsync().Result;
                var shft = shift.Content.ReadAsStringAsync().Result;

                ViewBag.Companies = JsonConvert.DeserializeObject<List<Company>>(com);
                ViewBag.Departments = JsonConvert.DeserializeObject<List<Department>>(dept);
                ViewBag.Designations = JsonConvert.DeserializeObject<List<Designation>>(desig);
                ViewBag.Shifts = JsonConvert.DeserializeObject<List<Designation>>(shft);
                var employee = JsonConvert.DeserializeObject<Employee>(employees);

                if (employee != null)
                {

                    return View(employee);
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee model)
        {

            try
            {

                var response = await _client.PostAsJsonAsync("Employee/Edit", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Optionally, you can parse and process the result here
                    return RedirectToAction("Index", "Employee");
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

            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }
        //[HttpGet]
        //public IActionResult EmployeeDetails(Guid id)
        //{
        //    var Employee = _context.Employee.Find(id);
        //    if (Employee != null)
        //    {
        //        return View(Employee);
        //    }
        //    else
        //    {
        //        return Ok();
        //    }

        //}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Delete(Guid Id)
        {

            var response = _client.GetAsync("Employee/Delete/" + Id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {

                 return RedirectToAction("Index", "Employee");

            }
            return RedirectToAction("Index", "Employee");
        }
    }
}
