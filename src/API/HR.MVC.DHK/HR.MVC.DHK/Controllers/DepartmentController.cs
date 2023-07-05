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
            return Ok(data);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = _unitOfWork.Department.GetAll();
            return Ok(departments);
        }
        //for testing Ok

        [HttpGet("{id:guid}")]
        public IActionResult DetailsDepartment(Guid id)
        {
            var data = _unitOfWork.Department.Where(x => x.DeptId == id);

            return Ok(data);
        }


        [HttpPost]
        public IActionResult Create(Department department)
        {
            try
            {

                _unitOfWork.Department.Add(department);
                _unitOfWork.Save();

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok();

        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
               var data = _unitOfWork.Department.FindWhere(x => x.DeptId == id);
                //OkBag.Companies = await _unitOfWork.Company.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok();
        }


        [HttpPost]
        public IActionResult EditDepartment(Department department)
        {
            try
            {

                _unitOfWork.Department.Edit(department);
                _unitOfWork.Save();

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Ok();

        }

        [HttpGet("{id:guid}")]
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

            return Ok();
        }

    }
}
