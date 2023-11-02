namespace backend.EO
{
    public class OrganizationInfoEO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Logo { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public bool Confirmed { get; set; }
        public required List<OrganizationUser> Users { get; set; }
        public string? secret { get; set; }
    }
     public class OrganizationUser{
        public int Id { get; set; }
        public int Id_Organization { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Phone { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public int LevelAdmin { get; set; }

    }
   
}