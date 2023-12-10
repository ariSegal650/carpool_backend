using LogicService.Dto;
using LogicService.EO;
using LogicService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequstController : ControllerBase
    {
        private readonly RequstService _RequstService;

        public RequstController(RequstService dB)
        {
            _RequstService = dB;
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> AddRequst([FromBody] RequstDto request)
        {
            //  JWT is stored in the HttpContext
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = await _RequstService.AddReuqst(request,jwt);
           
           if(!response.sucsses) 
                return NotFound();
           return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllTasks()
        {
            //  JWT is stored in the HttpContext
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = await _RequstService.GetAllRequest(jwt);

            if (response == null) return BadRequest();

            return Ok(response);
        }
    }
}
