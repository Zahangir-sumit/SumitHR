using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ApplicationDbContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employee
                                .Include(c => c.Company)
                                .Include(d => d.Department)
                                .ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employee.ToListAsync();
            return Json(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Companies = _context.Company.ToList();
            //ViewBag.Departments = _context.Department.ToList();
            //ViewBag.Designations = _context.Designation.ToList();
            //ViewBag.Shifts = _context.Shift.ToList();


            ViewBag.CompanyList = _context.Company.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee data)
        {
            Guid id = Guid.NewGuid();
            data.EmpId = id;
            try
            {
                _context.Employee.Add(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Edit(Guid EmpId)
        {
            var Employee = _context.Employee.Where(x => x.EmpId == EmpId).FirstOrDefault();
            if (Employee != null)
            {
                ViewBag.Companies = _context.Company.ToList();
                ViewBag.Departments = _context.Department.ToList();
                ViewBag.Designations = _context.Designation.ToList();
                ViewBag.Shifts = _context.Shift.ToList();
                return View(Employee);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee data)
        {

            try
            {
                await Task.Run(async () =>
                {
                    _context.Employee.Update(data);

                    _context.Entry(data).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                });
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
            return View();
        }
        [HttpGet]
        public IActionResult EmployeeDetails(Guid id)
        {
            var Employee = _context.Employee.Find(id);
            if (Employee != null)
            {
                return View(Employee);
            }
            else
            {
                return View();
            }

        }

    }
}
