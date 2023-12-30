using LogicService.Dto;
using LogicService.EO;
using LogicService.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Get()
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = await _userService.GetUserDto(jwt);

            return user != null ? Ok(user) : BadRequest();
        }

        [HttpPut]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateParameters(UserInfoEO user)
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = await _userService.UpdateParameters(user, jwt);

            return response.sucsses ? Ok(response) : BadRequest(response);
        }


        [HttpPost("tasks")]
       // [Authorize(Roles = "user")]
        public async Task<IActionResult> GetListOfTasks([FromBody] UserLatLng coord)
        {
            var response = await _userService.GetDistanceAsync(coord);

            return Ok(response);
        }

        [HttpPost("task")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ExecuteTask([FromBody] RequstDto task)
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = await _userService.CanExecuteTask(jwt, task);

            if (response.sucsses)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("history")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetTasksHistory()
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = await _userService.GetTasksHistory(jwt);

            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }

    }

}


