using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSSale.Models;
using WSSale.Models.Request;
using WSSale.Models.Response;
using WSSale.Services;

namespace WSSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest model)
        {
            Response response = new Response();
            var userResponse = _userService.Auth(model);
            if (userResponse == null)
            {
                response.Success = 0;
                response.Message = "Incorrect User or Password";
                return BadRequest(response);
            }

            response.Success = 1;
            response.Data = userResponse;
                
            return Ok(response);
        }
    }
}
