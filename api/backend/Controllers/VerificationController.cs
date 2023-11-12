using LogicService.Dto;
using LogicService.Services;
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
        public IActionResult VerificationCheck([FromBody] VerificationRequstDto requstDto)
        {
            var approved = _VerificationService.ChecCode(requstDto);
            return approved ? Ok() : BadRequest();
        }

    }
}
