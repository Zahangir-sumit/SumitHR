
using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.MVC.DHK.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ApplicationDbContext context, ILogger<CompanyController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Company.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            return Json(await _context.Company.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company data)
        {
                try
                {
                    _context.Company.Add(data);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }

            return RedirectToAction("Index", "Company");
        }

        public IActionResult Edit(Guid id)
        {
            var Company = _context.Company.Find(id);
            if (Company != null)
            {
                return View(Company);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]  
        public async Task<IActionResult> Edit(Company data)
        {

                try
                {
                    await Task.Run(() =>
                    {
                        _context.Company.Update(data);

                        _context.Entry(data).State = EntityState.Modified;

                        _context.SaveChangesAsync();
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }

            return RedirectToAction("Index", "Company");
        }

    }
}
