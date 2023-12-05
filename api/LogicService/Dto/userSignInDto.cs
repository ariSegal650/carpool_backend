

namespace LogicService.Dto
{
    public class userSignInDto
    {
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Image { get; set; }
        public Cardto? car { get; set; }
    }
    public class Cardto
    {
        public string? Year { get; set; }
        public string? TypeCar { get; set; }
        public string? seats { get; set; }
        public string? TrunkSize { get; set; }
        public string? Image { get; set; }
    }

}
