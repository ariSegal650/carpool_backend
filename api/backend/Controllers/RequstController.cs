using LogicService.EO;
using LogicService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

      //  [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRequst([FromBody] Request request)
        {
            var response=await _RequstService.AddReuqst(request);
           
            return response ? Ok() : BadRequest();
        }
    }
}
