using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Controllers
{
    public class EmployeeController : BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IUnitOfWork unitOfWork,  ILogger<EmployeeController> logger):base(unitOfWork)
        {
            //_context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var employees = _unitOfWork.Employee.GetEmployees();
            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployees(Guid companyId, Guid departmentId)
        {
            var employees = await _unitOfWork.Employee.Where(x => x.ComId == companyId);
            return Json(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.CompanyList = _unitOfWork.Company.GetAll();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Employee data)
        {

            try
            {
                _unitOfWork.Employee.Add(data);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return RedirectToAction("Index", "Employee");
        }

        public async Task<IActionResult> Edit(Guid EmpId)
        {

            var Employee =  _unitOfWork.Employee.FindWhere(x => x.EmpId == EmpId);

            if (Employee != null)
            {
                ViewBag.Companies = await _unitOfWork.Company.GetAllAsync();
                ViewBag.Departments = await _unitOfWork.Department.GetAllAsync();
                ViewBag.Designations = await _unitOfWork.Designation.GetAllAsync();
                ViewBag.Shifts = await _unitOfWork.Shift.GetAllAsync();
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
                    _unitOfWork.Employee.Edit(data);

                    _unitOfWork.Save();
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
        public async Task<IActionResult> Delete(Guid Id)
        {

            try
            {
                await Task.Run(async () =>
                {
                    _unitOfWork.Employee.Remove(x => x.EmpId == Id);

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
