
using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.MVC.DHK.Controllers
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
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Company.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            return Json(await _unitOfWork.Company.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company data)
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

            return RedirectToAction("Index", "Company");
        }

        public IActionResult Edit(Guid id)
        {
            var Company = _unitOfWork.Company.GetById(id);
            if (Company != null)
            {
                return View(Company);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]  
        public async Task<IActionResult> Edit(Company data)
        {

                try
                {
                    await Task.Run(() =>
                    {
                        _unitOfWork.Company.EditAsync(data);
                        _unitOfWork.Save();
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }

            return RedirectToAction("Index", "Company");
        }

    }
}
