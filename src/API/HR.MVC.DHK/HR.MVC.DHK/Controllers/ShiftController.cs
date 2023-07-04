using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Controllers
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

        public async Task<IActionResult> Index()
        {
            //list of department
            var data = await _unitOfWork.Shift.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> GetShifts(Guid companyId)
        {
            var designations = await _unitOfWork.Shift.Where(d => d.ComId == companyId);
            return Json(designations);
        }

        //for testing view

        [HttpGet]
        public IActionResult DetailsShift(Guid id)
        {
            var data = _unitOfWork.Shift.FindWhere(x => x.ShiftId == id);

            return View(data);
        }


        public IActionResult CreateShift()
        {
            ViewBag.Companies = _unitOfWork.Company.GetAll();
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> CreateShift(Shift data)
        {
            try
            {
                await _unitOfWork.Shift.AddAsync(data);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public IActionResult GetShift(Guid id)
        {

            _unitOfWork.Shift.FindWhere(x => x.ShiftId == id);
            return View();
        }

        public IActionResult EditShift(Guid id)
        {
            try
            {
                var data = _unitOfWork.Shift.FindWhere(x => x.ShiftId == id);
                ViewBag.Companies = _unitOfWork.Company.GetAll();
                return View(data);
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]

        public IActionResult EditShift(Shift shift)
        {
            try
            {
                _unitOfWork.Shift.Edit(shift);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

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

            return RedirectToAction("Index", "Employee");
        }

    }
}