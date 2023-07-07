using HR.MVC.DHK.Models;
using HR.MVC.DHK.Models.DTO;
using HR.MVC.DHK.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace HR.MVC.DHK.RepositoryPattern.IRepositories
{
    public interface IReportRepository 
    {

        Task<List<SalaryReport>> SalaryReportView(SalaryRptDTO salaryRptDTO);
        Task<List<SalarySummaryReport>> SalarySummaryReportView(SalaryRptDTO salaryRptDTO);

        Task<List<DailyAttendance>> DailyAttendtanceReport(AttendanceParameters AttendanceParameters);
        Task<List<MonthlyAttendance>> MonthlyAttendtanceReport(AttendanceParameters AttendanceParameters);
    }
}
