using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NewKhumaloCraft.Controllers
{
    public class OrchaController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public OrchaController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> StartOrchestration()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsync("https://<your-function-app-name>.azurewebsites.net/api/Function1_HttpStart", null);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content($"Durable Function started. Status response: {content}");
            }

            return Content("Failed to start Durable Function");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
