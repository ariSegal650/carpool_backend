using LogicService.Dto;
using LogicService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly VerificationService _VerificationService;

        public VerificationController(VerificationService verificationService)
        {
            _VerificationService = verificationService;
        }

        [HttpPost]
        public IActionResult GetVerification([FromBody] VerificationRequstDto requstDto)
        {
            var result = _VerificationService.GetVerification(requstDto);
            return result.sucsses ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Check")]
        public async Task<IActionResult> VerificationCheck([FromBody] VerificationRequstDto requstDto)
        {
            var response =await _VerificationService.ChecCode(requstDto);
            
            if(!response.sucsses)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("CheckCode")]
        public async Task<IActionResult> VerificationCheckUser([FromBody] VerificationUserDto requstDto)
        {
            var response = await _VerificationService.ChecCodeUser(requstDto);

            if (!response.sucsses)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult ValidateJwt()
        {
            return Ok(new { message = "JWT is valid" });
        }

        [HttpGet("ValidateJwt")]
        [Authorize(Roles = "user")]
        public IActionResult ValidateJwtUser()
        {
            return Ok(new { message = "JWT is valid" });
        }

    }
}
