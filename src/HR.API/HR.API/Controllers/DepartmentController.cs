using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.API.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Department.GetAllAsync());
        }

        [HttpGet]
        [Route("GetAllBy/{ComId:guid}")]
        public async Task<IActionResult> GetAllBy(Guid ComId)
        {
            return Ok(await _unitOfWork.Department.Where(x => x.ComId == ComId));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Department data)
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
        [Route("GetById/{Id:guid}")]
        public IActionResult GetById(Guid Id)
        {
            var Department = _unitOfWork.Department.FindWhere(x => x.DeptId == Id);
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
        public async Task<IActionResult> Edit(Department data)
        {

            try
            {
                await _unitOfWork.Department.EditAsync(data);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Route("Delete/{Id:guid}")]
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
