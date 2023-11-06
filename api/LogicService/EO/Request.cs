namespace LogicService.EO
{
    public class Request
    {
        public int Id { get; set; }
        public int Id_Org { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public byte Gender { get; set; }
        public string? Type { get; set; }
        public byte Count { get; set; }
        public string? CarSize { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateEnd { get; set; }
        public string? Phone_org { get; set; }
        public int Id_Org_Admin { get; set; }
        public bool Executed { get; set; }
        public int Id_User { get; set; }
        public DateTime Executed_Time { get; set; }
        public string? Notes { get; set; }
    }
}