using LogicService.Dto;
using LogicService.EO;
using LogicService.Services;
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

        //[HttpPost]
        //public IActionResult CreateUser([FromBody] UserInfoEO user)
        //{
        //    var result = _userService.CreateUser(user).Result;
        //    return result.sucsses ? NoContent() : BadRequest(result);

        //}

        [HttpPost("test")]
        //[Authorize(Roles = "user")]
        public async Task<IActionResult> test([FromBody] UserLatLng coord)
        {
           var response=await _userService.GetDistanceAsync(coord);

            return Ok(response);
        }
        //[HttpGet("test")]
        ////[Authorize(Roles = "user")]
        //public async Task<IActionResult> test1()
        //{
        //    var response = await _userService.test();

        //    return Ok(response);
        //}


    }

}
  

