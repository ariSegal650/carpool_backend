using LogicService.EO;

namespace LogicService.Dto
{
    public class userSignInDto 
    {
        public string Name { get; set; }=string.Empty;
        public string? Nickname { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Gmail { get; set; }
        public string? Image { get; set; }
        public Car? car { get; set; }
    }

}
