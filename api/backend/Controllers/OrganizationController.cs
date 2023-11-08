
using LogicService.Dto;
using LogicService.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _OrganizationService;
        private readonly VerificationService _VerificationService;

        public OrganizationController(OrganizationService dB, VerificationService verificationService)
        {
            _OrganizationService = dB;
            _VerificationService = verificationService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDto org)
        {
            var status = await _OrganizationService.CreateOrganization(org);
            return status ? Ok() : BadRequest();
        }

        [HttpPost("test")]
        public IActionResult test()
        {
            var result = _VerificationService.GetSmsVerification();
            return result.sucsses ? Ok(result) : BadRequest(result);
        }
        [HttpPost("check")]
        public IActionResult check(string code)
        {
            _VerificationService.CheckSms(code);
            return Ok();
        }

    }
}

