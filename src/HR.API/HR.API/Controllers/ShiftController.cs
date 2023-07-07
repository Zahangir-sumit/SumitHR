using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.API.Controllers
{
    public class ShiftController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ShiftController> _logger;

        public ShiftController(IUnitOfWork unitOfWork, ApplicationDbContext context, ILogger<ShiftController> logger): base(unitOfWork)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var designations = await _unitOfWork.Shift.GetAllAsync();
            return Ok(designations);
        }

        [HttpGet]
        [Route("GetAllBy/{ComId:guid}")]
        public async Task<IActionResult> GetAllBy(Guid ComId)
        {
            return Ok(await _unitOfWork.Shift.Where(x => x.ComId == ComId));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Shift data)
        {
            try
            {
                await _unitOfWork.Shift.AddAsync(data);
                _unitOfWork.Save();
                return Ok();
            }
            catch
            {
                return Ok();
            }
        }


        [HttpGet]
        [Route("GetById/{Id:guid}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var data = _unitOfWork.Shift.FindWhere(x => x.ShiftId == Id);
                //OkBag.Companies = _unitOfWork.Company.GetAll();
                return Ok(data);
            }
            catch
            {
                return Ok();
            }
        }


        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Shift shift)
        {
            try
            {
                await _unitOfWork.Shift.EditAsync(shift);
                _unitOfWork.Save();
                return Ok();
            }
            catch
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("Delete/{Id:guid}")]
        public async Task<IActionResult> Delete(Guid Id)
        {

            try
            {
                await Task.Run(async () =>
                {
                    _unitOfWork.Shift.Remove(x => x.ShiftId == Id);

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