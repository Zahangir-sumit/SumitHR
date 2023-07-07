
using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR.API.Controllers
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Designation.GetAllAsync());
        }

        [HttpGet]
        [Route("GetAllBy/{ComId:guid}")]
        public async Task<IActionResult> GetAllBy(Guid ComId)
        {
            return Ok(await _unitOfWork.Designation.Where(x => x.ComId == ComId));
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
        public IActionResult GetById(Guid Id)
        {
            var Designation = _unitOfWork.Designation.FindWhere(x => x.DesigId == Id);
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

                 await _unitOfWork.Designation.EditAsync(data);
                 _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Route("Delete/{id:guid}")]
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
