namespace backend.EO
{
    public class UserInfoEO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Nickname { get; set; }
        public required string Phone { get; set; }
        public string? Image { get; set; }
        public Car? car { get; set; }
        public string? Gender { get; set; }
    }
    public class Car
    {
        public int id { get; set; }
        public int IdUser { get; set; }
        public string? TypeCar { get; set; }
        public string? seats { get; set; }
        public string? TrunkSize {get; set;}
        public string? Image { get; set; }
    }
}