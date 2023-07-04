using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.MVC.DHK.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IUnitOfWork unitOfWork, ApplicationDbContext context, ILogger<DepartmentController> logger):base(unitOfWork)
        {
            _context = context;
            _logger = logger;
        }

        public async  Task<IActionResult> Index()
        {
            //list of department
           var data = await _unitOfWork.Department.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> GetDepartments(Guid companyId)
        {
            var departments = await _unitOfWork.Department.Where(d => d.ComId == companyId);
            return Json(departments);
        }
        //for testing view

        [HttpGet]
        public IActionResult DetailsDepartment(Guid id)
        {
            var data = _unitOfWork.Department.Where(x => x.DeptId == id);

            return View(data);
        }

        
        public async Task<IActionResult> CreateDepartment()
        {
            ViewBag.Companies = await _unitOfWork.Company.GetAllAsync();

            return View();
        }


        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            try
            {

                _unitOfWork.Department.Add(department);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> EditDepartment(Guid id)
        {
            try
            {
               var data = _unitOfWork.Department.FindWhere(x => x.DeptId == id);
                ViewBag.Companies = await _unitOfWork.Company.GetAllAsync();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult EditDepartment(Department department)
        {
            try
            {

                _unitOfWork.Department.Edit(department);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));

        }

        public IActionResult DeleteDepartment()
        {
            return View();
        }
        public async Task<IActionResult> Delete(Guid Id)
        {

            try
            {
                await Task.Run(async () =>
                {
                    _unitOfWork.Department.Remove(x => x.DeptId == Id);

                    _unitOfWork.Save();
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Index", "Employee");
        }

    }
}
