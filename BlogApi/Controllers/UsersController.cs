using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserGetDto>>> GetAll()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{value}")]
        public async Task<ActionResult<UserGetDto>> SearchUser(string value)
        {
            var user = await _userService.SearchUser(value);
            if (user.Count == 0)
            {
                return NotFound($"User was not found!");
            }
            return Ok(user);
        }
    }
}
