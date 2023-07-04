
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
        public async Task<IActionResult> Index()
        {
            return Ok(await _unitOfWork.Designation.GetAllAsync());
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetDesignations(Guid companyId, Guid dpartmentId)
        {
            var designations = await _unitOfWork.Designation.Where(d => d.ComId == companyId);
            return Ok(designations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation data)
        {

            try
            {
                await _unitOfWork.Designation.AddAsync(data);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Ok();
        }

        [HttpGet("{id:guid}")]
        public IActionResult Edit(Guid DesigId)
        {
            var Designation = _unitOfWork.Designation.Where(x => x.DesigId == DesigId);
            if (Designation != null)
            {
                return Ok(Designation);
            }
            else
            {
                return Ok();
            }
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(Designation data)
        {

            try
            {
                await Task.Run(async () =>
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
        public IActionResult DesignationDetails(Guid id)
        {
            var Designation = _unitOfWork.Designation.Where(x => x.DesigId == id);
            if (Designation != null)
            {
                return Ok(Designation);
            }
            else
            {
                return Ok();
            }

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
