
using LogicService.Dto;
using LogicService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _OrganizationService;

        public OrganizationController(OrganizationService dB)
        {
            _OrganizationService = dB;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDto org)
        {
            var response = await _OrganizationService.CreateOrganization(org);

            if (!response.sucsses)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOrganization()
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response = await _OrganizationService.GetOrganizationById(jwt);

            if (response == null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationDto org)
        {
            var jwt = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var response =await _OrganizationService.UpdateOrganization(jwt, org);

            if (!response.sucsses)
                return BadRequest(response);
            return Ok(response);
        }


    }
}


