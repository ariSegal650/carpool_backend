using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace LogicService.EO
{
    public class OrganizationInfoEO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public bool Confirmed { get; set; }

        [BsonElement("users")]
        [JsonPropertyName("users")]
        public List<OrganizationUser>? Users { get; set; }
        public string? secret { get; set; }
    }
     public class OrganizationUser{
        public OrganizationUser() { }
        public OrganizationUser(int id, string name, string phone, string email, int levelAdmin)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            LevelAdmin = levelAdmin;
        }

        public int Id { get; set; } = 0;
        public string Name { get; set; }=string.Empty;
        public string Phone { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public int LevelAdmin { get; set; } = 0;

    }
   
}