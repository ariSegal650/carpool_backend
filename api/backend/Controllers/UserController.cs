using LogicService.Dto;
using LogicService.EO;
using LogicService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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


        [HttpPost("tasks")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> test([FromBody] UserLatLng coord)
        {
           var response=await _userService.GetDistanceAsync(coord);

            return Ok(response);
        }

        [HttpPost("task")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ExecuteTask([FromBody] RequstDto task)
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = await _userService.CanExecuteTask(jwt,task);

            if (response.sucsses)
                return Ok(response);
            return BadRequest(response);
        }

    }

}
  

