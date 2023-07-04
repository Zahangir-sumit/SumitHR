using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;

namespace HR.MVC.DHK.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompanyController> _logger;

        public AttendanceController(IUnitOfWork unitOfWork, ApplicationDbContext context, ILogger<CompanyController> logger) : base(unitOfWork)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {


            ViewBag.Companies = _unitOfWork.Company.GetAll();  
            return View();
        }

        public IActionResult Create()
        {

            ViewBag.Companies = _unitOfWork.Company.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Attendance attendance)
        {

            //var com = _context.Company.Where(x => x.ComId == attendance.ComId).FirstOrDefault();
            //var emp = _context.Employee.Where(x => x.EmpId == attendance.EmpId).FirstOrDefault();

            //attendance.Company = com;
            //attendance.Employee = emp;

            try
            {
                _unitOfWork.Attendance.Add(attendance);
                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }


            return RedirectToAction("Index", "Attendance");
        }

        public IActionResult AttendanceProcess()
        {

            ViewBag.Companies = _unitOfWork.Company.GetAll();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AttendanceProcess(AttProcess attProcess)
        {

            await _unitOfWork.Attendance.Process(attProcess);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult MonthlyAttendanceProcess()
        {
            var monthOptions = new List<SelectListItem>();

            for (int i = 1; i <= 12; i++)
            {
                var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                monthOptions.Add(new SelectListItem { Value = i.ToString(), Text = monthName });
            }

            ViewBag.MonthOptions = monthOptions;

            ViewBag.Companies = _unitOfWork.Company.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MonthlyAttendanceProcess(MonthlyAttProcess monthlyAttProcess)
        {

            await _unitOfWork.Attendance.MonthlyProcess(monthlyAttProcess);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile file)
        {
            Guid comid = Guid.Parse(Request.Form["comid"]);
            Company com = _context.Company.Where(x => x.ComId ==  comid).FirstOrDefault();  
            
            string ext = Path.GetExtension(file.FileName);
            if (ext == ".txt")
            {
                try
                {


                    if (file != null)
                    {
                        string fileLocation = Path.GetFullPath("wwwroot/Content/Upload/" + com.ComName + "/");
                        if (Directory.Exists(fileLocation))
                        {
                            Directory.Delete(fileLocation, true);
                        }
                        string uploadlocation = Path.GetFullPath("wwwroot/Content/Upload/" + com.ComName + "/");

                        if (!Directory.Exists(uploadlocation))
                        {
                            Directory.CreateDirectory(uploadlocation);
                        }

                        string filePath = uploadlocation + Path.GetFileName(file.FileName);

                        string extension = Path.GetExtension(file.FileName);
                        var fileStream = new FileStream(filePath, FileMode.Create);
                        file.CopyTo(fileStream);
                        fileStream.Close();

                        string path = filePath;
                        string[] readText = System.IO.File.ReadAllLines(path);

                        //var DeviceNo = readText[0][1];
                        List<string> newList = readText.ToList();
                        List<Attendance> attendances = new List<Attendance>();
                        foreach (var item in newList)
                        {
                            var data = item.Split(':');
                            Guid empid = Guid.Parse(data[0]);
                            var punchDate = data[1];
                            var inHour = int.Parse(data[2]);
                            var inMinute = int.Parse(data[3]);
                            //var inSecond = int.Parse(data[4]);
                            var outHour = int.Parse(data[4]);
                            var outMinute = int.Parse(data[5]);
                            // var outSecond = int.Parse(data[7]);

                            var inTime = new TimeSpan(inHour, inMinute, 00);
                            var outTime = new TimeSpan(outHour, outMinute, 00);

                            Attendance db = new Attendance
                            {
                                EmpId = empid,
                                DtDate = DateTime.Parse(punchDate),
                                InTime = inTime,
                                OutTime = outTime,
                                ComId = comid
                            };


                            attendances.Add(db);
                        }
                        await _context.Attendance.AddRangeAsync(attendances);
                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Data Import Successfully";
                        TempData["Status"] = "1";
                    }

                    else
                    {
                        TempData["Message"] = "Please, Select a file!";
                        TempData["Status"] = "3";
                    }

                }
                catch (Exception ex)
                {
                    return Ok(ex.InnerException.Message);

                }
            }

            return View();
        }

        //private string preprocess(IFormFile file, string comid)
        //{
        //    string FileName = null;
        //    string path = "";
        //    if (file != null)
        //    {
        //        //string folder = "book/Gallery/";
        //        string serverFolder = Path.GetFullPath("wwwroot/Content/Upload/" + comid + "/");
        //        FileName = file.FileName;
        //        string filePath = Path.Combine(serverFolder, FileName);
        //        using (var fs = new FileStream(filePath, FileMode.Create))
        //        {
        //            file.CopyTo(fs);
        //        }
        //        path += filePath;
        //    }
        //    return path;
        //}
    }
}
