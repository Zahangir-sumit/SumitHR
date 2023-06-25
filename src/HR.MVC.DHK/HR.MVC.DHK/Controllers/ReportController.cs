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

namespace HR.MVC.DHK.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ApplicationDbContext context, ILogger<ReportController> logger)
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

            var comIdParam = new SqlParameter("@ComId", SqlDbType.UniqueIdentifier) { Value = salaryRptDTO.ComId };
            var dateParam = new SqlParameter("@Date", SqlDbType.Date) { Value = salaryRptDTO.Date };
            var typeParam = new SqlParameter("@Type", SqlDbType.Int) { Value = 0 };
            var departmentIdParam = new SqlParameter("@DepartmentID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var designationIdParam = new SqlParameter("@DesignationID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var isPaidParam = new SqlParameter("@IsPaid", SqlDbType.Bit) { Value = false };



            //string Procedure = "FilterSalaryTable @ComId, @date, @Type, @DepartmentID, @DesignationID";

            ////var SpParameter = new List<SqlParameter>();
            //var ComId = new SqlParameter("@ComId", salaryRptDTO.ComId);
            //var date = new SqlParameter("@date", salaryRptDTO.Date);
            //var type = new SqlParameter("@Type", Type);
            //var DepartmentID = new SqlParameter("@DepartmentID", salaryRptDTO.DeptId);
            //DepartmentID.Value = DBNull.Value;
            //var DesignationID = new SqlParameter("@DesignationID", salaryRptDTO.DesigId);
            //DesignationID.Value = DBNull.Value;

            //var IsPdaid = new SqlParameter("@IsPdaid", salaryRptDTO.IsPaid);

            //var results = _context.Set<Dictionary<string, object>>()
            //    .FromSqlRaw(Procedure, ComId, date, type, DepartmentID, DesignationID, IsPdaid)
            //    .ToList();


            //var result = await Task.Run(() => _context.Database
            //.ExecuteSqlRawAsync(@"exec FilterSalaryTable @ComId, @date, @Type, @DepartmentID, @DesignationID", SpParameter.ToArray()));
            //var results = _context.Database.ExecuteSqlRawAsync("FilterSalaryTable @ComId, @date, @Type, @DepartmentID, @DesignationID",SpParameter);
            //List<SalaryReport> SalaryList =  _context.SalaryReport.FromSqlRaw(Procedure, ComId, date, type, DepartmentID, DesignationID).ToList();
            //List<SalaryReport> SalaryList =  _context.SalaryReport.FromSqlRaw(Procedure, ComId, date, type, DepartmentID, DesignationID, IsPdaid)
            //        .AsEnumerable()
            //        .Where(s => s.IsPaid == false)
            //        .ToList();




            var SalaryList = _context.SalaryReport.FromSqlRaw("EXEC FilterSalaryTable @ComId, @Date,@Type, @DepartmentID, @DesignationID, @IsPaid",
                comIdParam, dateParam, typeParam, departmentIdParam, designationIdParam, isPaidParam).ToList();


            //    var SpParameter = new[]
            //    {
            //    new SqlParameter("@ComId", salaryRptDTO.ComId),
            //    new SqlParameter("@date", salaryRptDTO.Date),
            //    new SqlParameter("@Type", Type),
            //    new SqlParameter("@DepartmentID", salaryRptDTO.DeptId),
            //    new SqlParameter("@DesignationID", salaryRptDTO.DesigId),
            //    new SqlParameter("@IsPdaid", salaryRptDTO.IsPaid)

            //     };


            //var SalaryList = _context.SalaryReport.FromSqlRaw(Procedure, SpParameter).ToList();

            return View(SalaryList);
        }



        public IActionResult SalarySummaryReportView(SalaryRptDTO salaryRptDTO)
        {


            var comIdParam = new SqlParameter("@ComId", SqlDbType.UniqueIdentifier) { Value = salaryRptDTO.ComId };
            var dateParam = new SqlParameter("@Date", SqlDbType.Date) { Value = salaryRptDTO.Date };
            var typeParam = new SqlParameter("@Type", SqlDbType.Int) { Value = 1 };
            var departmentIdParam = new SqlParameter("@DepartmentID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var designationIdParam = new SqlParameter("@DesignationID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var isPaidParam = new SqlParameter("@IsPaid", SqlDbType.Bit) { Value = false };


            var SalarySummaries = _context.SalarySummaryReport.FromSqlRaw("EXEC FilterSalaryTable @ComId, @Date,@Type, @DepartmentID, @DesignationID, @IsPaid",
                comIdParam, dateParam, typeParam, departmentIdParam, designationIdParam, isPaidParam).ToList();
            //type = new SqlParameter("@Type", Type);

            // List<SalarySummaryReport> SalarySummaryList = _context.SalarySummaryReport.FromSqlRaw(Procedure, ComId, date, type, DepartmentID, DesignationID).ToList();

            // Redirect to another action or view
            return View(SalarySummaries);
        }

    }
}
