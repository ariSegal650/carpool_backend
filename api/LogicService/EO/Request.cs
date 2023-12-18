using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using LogicService.Dto;

namespace LogicService.EO
{
    public class Request
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Type { get; set; }
        public byte Count { get; set; }
        public string? CarSize { get; set; }
        public Place? Origin { get; set; }
        public Place? Destination { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Phone_org { get; set; }
        //  public OrganizationAdminDto? Admin { get; set; }
        public OrganizationDto? Organization { get; set; }
        public bool Executed { get; set; } = false;
        public string? Id_User { get; set; }
        public DateTime Executed_Time { get; set; }
        public string? Notes { get; set; }

        public DateTime? lastModified { get; set; }
    }
}