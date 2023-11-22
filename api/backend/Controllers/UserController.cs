using LogicService.Dto;
using LogicService.EO;
using LogicService.Services;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Get([FromQuery] string id)
        {
            Console.WriteLine(id);
            return Ok(_userService.GetUser(id));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserInfoEO user)
        {
            var result = _userService.CreateUser(user).Result;
            return result.sucsses ? NoContent() : BadRequest(result);

        }

        [HttpPost("test")]
        public IActionResult test([FromBody] aa coord)
        {
            const double earthRadius = 6371; // Earth's radius in kilometers

            // Compute the differences in latitude and longitude between the two coordinates
            double dLat = DegreesToRadians(coord.b.Lat - coord.a.Lat);
            double dLng = DegreesToRadians(coord.b.Lng - coord.a.Lng);

            // Calculate the Haversine of half the angular distance along each axis
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(coord.a.Lat)) * Math.Cos(DegreesToRadians(coord.b.Lat)) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            // Calculate the central angle (in radians) using the inverse Haversine
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate the distance using the Earth's radius and the central angle
            double distance = earthRadius * c;

            return Ok(distance);
        }

        // Helper function to convert degrees to radians
        private static double DegreesToRadians(double degrees)
        {
            return (degrees * Math.PI) / 180;
        }



    }

}
  

