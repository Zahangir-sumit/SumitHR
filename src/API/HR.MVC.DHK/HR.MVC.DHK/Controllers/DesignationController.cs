
using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Controllers
{
    public class DesignationController : BaseController
    {

        //private readonly ApplicationDbContext _context;
        private readonly ILogger<DesignationController> _logger;

        public DesignationController(IUnitOfWork unitOfWork, ILogger<DesignationController> logger):base(unitOfWork)
        {
            _logger = logger;
        }
        [HttpGet("GetAll")]
        //[Route("GetAll")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _unitOfWork.Designation.GetAllAsync());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Designation data)
        {
            try
            {
                _unitOfWork.Designation.Add(data);
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
            var Designation = _unitOfWork.Designation.GetById(id);
            if (Designation != null)
            {
                return Ok(Designation);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] Designation data)
        {

            try
            {
                await Task.Run(() =>
                {
                    _unitOfWork.Designation.EditAsync(data);
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
                    _unitOfWork.Designation.Remove(x => x.DesigId == Id);

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
