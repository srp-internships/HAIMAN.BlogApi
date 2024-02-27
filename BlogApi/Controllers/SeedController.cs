using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly ISeedService _seedService;
        public SeedController(ISeedService seedService)
        {
            _seedService = seedService;            
        }

        [HttpPost]
        public async Task<ActionResult> SeedDatabase()
        {
            await _seedService.SeedDatabase();
            return Ok();
        }
    }
}
