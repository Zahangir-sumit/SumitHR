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

namespace HR.API.Controllers
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


        [HttpPost]
        [Route("SalaryReport")]
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


        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<IActionResult> SalaryReportView(SalaryRptDTO salaryRptDTO)
        {


            var SalaryList = await _unitOfWork.Report.SalaryReportView(salaryRptDTO);

            return Ok(SalaryList);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<IActionResult> SalarySummaryReportView(SalaryRptDTO salaryRptDTO)
        {

            var SalarySummaries = await _unitOfWork.Report.SalarySummaryReportView(salaryRptDTO);

            return Ok(SalarySummaries);
        }

        [HttpPost]
        [Route("AttendanceReport")]
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<IActionResult> DailyAttendtanceReport(AttendanceParameters AttendanceParameters)
        {


            var DailyAttendances = await _unitOfWork.Report.DailyAttendtanceReport(AttendanceParameters);

            return Ok(DailyAttendances);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<IActionResult> MonthlyAttendtanceReport(AttendanceParameters AttendanceParameters)
        {

            var MonthlyAttendances = await _unitOfWork.Report.MonthlyAttendtanceReport(AttendanceParameters);

            return Ok(MonthlyAttendances);
        }

    }
}
