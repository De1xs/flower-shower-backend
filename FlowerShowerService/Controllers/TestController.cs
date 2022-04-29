using Microsoft.AspNetCore.Mvc;

namespace FlowerShowerService.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("test")]
        public IActionResult TestEndpoint()
        {
            return Ok("Hello world");
        }
    }
}
