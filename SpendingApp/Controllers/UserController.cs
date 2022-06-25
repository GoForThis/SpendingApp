using Microsoft.AspNetCore.Mvc;
using SpendingApp.ModelsDTO;
using SpendingApp.Services;

namespace SpendingApp.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterDTO dto)
        {
            _userService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDTO dto)
        {
            string jwt = _userService.GenerateJwt(dto);
            return Ok(jwt);
        }
    }
}
