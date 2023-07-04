using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;

namespace HR.MVC.DHK.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
    }
}
