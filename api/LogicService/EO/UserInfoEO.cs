using System.Text.Json.Serialization;
using LogicService.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogicService.EO
{
    public class UserInfoEO 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Nickname { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Gmail { get; set; }
        public string? Image { get; set; }
        public Car? Car { get; set; }

        [BsonElement("Tasks")]
        [JsonPropertyName("Tasks")]
        public List<RequstDto> Tasks { get; set; } = new List<RequstDto>();
    }
    public class Car
    {
        public int Year { get; set; }
        public string TypeCar { get; set; } = string.Empty;
        public string Seats { get; set; } = string.Empty;
        public string? TrunkSize { get; set; }
        public string? Image { get; set; }
    }
}