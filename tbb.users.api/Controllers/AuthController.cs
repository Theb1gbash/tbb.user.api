using Microsoft.AspNetCore.Mvc;
using tbb.users.api.Models;
using tbb.users.api.Interfaces;
using System.Threading.Tasks;

namespace tbb.users.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterUserAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return RedirectToAction("Welcome");
        }

        [HttpGet("welcome")]
        public IActionResult Welcome()
        {
            return Ok("Welcome to the application!");
        }
    }
}
