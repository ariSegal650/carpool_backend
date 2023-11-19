
using LogicService.Dto;
using LogicService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _OrganizationService;

        public OrganizationController(OrganizationService dB )
        {
            _OrganizationService = dB;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDto org)
        {
            var response = await _OrganizationService.CreateOrganization(org);

            if (!response)
                return BadRequest("somthing worng");
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTasks()
        {
            //  JWT is stored in the HttpContext
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

           var response=await _OrganizationService.GetAllRequest(jwt);

            if (response == null) return BadRequest();

            return Ok(response);    
        }

    }
}

