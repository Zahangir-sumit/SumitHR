using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.API.Controllers
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _unitOfWork.Employee.GetAllAsync();
            return Ok(employees);
        }


        [HttpPost]
        [Route("Create")]
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

            return Ok();
        }

        [HttpGet]
        [Route("GetById/{Id:guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {

            var Employee =  _unitOfWork.Employee.FindWhere(x => x.EmpId == Id);

            if (Employee != null)
            {
                //ViewBag.Companies = await _unitOfWork.Company.GetAllAsync();
                //ViewBag.Departments = await _unitOfWork.Department.GetAllAsync();
                //ViewBag.Designations = await _unitOfWork.Designation.GetAllAsync();
                //ViewBag.Shifts = await _unitOfWork.Shift.GetAllAsync();
                return Ok(Employee);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Employee data)
        {

            try
            {

                  await _unitOfWork.Employee.EditAsync(data);

                  _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

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
        [HttpGet]
        [Route("Delete/{Id:guid}")]
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

            return Ok();
        }
    }
}
