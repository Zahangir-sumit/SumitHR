
using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;

namespace HR.API.Controllers
{
    public class CompanyController : BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(IUnitOfWork unitOfWork, ApplicationDbContext context, ILogger<CompanyController> logger):base(unitOfWork)
        {
            //_context = context;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        //[Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Company.GetAllAsync());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody]Company data)
        {
                try
                {
                    _unitOfWork.Company.Add(data);
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
            var Company = _unitOfWork.Company.FindWhere(x => x.ComId == Id);
            if (Company != null)
            {
                return Ok(Company);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody]Company data)
        {

                try
                {

                      await _unitOfWork.Company.EditAsync(data);
                      _unitOfWork.Save();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }

            return Ok();
        }

    }
}
