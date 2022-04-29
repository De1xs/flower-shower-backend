using FlowerShowerService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShowerService.Controllers
{
    public class TestController : Controller
    {
        private readonly DataContext _db;

        public TestController(DataContext db)
        {
            _db = db;
        }
        [HttpGet("test")]
        public async Task<IActionResult> TestEndpoint()
        {
            var firstTestEntity = await _db.TestEntities.OrderBy(te => te.ID).FirstOrDefaultAsync();

            return Ok($"Hello world, first test entity:{firstTestEntity?.Name}");
        }
    }
}
