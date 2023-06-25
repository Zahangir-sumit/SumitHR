using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ApplicationDbContext context, ILogger<DepartmentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //list of department
           var data =  _context.Department.ToList();
            return View(data);
        }
        public IActionResult GetDepartments(Guid companyId)
        {
            var departments = _context.Department.Where(d => d.ComId == companyId).ToList();
            return Json(departments);
        }
        //for testing view

        [HttpGet]
        public IActionResult DetailsDepartment(Guid id)
        {
            var data = _context.Department.Where(x => x.DeptId == id).FirstOrDefault();

            return View(data);
        }

        
        public IActionResult CreateDepartment()
        {
            ViewBag.Companies =   _context.Company.ToList();

            return View();
        }


        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            try
            {

                Guid id = Guid.NewGuid();
                department.DeptId = id;
                try
                {
                    _context.Department.Add(department);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));

        }


        public ActionResult GetDepartment(Guid id)
        {

            return View();
        }

        public ActionResult EditDepartment(Guid id)
        {
            try
            {
               var data = _context.Department.Where(x => x.DeptId == id).FirstOrDefault();
                ViewBag.Companies = _context.Company.ToList();
                return View(data);
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult EditDepartment(Department department)
        {
            try
            {

                _context.Department.Update(department);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteDepartment()
        {
            return View();
        }
        public ActionResult DeleteDepartment(Guid id)
        {
            var data = _context.Department.Where(x => x.DeptId == id).FirstOrDefault();

            return View(data);
           // return View();
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Department department = _context.Department.Where(x => x.DeptId == id).FirstOrDefault();
           //Department data =  _context.Department.Where(x => x.DeptId == dept.DeptId);
            _context.Department.Remove(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
