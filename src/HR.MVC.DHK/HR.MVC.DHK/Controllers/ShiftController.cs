using HR.MVC.DHK.Models;
using HR.MVC.DHK.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Controllers
{
    public class ShiftController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShiftController(ApplicationDbContext context)
        {
            _context = context;

        }

        public ActionResult Index()
        {
            //list of department
            var data = _context.Shift.ToList();
            return View(data);
        }
        public IActionResult GetShifts(Guid companyId)
        {
            var shifts = _context.Shift.Where(d => d.ComId == companyId).ToList();
            return Json(shifts);
        }

        //for testing view

        [HttpGet]
        public ActionResult DetailsShift(Guid id)
        {
            var data = _context.Shift.Where(x => x.ShiftId == id).FirstOrDefault();

            return View(data);
        }


        public ActionResult CreateShift()
        {
            ViewBag.Companies = _context.Company.ToList();
            return View();
        }


        [HttpPost]

        public ActionResult CreateShift(Shift data)
        {
            Guid id = Guid.NewGuid();
            data.ShiftId = id;
            try
            {
                _context.Shift.Add(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult GetShift(Guid id)
        {
            //Guid comid = new Guid();

            // _context.Department.Where(x => x.DeptId == id && x.ComId == comid);
            _context.Shift.Where(x => x.ShiftId == id);
            return View();
        }

        public ActionResult EditShift(Guid id)
        {
            try
            {
                var data = _context.Shift.Where(x => x.ShiftId == id).FirstOrDefault();
                ViewBag.Companies = _context.Company.ToList();
                return View(data);
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]

        public ActionResult EditShift(Shift shift)
        {
            try
            {

                _context.Shift.Update(shift);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteShift(Guid id)
        {
            var data = _context.Shift.Where(x => x.ShiftId == id).FirstOrDefault();

            return View(data);
            // return View();
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Shift shift = _context.Shift.Where(x => x.ShiftId == id).FirstOrDefault();
            //Department data =  _context.Department.Where(x => x.DeptId == dept.DeptId);
            _context.Shift.Remove(shift);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}