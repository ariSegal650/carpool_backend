using LogicService.EO;


namespace LogicService.Dto
{
    public class UserLatLng
    {
        public LatLng origin { get; set;}=new LatLng();
        public LatLng destination { get; set; } = new LatLng();
        public UserInfoEO? UserInfo { get; set; }
       // List<RequstDto>? sortedList = new List<RequstDto>();

    }
}
