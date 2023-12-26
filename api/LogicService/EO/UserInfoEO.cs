using MongoDB.Bson.Serialization.Attributes;

namespace LogicService.EO
{
    public class UserInfoEO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; } 
        public string? Nickname { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Image { get; set; }
        public Car? Car { get; set; }
    }
    public class Car
    {
        public int? Year { get; set; }
        public string? TypeCar { get; set; }
        public string? Seats { get; set; }
        public string? TrunkSize {get; set;}
        public string? Image { get; set; }
    }
}