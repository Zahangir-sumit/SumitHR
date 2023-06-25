
using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Controllers
{
    public class DesignationController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly ILogger<DesignationController> _logger;

        public DesignationController(ApplicationDbContext context, ILogger<DesignationController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Designation.ToListAsync());
        }

        public IActionResult GetDesignations(Guid companyId)
        {
            var designations = _context.Designation.Where(d => d.ComId == companyId).ToList();
            return Json(designations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Companies = _context.Company.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation data)
        {
            Guid id = Guid.NewGuid();
            data.DesigId = id;
            try
            {
                _context.Designation.Add(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Index", "Designation");
        }

        public IActionResult Edit(Guid DesigId, Guid ComId)
        {
            var Designation = _context.Designation.Where(x => x.DesigId == DesigId).FirstOrDefault();
            if (Designation != null)
            {
                return View(Designation);
            }
            else
            {
                return View();
            }
        }

 
       
       
        [HttpPost]
        public async Task<IActionResult> Edit(Designation data)
        {

            try
            {
                await Task.Run(async () =>
                {
                    _context.Designation.Update(data);

                    _context.Entry(data).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Index", "Designation");
        }

        [HttpGet]
        public IActionResult DesignationDetails(Guid id)
        {
            var Designation = _context.Designation.Find(id);
            if (Designation != null)
            {
                return View(Designation);
            }
            else
            {
                return View();
            }

        }


    }
}
