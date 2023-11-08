using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace LogicService.EO
{
    public class OrganizationInfoEO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public bool Confirmed { get; set; }
        public string? Secret { get; set; }

        [BsonElement("users")]
        [JsonPropertyName("users")]
        public List<OrganizationUser>? Users { get; set; }

    }
    public class OrganizationUser
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int LevelAdmin { get; set; } = 0;

    }

}