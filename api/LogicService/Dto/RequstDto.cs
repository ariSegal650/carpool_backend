using LogicService.EO;

namespace LogicService.Dto
{
    public class RequstDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        //public byte? Gender { get; set; }
        public string? Type { get; set; }
        public byte? Count { get; set; } = 0;
        public string? CarSize { get; set; }
        public Place? Origin { get; set; }
        public Place? Destination { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Phone_org { get; set; }

        public OrganizationAdminDto? Admin { get; set; }
        public bool? Executed { get; set; }
        public string? Id_User { get; set; }
        public DateTime? Executed_Time { get; set; }
        public string? Notes { get; set; }
        public double Distance { get; set; }
    }

}