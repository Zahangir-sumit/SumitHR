using HR.MVC.DHK.RepositoryPattern;
using Microsoft.AspNetCore.Mvc;

namespace HR.MVC.DHK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
    }
}
