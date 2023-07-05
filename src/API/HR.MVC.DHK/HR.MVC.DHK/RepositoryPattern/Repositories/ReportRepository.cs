using HR.MVC.DHK.Models;
using HR.MVC.DHK.Models.DTO;
using HR.MVC.DHK.Models.VM;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HR.MVC.DHK.RepositoryPattern.Repositories
{
    public class ReportRepository :IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context) 
        {
            _context = context;
        }


        public async Task<List<SalaryReport>> SalaryReportView(SalaryRptDTO salaryRptDTO)
        {
            var comIdParam = new SqlParameter("@ComId", SqlDbType.UniqueIdentifier) { Value = salaryRptDTO.ComId };
            var dateParam = new SqlParameter("@Date", SqlDbType.Date) { Value = salaryRptDTO.Date };
            var typeParam = new SqlParameter("@Type", SqlDbType.Int) { Value = 0 };
            var departmentIdParam = new SqlParameter("@DepartmentID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var designationIdParam = new SqlParameter("@DesignationID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var isPaidParam = new SqlParameter("@IsPaid", SqlDbType.Bit) { Value = false };



            var SalaryList = _context.SalaryReport.FromSqlRaw("EXEC FilterSalaryTable @ComId, @Date,@Type, @DepartmentID, @DesignationID, @IsPaid",
                comIdParam, dateParam, typeParam, departmentIdParam, designationIdParam, isPaidParam).ToList();

            return SalaryList;
        }
        public async Task<List<SalarySummaryReport>> SalarySummaryReportView(SalaryRptDTO salaryRptDTO)
        {

            var comIdParam = new SqlParameter("@ComId", SqlDbType.UniqueIdentifier) { Value = salaryRptDTO.ComId };
            var dateParam = new SqlParameter("@Date", SqlDbType.Date) { Value = salaryRptDTO.Date };
            var typeParam = new SqlParameter("@Type", SqlDbType.Int) { Value = 1 };
            var departmentIdParam = new SqlParameter("@DepartmentID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var designationIdParam = new SqlParameter("@DesignationID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var isPaidParam = new SqlParameter("@IsPaid", SqlDbType.Bit) { Value = false };



            var SalarySummaries = _context.SalarySummaryReport.FromSqlRaw("EXEC FilterSalaryTable @ComId, @Date,@Type, @DepartmentID, @DesignationID, @IsPaid",
                comIdParam, dateParam, typeParam, departmentIdParam, designationIdParam, isPaidParam).ToList();

            return SalarySummaries;
        }

        public async Task<List<DailyAttendance>> DailyAttendtanceReport(AttendanceParameters AttendanceParameters)
        {
            var comIdParam = new SqlParameter("@ComId", SqlDbType.UniqueIdentifier) { Value = AttendanceParameters.ComId };
            var dateParam = new SqlParameter("@Date", SqlDbType.Date) { Value = AttendanceParameters.Date };
            var AttendanceTypeParam = new SqlParameter("@AttendanceType", SqlDbType.VarChar) { Value = "DAILY" };
            var AttStatusParam = new SqlParameter("@AttStatus", SqlDbType.VarChar) { Value = AttendanceParameters.AttStatus };
            var departmentIdParam = new SqlParameter("@DepartmentID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var empIdParam = new SqlParameter("@EmpId", SqlDbType.UniqueIdentifier) { Value = AttendanceParameters.EmpId };



            var DailyAttendances = _context.DailyAttendances.FromSqlRaw("EXEC AttendanceFilter @ComId, @Date,@AttendanceType,@AttStatus, @DepartmentID, @EmpId",
                comIdParam, dateParam, AttendanceTypeParam, AttStatusParam, departmentIdParam, empIdParam).ToList();

            return DailyAttendances;
        }

        public async Task<List<MonthlyAttendance>> MonthlyAttendtanceReport(AttendanceParameters AttendanceParameters)
        {
            var comIdParam = new SqlParameter("@ComId", SqlDbType.UniqueIdentifier) { Value = AttendanceParameters.ComId };
            var dateParam = new SqlParameter("@Date", SqlDbType.Date) { Value = AttendanceParameters.Date };
            var AttendanceTypeParam = new SqlParameter("@AttendanceType", SqlDbType.VarChar) { Value = "Monthly" };
            var AttStatusParam = new SqlParameter("@AttStatus", SqlDbType.VarChar) { Value = AttendanceParameters.AttStatus };
            var departmentIdParam = new SqlParameter("@DepartmentID", SqlDbType.UniqueIdentifier) { Value = DBNull.Value };
            var empIdParam = new SqlParameter("@EmpId", SqlDbType.UniqueIdentifier) { Value = AttendanceParameters.EmpId };



            var MonthlyAttendances = _context.MonthlyAttendances.FromSqlRaw("EXEC AttendanceFilter @ComId, @Date,@AttendanceType,@AttStatus, @DepartmentID, @EmpId",
                comIdParam, dateParam, AttendanceTypeParam, AttStatusParam, departmentIdParam, empIdParam).ToList();

            return MonthlyAttendances;

        }
    }
}
