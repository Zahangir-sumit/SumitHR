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

        [HttpGet("GetAll")]
        //[Route("GetAll")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _unitOfWork.Department.GetAllAsync());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Department data)
        {
            try
            {
                _unitOfWork.Department.Add(data);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetById/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var Department = _unitOfWork.Department.GetById(id);
            if (Department != null)
            {
                return Ok(Department);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] Department data)
        {

            try
            {
                await Task.Run(() =>
                {
                    _unitOfWork.Department.EditAsync(data);
                    _unitOfWork.Save();
                });
            }
            catch (Exception ex)
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
