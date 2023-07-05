using Microsoft.AspNetCore.Mvc;

namespace HR.UI.Controllers
{
    [Area("Admin")]
    public class BaseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly HttpClient _client;
        public BaseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("HR");
        }
    }
}
