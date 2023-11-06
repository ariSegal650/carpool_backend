
using LogicService.Data;
using LogicService.EO;
using LogicService.Services;
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
        public async Task<IActionResult> CreateOrganization([FromBody] OrganizationInfoEO org)
        {
            var status = await _OrganizationService.CreateOrganization(org);
            return status ? Ok() : BadRequest();
        }
    }
}
