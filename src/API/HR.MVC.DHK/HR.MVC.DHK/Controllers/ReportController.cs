using HR.MVC.DHK.Models;
using HR.MVC.DHK.Models.DTO;
using HR.MVC.DHK.Models.VM;
using HR.MVC.DHK.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using HR.MVC.DHK.RepositoryPattern;

namespace HR.MVC.DHK.Controllers
{
    public class ReportController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IUnitOfWork unitOfWork, ApplicationDbContext context, ILogger<ReportController> logger) :base(unitOfWork)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SalaryReport()
        {

            ViewBag.Companies = _context.Company.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult Report(SalaryRptDTO salaryRptDTO)
        {
            if(salaryRptDTO.Type == "Summary")
            {
                return RedirectToAction("SalarySummaryReportView", salaryRptDTO );
            }
            else
            {
                return RedirectToAction("SalaryReportView", salaryRptDTO );
            }
        }


        //[HttpPost]
        public async Task<IActionResult> SalaryReportView(SalaryRptDTO salaryRptDTO)
        {


            var SalaryList = await _unitOfWork.Report.SalaryReportView(salaryRptDTO);

            return View(SalaryList);
        }



        public async Task<IActionResult> SalarySummaryReportView(SalaryRptDTO salaryRptDTO)
        {

            var SalarySummaries = await _unitOfWork.Report.SalarySummaryReportView(salaryRptDTO);

            return View(SalarySummaries);
        }
        public IActionResult AttendanceReport()
        {

            ViewBag.Companies = _unitOfWork.Company.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult AttendtanceReportView(AttendanceParameters AttendanceParameters)
        {
            if (AttendanceParameters.AttendanceType == "Daily")
            {
                return RedirectToAction("DailyAttendtanceReport", AttendanceParameters);
            }
            else
            {
                return RedirectToAction("MonthlyAttendtanceReport", AttendanceParameters);
            }
        }
        public async Task<IActionResult> DailyAttendtanceReport(AttendanceParameters AttendanceParameters)
        {


            var DailyAttendances = await _unitOfWork.Report.DailyAttendtanceReport(AttendanceParameters);

            return View(DailyAttendances);
        }



        public async Task<IActionResult> MonthlyAttendtanceReport(AttendanceParameters AttendanceParameters)
        {

            var MonthlyAttendances = await _unitOfWork.Report.MonthlyAttendtanceReport(AttendanceParameters);

            return View(MonthlyAttendances);
        }

    }
}
