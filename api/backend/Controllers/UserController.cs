using LogicService.EO;
using LogicService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService user)
        {
            _userService = user;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string id)
        {
            Console.WriteLine(id);
            return Ok(_userService.GetUser(id));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserInfoEO user)
        {
            var result = _userService.CreateUser(user).Result;
            return result.sucsses ? NoContent() : BadRequest(result);

        }
    }
}
