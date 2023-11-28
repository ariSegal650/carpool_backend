

namespace LogicService.EO
{
    public class Place
    {
        public string? Name { get; set; }=string.Empty;
        public string? City { get; set; } = string.Empty;
        public string Lat { get; set; } = string.Empty;
        public string Lng { get; set; } = string.Empty;
       
    }
    public class LatLng
    {
        public string Lat { get; set; } = string.Empty;
        public string Lng { get; set; } = string.Empty;

        public LatLng(string lat,string lng) 
        { 
            this.Lat = lat;
            this.Lng = lng;
        }
        public LatLng()
        {
           
        }

    }
}
