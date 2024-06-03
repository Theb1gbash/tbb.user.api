using Microsoft.AspNetCore.Mvc;
using tbb.users.api.Interfaces;
using tbb.users.api.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserProvider _userProvider;

    public UserController(IUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var result = await _userProvider.RegisterUserAsync(request);
        if (result)
        {
            return Ok(new { Message = "Registration successful!" });
        }
        else
        {
            return BadRequest(new { Message = "Registration failed. Please check the input fields." });
        }
    }
}
