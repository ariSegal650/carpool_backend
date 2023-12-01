
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {

            if (image == null || image.Length == 0)
            {
                return BadRequest("Invalid image file");
            }

            // You can customize the file naming strategy here
            var fileName = Path.GetRandomFileName() + Path.GetExtension(image.FileName);
            var folderName = Path.Combine("resources", "images");
            var filePath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }


            return Ok("Image uploaded successfully");


        }


        //[HttpPost("upload"), DisableRequestSizeLimit]
        //public async Task<IActionResult> Upload()
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.First();
        //        var folderName = Path.Combine("Resources", "Images");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.ContentDisposition);
        //            var fullPath = Path.Combine(pathToSave, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            return Ok(new { fullPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}

        //[HttpPost("upload"), DisableRequestSizeLimit]
        //public async Task<IActionResult> Upload(IFormFile image)
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.First();
        //        var folderName = Path.Combine("Resources", "Images");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.ContentDisposition);
        //            var fullPath = Path.Combine(pathToSave, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            return Ok(new { fullPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}

    }
    

}


